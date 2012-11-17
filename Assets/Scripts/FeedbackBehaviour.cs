using UnityEngine;
using System.Collections;

public class FeedbackBehaviour : MonoBehaviour
{
	// Global state
	public static bool feedbackPressed;
	private Vector2 feedbackButton;
	
	// Auxiliar variables
	private Touch touch;
	private bool visible;
	private bool clickable;
	
	public void Start ()
	{
		clickable = true;
		visible = false;
		feedbackPressed = false;
		feedbackButton = new Vector2(928, 688);
	}
	
	public void Update ()
	{
		feedbackPressed = Input.GetKey(KeyCode.Space);
		if(Input.GetMouseButton(0) && Vector2.Distance(Input.mousePosition, feedbackButton) < 64){ feedbackPressed = true; }
		
		if (feedbackPressed && clickable) {
			clickable = false;
			visible = !visible;
			GUIText[] texts = GetComponentsInChildren<GUIText>();
			for(int i = 0; i < texts.Length; i++){
				texts[i].enabled = visible;
			}
		}
		
		if(!clickable && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))){
			clickable = true;
		}
	}
}
