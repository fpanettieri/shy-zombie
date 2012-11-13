using UnityEngine;

/**
 * Cubic bezier are the most common bezier curve, used on CAD.
 * It's rendered
 */
public class CubicBezierCurve : BezierCurve
{
	protected Transform m_beginControl;
	protected Transform m_endControl;
	private float segments = 20;

	override public Vector3 GetPosition( float step )
	{
		Vector3 position =
			Mathf.Pow( 1 - step, 3) * m_begin.position +
			3 * Mathf.Pow( 1 - step, 2) * step * m_beginControl.position +
			3 * ( 1 - step ) * Mathf.Pow( step, 2 ) * m_endControl.position +
			Mathf.Pow( step, 3) * m_end.position;
		return position;
	}
	
	override public Quaternion GetRotation( float step )
	{
		Quaternion q1 = Quaternion.Slerp( m_begin.rotation, m_beginControl.rotation, step );
		Quaternion q2 = Quaternion.Slerp( m_beginControl.rotation, m_endControl.rotation, step );
		Quaternion q3 = Quaternion.Slerp( m_endControl.rotation, m_end.rotation, step );
		Quaternion q12 = Quaternion.Slerp( q1, q2, step );
		Quaternion q23 = Quaternion.Slerp( q2, q3, step );
		Quaternion rotation = Quaternion.Slerp( q12, q23, step );
		return rotation;
	}
		
	override public void CreateControlPoints( BezierCurve previous )
	{
		base.CreateControlPoints( previous );
			
		m_beginControl = new GameObject("BeginControlPoint").transform;
		m_beginControl.parent = transform;
		m_beginControl.position = m_begin.position + new Vector3(-10, 0, 0);
		
		m_endControl = new GameObject("EndControlPoint").transform;
		m_endControl.parent = transform;
		m_endControl.position = m_end.position + new Vector3(10, 0, 0);
	}
	
	override protected void FindControlPoints()
	{
		base.FindControlPoints();
		if ( m_beginControl == null )
		{
			m_beginControl = transform.FindChild("BeginControlPoint").transform;
		}
		
		if ( m_endControl == null )
		{
			m_endControl = transform.FindChild("EndControlPoint").transform;
		}
	}
	
	override protected void DrawControlLines()
	{
		Gizmos.DrawLine( m_begin.position, m_beginControl.position );
		Gizmos.DrawLine( m_end.position, m_endControl.position );
	}
	
	override protected void DrawControlPoints()
	{
		DrawOrientedCube( m_beginControl );
		DrawOrientedCube( m_endControl );
	}
	
	override protected void DrawLine()
	{
		float segmentLength = 1.0f / segments;
		for(int i = 0; i < segments; i++)
		{
			// TODO[fabio]: rotate segment color to make the lione more visible?
			Gizmos.DrawLine( GetPosition(i * segmentLength), GetPosition((i + 1) * segmentLength) );
		}
	}
}
