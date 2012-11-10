using UnityEngine;
using System.Collections;

public class DebugAccelerometer : MonoBehaviour
{

	void Start ()
	{
	
	}

	void Update ()
	{
		guiText.text = Input.acceleration.ToString();
	}
}
