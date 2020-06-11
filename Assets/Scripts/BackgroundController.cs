using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
	void Update()
	{
		transform.Translate(-0.01f, 0, 0);
		if (transform.position.x < -13.8f)
		{
			transform.position = new Vector3(13.8f, 0, 0);
		}
	}
}