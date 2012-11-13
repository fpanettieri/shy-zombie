using UnityEngine;

/**
 * A linear Bezier curve is simply a straight line between two points.
 */
public class LinearBezierCurve : BezierCurve
{
	override public Vector3 GetPosition( float step )
	{
		return Vector3.Lerp( m_begin.position, m_end.position, step );
	}
	
	override public Quaternion GetRotation( float step )
	{
		return Quaternion.Slerp( m_begin.rotation, m_end.rotation, step );
	}
	
	override protected void DrawLine()
	{
		Gizmos.DrawLine( m_begin.position, m_end.position );
	}
}
