using UnityEngine;
using System.Collections;

public class DestroyOutOfBounds : MonoBehaviour
{
	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}
}