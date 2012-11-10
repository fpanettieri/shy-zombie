using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
	// Inspector properties
	public float walkSpeed = 6.0f;
	public float walkAcceleration = 20.0f;
	public float walkDeceleration = 60.0f;
	public float walkAnimationSpeed = 2.0f;
	public float rotateSpeed = 3.0f;
	public float idleTime = 10.0f;
	
	// Internal state
	private bool leftPressed;
	private bool rightPressed;
	private int state;
	private int previousState;
	private float currentSpeed;
	private float _idleTime;
	
	// Auxiliar variables
	private Touch touch;
	private new Animation animation;
	private CharacterController controller;
	private Vector3 forward;
	
	public void Start ()
	{
		leftPressed = false;
		rightPressed = false;
		state = CharacterConstants.IDLE_STATE;
		previousState = CharacterConstants.IDLE_STATE;
		
		animation = GetComponentInChildren<Animation>();
		animation.Play("zombie_idle");
		animation["zombie_walk"].speed = walkAnimationSpeed;
		
		controller = GetComponent<CharacterController>();
	}
	
	public void Update ()
	{
		DetectInput();
		SwitchState();
		UpdateState();
		ChangeAnimation();
	}
	
	private void DetectInput()
	{
#if !UNITY_IPHONE
		leftPressed = false;
		rightPressed = false;
	
		for(int i = 0; i < Input.touchCount; i++){
			touch = Input.GetTouch(i);
			if(touch.position.x < LEFT_SIDE){ leftPressed = true; }
			if(touch.position.x > RIGHT_SIDE){ rightPressed = true; }
		}
#else
		leftPressed = Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W);
		rightPressed = Input.GetMouseButton(1) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W);
#endif
	}
	
	private void SwitchState()
	{
		if(leftPressed && rightPressed){
			state = CharacterConstants.WALKING_STATE;
		} else if(leftPressed) {
			state = CharacterConstants.TURNING_LEFT_STATE;
		} else if(rightPressed){
			state = CharacterConstants.TURNING_RIGHT_STATE;
		} else {
			state = CharacterConstants.IDLE_STATE;
		}
	}
	
	private void UpdateState()
	{
		if(state == CharacterConstants.TURNING_LEFT_STATE){
			transform.Rotate(0, -rotateSpeed, 0);
			
		} else if(state == CharacterConstants.TURNING_RIGHT_STATE) {
			transform.Rotate(0, rotateSpeed, 0);
			
		}
		
		if(state == CharacterConstants.WALKING_STATE){
			currentSpeed += walkAcceleration * Time.deltaTime;
		} else {
			currentSpeed -= walkDeceleration * Time.deltaTime;
		}
		
		currentSpeed = Mathf.Clamp(currentSpeed, 0, walkSpeed);
		forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * currentSpeed);
	}
	
	private void ChangeAnimation()
	{
		if(previousState == state){
			if(state == CharacterConstants.IDLE_STATE){
				_idleTime += Time.deltaTime;
				if(_idleTime > idleTime){
					animation.Play("zombie_attack");
					animation.Play("zombie_idle", AnimationPlayMode.Queue);
					_idleTime = -2;
					// TODO: SFX groan 
				}
			}
			return; 
		}
		
		Debug.Log("Changing animation to " + state);
		_idleTime = 0;
		
		switch(state){
		case CharacterConstants.IDLE_STATE:
		case CharacterConstants.TURNING_LEFT_STATE:
		case CharacterConstants.TURNING_RIGHT_STATE:
			animation.CrossFade("zombie_idle"); break;
		case CharacterConstants.WALKING_STATE:
			animation.CrossFade("zombie_walk"); break;
		} 
		
		previousState = state;
	}
	
	
}
