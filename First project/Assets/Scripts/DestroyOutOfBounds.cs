using UnityEngine;
using System.Collections;

public class DestroyOutOfBounds : MonoBehaviour
{
	public GameController gameController;

	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
		if (other.tag == "Asteroid")
		{
			gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
			gameController.GameOver ();
		}
	}
}