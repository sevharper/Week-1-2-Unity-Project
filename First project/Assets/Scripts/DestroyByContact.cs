using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		Network.Instantiate(explosion, transform.position, transform.rotation,0);
		if (other.tag == "Player")
		{
			Network.Instantiate(playerExplosion, other.transform.position, other.transform.rotation,0);
			//gameController.GameOver ();
		}
		Network.Destroy(other.gameObject);
		Network.Destroy(gameObject);
	}
}