using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	// inspector properties
	public BezierNavigator navigator;
	public BezierPath path;
	public float interval = 0;
	public float skip = 0;
	
	// internal state
	private bool delayed;
	private float delay;
	private int counter;
	
	// auxiliar variables 
	private new Animation animation;
	
	public void Start()
	{
		counter = 0;
		navigator.OnCurve = OnCurve;
		navigator.OnComplete = OnComplete;
		navigator.Navigate(path.GetCurvesArray());
		animation = GetComponentInChildren<Animation>();
		animation.Play("walk");
	}

	public void Update ()
	{
		if(delayed){
			delay -= Time.deltaTime;
			if(delay < 0){
				delayed = false;
				animation.Play("walk", AnimationPlayMode.Mix);
				navigator.Resume();
			}
		}
	}
	
	public void OnCurve()
	{
		if(counter++ < skip){ return; }
		counter = 0;
		delayed = true;
		delay = interval;
		navigator.Pause();
		animation.Play("idle", AnimationPlayMode.Mix);
	}
	
	public void OnComplete()
	{
		navigator.Navigate(path.GetCurvesArray());
	}
}
