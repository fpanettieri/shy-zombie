using UnityEngine;
using System.Collections;

public class HudBehaviour : MonoBehaviour
{
	// Injected properties
	public GameObject leftButtonOff;
	public GameObject leftButtonOn;
	
	public GameObject rightButtonOff;
	public GameObject rightButtonOn;
	
	public GameObject walkButtonOff;
	public GameObject walkButtonOn;
	
	public void FixedUpdate ()
	{	
		leftButtonOff.renderer.enabled = !CharacterBehaviour.leftPressed;
		leftButtonOn.renderer.enabled = CharacterBehaviour.leftPressed;
		
		rightButtonOff.renderer.enabled = !CharacterBehaviour.rightPressed;
		rightButtonOn.renderer.enabled = CharacterBehaviour.rightPressed;
		
		walkButtonOff.renderer.enabled = !CharacterBehaviour.walkPressed;
		walkButtonOn.renderer.enabled = CharacterBehaviour.walkPressed;
	}
}
