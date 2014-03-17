using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public GameController gameController;
	
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
			gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
			gameController.GameOver ("Game Over!");
		}
		else if (other.tag == "Laser")
		{
			gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
			gameController.AddScore(100);
		}
		Network.Destroy(other.gameObject);
		Network.Destroy(gameObject);
	}
}