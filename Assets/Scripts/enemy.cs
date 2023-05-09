using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "pulling")
		{
			Destroy(this.gameObject);
		}
	}
}
