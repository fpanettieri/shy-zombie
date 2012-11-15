using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
	private const float MAX_ANGLE = Mathf.PI * 2;
	
	// inspector properties
	public Transform target;
	public float minZoom = 0.01f;
	public float maxZoom = 5;
	public float zoom = 2;
	public float zoomRatio = 2;
	public float angle = 0;
	public float rotationSwipe = 200;
	public float zoomSwipe = 100;
	
	// aux variables
	private float distance;
	private Vector3 offset;
	private bool moving;
	private Vector3 beginPosition;
	private float beginAngle;
	private float beginZoom;
	
	// scared animation
	private bool scared;
	private float scaredTime;
	private Vector3 scaredBeginPosition;
	private Quaternion scaredBeginRotation;
	private Vector3 scaredEndPosition;
	private Quaternion scaredEndRotation;
	
	public void Start ()
	{
		moving = false;
		MoveCamera();
	}
	
	public void Update ()
	{
		if(scared){
			scaredTime -= Time.deltaTime;
			if(scaredTime > 1){
				transform.position = Vector3.Lerp(scaredBeginPosition, scaredEndPosition, 2 - scaredTime);
				transform.rotation = Quaternion.Lerp(scaredBeginRotation, scaredEndRotation, 2 - scaredTime);
			}
			
			if (scaredTime < 0){
				scared = false;
				angle = 2.3f;
			}
			return;
		}
		DetectInput();
		MoveCamera();
	}
	
	public void DetectInput()
	{
		if(CharacterBehaviour.leftPressed || CharacterBehaviour.rightPressed || CharacterBehaviour.walkPressed) {
			moving = false;
			return;
		}
		
		if(moving){
			angle = beginAngle + (Input.mousePosition.x - beginPosition.x) / rotationSwipe;
			if(angle > MAX_ANGLE){ angle -= MAX_ANGLE; }
			if(angle < 0){ angle += MAX_ANGLE; }
			
			zoom = Mathf.Clamp( beginZoom + (Input.mousePosition.y - beginPosition.y) / zoomSwipe, minZoom, maxZoom);
			
			if(Input.GetMouseButtonUp(0)){ moving = false; }
			
			
		} else if(Input.GetMouseButtonDown(0)) {
			moving = true;
			beginPosition = Input.mousePosition;
			beginAngle = angle;
			beginZoom = zoom;
		}
		
	}
	
	public void MoveCamera()
	{
		distance = zoom * zoomRatio;
		offset = new Vector3(Mathf.Sin(angle) * distance, zoom, Mathf.Cos(angle) * distance);
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
	
	public void Scare(Transform enemy)
	{
		scared = true;
		scaredTime = 2;
		scaredBeginPosition = transform.position;
		scaredBeginRotation = transform.rotation;
		scaredEndPosition = enemy.position + enemy.forward * 3;
		scaredEndRotation = enemy.rotation * Quaternion.Euler( new Vector3(0, 180, 0 ));
	}
}
