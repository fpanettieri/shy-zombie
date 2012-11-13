using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public BezierNavigator navigator;
	public BezierPath path;
	
	public void Start()
	{
		navigator.Navigate(path.GetCurvesArray());
	}

	public void Update ()
	{
		
	}
}
