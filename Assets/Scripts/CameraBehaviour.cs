using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
	// inspector properties
	public Transform target;
	public float height = 3;
	public float distance = 4;
	public float angle = 0;
	
	// aux variables
	private Vector3 offset;
	
	public void Start ()
	{
		MoveCamera();
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
	
	// Hack used to debug camera configuration
	public void OnDrawGizmos()
	{
		if(target == null){
			Debug.LogWarning("Camera target is null");
			return;
		}
		
		MoveCamera();
	}
	
}
