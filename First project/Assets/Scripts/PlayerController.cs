using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public float screenWidth = Screen.currentResolution.width;
	public float screenHeight = Screen.currentResolution.height;

	//public float speed = 10.0F;

	void FixedUpdate()
	{

		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		//dir.z = Input.acceleration.x;
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
			
		dir *= Time.deltaTime;
		transform.Translate(dir * speed);

		rigidbody.position = new Vector3
			(
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f, 
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);


		/*float moveHorizontal;
		float moveVertical;
		
		if ((Input.touchCount == 1)) 
		{
			if ( Input.GetTouch(0).position.x < (screenWidth/2) )
			{
				moveHorizontal = -1;
			}
			else
			{
				moveHorizontal = 1;
			}
		}
		else
		{
			moveHorizontal = 0;
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
		rigidbody.velocity = movement * speed * Time.deltaTime;
		
		//movement script goes here 
		rigidbody.position = new Vector3
			(
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f, 
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
				);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);*/

		
	}
}
