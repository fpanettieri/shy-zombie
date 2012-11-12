using UnityEngine;
using System.Collections;

public class HudBehaviour : MonoBehaviour
{
	// Injected properties
	public MeshRenderer leftButtonOff;
	public MeshRenderer leftButtonOn;
	
	public MeshRenderer rightButtonOff;
	public MeshRenderer rightButtonOn;
	
	public MeshRenderer walkButtonOff;
	public MeshRenderer walkButtonOn;
	
	public GUIText brainzText;
	
	private int brainz;
	
	public void Start()
	{
		brainz = 0;
	}
	
	public void FixedUpdate ()
	{	
		leftButtonOff.enabled = !CharacterBehaviour.leftPressed;
		leftButtonOn.enabled = CharacterBehaviour.leftPressed;
		
		rightButtonOff.enabled = !CharacterBehaviour.rightPressed;
		rightButtonOn.enabled = CharacterBehaviour.rightPressed;
		
		walkButtonOff.enabled = !CharacterBehaviour.walkPressed;
		walkButtonOn.enabled = CharacterBehaviour.walkPressed;
		
		if(CharacterBehaviour.brainz != brainz){
			brainz = CharacterBehaviour.brainz;
			brainzText.text = brainz.ToString();	
		}
	}
}
