using UnityEngine;

/**
 * A Quadratic Bézier curve is a parabolic (or conic) segment.
 */
public class QuadraticBezierCurve : BezierCurve
{
	protected Transform m_control;
	private float segments = 10;

	override public Vector3 GetPosition( float step )
	{
		Vector3 position =
			Mathf.Pow( 1 - step, 2) * m_begin.position +
			2 * ( 1 - step ) * step * m_control.position +
			Mathf.Pow( step, 2) * m_end.position;
		return position;
	}
	
	override public Quaternion GetRotation( float step )
	{
		Quaternion q1 = Quaternion.Slerp( m_begin.rotation, m_control.rotation, step );
		Quaternion q2 = Quaternion.Slerp( m_control.rotation, m_end.rotation, step );
		Quaternion rotation = Quaternion.Slerp( q1, q2, step );
		return rotation;
	}
		
	override public void CreateControlPoints( BezierCurve previous )
	{
		base.CreateControlPoints( previous );
			
		m_control = new GameObject("ControlPoint").transform;
		m_control.parent = transform;
		m_control.position = m_begin.position + new Vector3(10, 0, 5);
	}
	
	override protected void FindControlPoints()
	{
		base.FindControlPoints();
		if ( m_control == null )
		{
			m_control = transform.FindChild("ControlPoint").transform;
		}
	}
	
	override protected void DrawControlLines()
	{
		Gizmos.DrawLine( m_begin.position, m_control.position );
		Gizmos.DrawLine( m_control.position, m_end.position );
	}
	
	override protected void DrawControlPoints()
	{
		DrawOrientedCube( m_control );
	}
	
	override protected void DrawLine()
	{
		float segmentLength = 1.0f / segments;
		for(int i = 0; i < segments; i++)
		{
			Gizmos.DrawLine( GetPosition(i * segmentLength), GetPosition((i + 1) * segmentLength) );
		}
	}
}
