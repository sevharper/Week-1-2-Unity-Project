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
	//public float screenWidth = Screen.width;
	//public float screenHeight = Screen.height;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	private int playerNum;

	public PlayerController (int networkNumber)
	{
		playerNum = networkNumber;
	}

	void Update ()
	{
		if (networkView.isMine) 
		{
			if (Input.GetButton ("Fire1") && Time.time > nextFire) 
			{
				nextFire = Time.time + fireRate;
				Network.Instantiate (shot, shotSpawn.position, shotSpawn.rotation,0);
				audio.Play ();
			}
		}
	}

	void FixedUpdate()
	{
		if (networkView.isMine) 
		{
			Vector3 dir = Vector3.zero;
			dir.x = Input.acceleration.x;
			//dir.z = Input.acceleration.x;
			if (dir.sqrMagnitude > 1)
					dir.Normalize ();
			
			dir *= Time.deltaTime;
			transform.Translate (dir * speed);

			rigidbody.position = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		
			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);

		}
	}
}
