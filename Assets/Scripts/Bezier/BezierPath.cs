using UnityEngine;
using System.Collections.Generic;

/**
 * A Bezier path is combination of linked Bezier Curves
 * Each curve is refered as a Segment of the Path.
 * 
 * A path can contain any number of curves, each one being of
 * a different type (Linear, Quadratic or Cubic).
 * 
 * I'ts used to animate objects
 */
public class BezierPath : MonoBehaviour
{
	private float m_length = 0;
	
	/**
	 * Add a new curve based on the given type
	 * Bezier are defined as constants in the BezierCurve class
	 */
	public void AddCurve(string type)
	{
		GameObject obj = new GameObject();
		BezierCurve previous = GetLastCurve();
		BezierCurve curve = null;
		if( type == BezierCurve.LINEAR )
		{
			obj.name = "LinearBezierCurve";
			curve = obj.AddComponent<LinearBezierCurve>();
		}
		else if( type == BezierCurve.QUADRATIC )
		{
			obj.name = "QuadraticBezierCurve";
			curve = obj.AddComponent<QuadraticBezierCurve>();
		}
		else if( type == BezierCurve.CUBIC )
		{
			obj.name = "CubicBezierCurve";
			curve = obj.AddComponent<CubicBezierCurve>();
		}
		obj.name = GetCountString() + "0_" + obj.name;
		obj.transform.parent = transform;
		curve.CreateControlPoints( previous );
	}
	
	public void UpdateLength()
	{
		m_length = 0;
		BezierCurve[] curves = GetCurvesArray();
		for(int i = 0; i < curves.Length; i++) {
			curves[i].UpdateLength();
			m_length += curves[i].m_length;
		}
	}
	
	public BezierCurve[] GetCurvesArray()
	{
		return GetComponentsInChildren<BezierCurve>(true);
	}
	
	public BezierCurve GetFirstCurve()
	{
		BezierCurve[] curves = GetCurvesArray();
		return curves.Length > 0 ? curves[0] : null;
	}
	
	public BezierCurve GetLastCurve()
	{
		BezierCurve[] curves = GetCurvesArray();
		return curves.Length > 0 ? curves[curves.Length - 1] : null;
	}
	
	public string GetCountString()
	{
		return transform.GetChildCount().ToString();	
	}
	
	public string GetLengthString()
	{
		return m_length.ToString();	
	}
}
