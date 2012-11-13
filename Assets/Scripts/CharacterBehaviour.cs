using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
	// Inspector properties
	public float walkSpeed = 1.5f;
	public float walkAcceleration = 5.0f;
	public float walkDeceleration = 5.0f;
	public float walkAnimationSpeed = 2.5f;
	public float rotateSpeed = 1.5f;
	public float idleTime = 10.0f;
	
	// Global state
	public static bool leftPressed;
	public static bool rightPressed;
	public static bool walkPressed;
	public static int brainz = 0;
	
	private int state;
	private int previousState;
	private float currentSpeed;
	private float _idleTime;
	
	// Auxiliar variables
	private Touch touch;
	private new Animation animation;
	private CharacterController controller;
	private Vector3 forward;
	
	private Vector2 leftButton;
	private Vector2 rightButton;
	private Vector2 walkButton;
	
	public void Start ()
	{
		leftPressed = false;
		rightPressed = false;
		walkPressed = false;
		state = CharacterConstants.IDLE_STATE;
		previousState = CharacterConstants.IDLE_STATE;
		
		animation = GetComponentInChildren<Animation>();
		animation.Play("zombie_idle");
		animation["zombie_walk"].speed = walkAnimationSpeed;
		
		controller = GetComponent<CharacterController>();
		
		leftButton = new Vector2(80, 184);
		rightButton = new Vector2(192, 80);
		walkButton = new Vector2(888, 136);
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
		leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
		rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
		walkPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

		for(int i = 0; i < Input.touchCount; i++){
			touch = Input.GetTouch(i);
			if(Vector2.Distance(touch.position, leftButton) < 64){ leftPressed = true; }
			if(Vector2.Distance(touch.position, rightButton) < 64){ rightPressed = true; }
			if(Vector2.Distance(touch.position, walkButton) < 64){ walkPressed = true; }
		}
	}
	
	private void SwitchState()
	{
		if(walkPressed){
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
		if(leftPressed){
			transform.Rotate(0, -rotateSpeed, 0);
			
		} else if(rightPressed) {
			transform.Rotate(0, rotateSpeed, 0);
		}
		
		if(walkPressed){
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
		
		_idleTime = 0;
		
		switch(state){
		case CharacterConstants.IDLE_STATE:
			animation.CrossFade("zombie_idle");
			audio.Pause();
			break;
		case CharacterConstants.TURNING_LEFT_STATE:
		case CharacterConstants.TURNING_RIGHT_STATE:
		case CharacterConstants.WALKING_STATE:
			animation.CrossFade("zombie_walk");
			audio.Play();
			break;
		} 
		
		previousState = state;
	}
	
	public void OnTriggerEnter (Collider trigger)
	{
		if(trigger.tag == "Collectable"){
			brainz++;
			Destroy (trigger.gameObject);
			// TODO: SFX omn nom nom 
		}
		
		if(trigger.tag == "Goal" && brainz > 0){
			Debug.Log("Show puzzle complete screen");
			// TODO: SFX omn nom nom 
		}
	}
}
