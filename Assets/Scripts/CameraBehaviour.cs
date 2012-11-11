using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
	// inspector properties
	public Transform target;
	public float height = 2;
	public float distance = 5;
	public float angle = 45;
	
	// internal state
	private bool moving;
	
	// aux variables
	private Vector3 offset;
	
	public void Start ()
	{
		moving = false;
	}
	
	public void Update ()
	{
		DetectInput();
		MoveCamera();
	}
	
	public void DetectInput()
	{
		
	}
	
	public void MoveCamera()
	{
		offset = new Vector3(Mathf.Sin(angle) * distance, height, Mathf.Cos(angle) * distance);
		transform.position = target.position + offset;
		transform.LookAt(target);
	}
	
}
