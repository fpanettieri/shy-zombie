using UnityEngine;

/**
 * Bezier curves are used to model smooth curves that can be
 * scaled indefinitely.
 * 
 * This is the base class and is not used directly in any Path.
 * Instead they are composed of Linear, Quadratic or Cubic Curves
 * that implement code optimized for each type.
 */
public abstract class BezierCurve : MonoBehaviour
{	
	private const float APPROXIMATION_STEPS = 100;
	
	public static string LINEAR = "linear";
	public static string QUADRATIC = "quadratic";
	public static string CUBIC = "cubic";
	
	public float m_length = 1;
		
	protected Transform m_begin;
	protected Transform m_end;
	
	// EndPoint accessors
	public Transform BeginPoint	{ get { return m_begin; } }
	public Transform EndPoint	{ get { return m_end; } }
	
	void Awake()
	{
		FindControlPoints();	
	}
	
	/**
	 * Calculate a point along the curve ( 0.0f >= t <= 1.0f )
	 */ 
	public virtual Vector3 GetPosition( float step )
	{
		return Vector3.one;
	}
	
	/**
	 * Calculate a rotation along the curve ( 0.0f >= t <= 1.0f )
	 */ 
	public virtual Quaternion GetRotation( float step )
	{
		return Quaternion.identity;
	}
	
	/**
	 * It creates the game objects used to control the bezier.
	 */ 
	public virtual void CreateControlPoints( BezierCurve previous )
	{
		m_begin = new GameObject("BeginPoint").transform;
		m_end = new GameObject("EndPoint").transform;
	
		m_begin.parent = transform;
		m_end.parent = transform;
		
		if( previous )
		{
			m_begin.position = previous.EndPoint.position;
			m_begin.rotation = previous.EndPoint.rotation;
		}
		
		m_end.position = m_begin.position + new Vector3(0, 0, 10);
		m_end.rotation = m_begin.rotation;
	}
	
	/**
	 * Update the curve length to an aproximated value.
	 */
	public virtual void UpdateLength()
	{
		m_length = 0;
		Vector3 begin, end;
		
		for( int i = 0; i < APPROXIMATION_STEPS - 1; i++ ){
			begin = GetPosition( i / APPROXIMATION_STEPS );
			end = GetPosition( (i + 1) / APPROXIMATION_STEPS );
			m_length = Vector3.Distance( begin, end );
		}
	}
	
	/**
	 * Called each frame to render the bezier gizmos on screen.
	 * This draw
	 */ 
	void OnDrawGizmos()
	{
		FindControlPoints();
		
		Gizmos.color = Color.gray;
		DrawControlLines();
		DrawControlPoints();
		
		Gizmos.color = Color.white;
		DrawLine();
		DrawPoints();
	}
	
	/**
	 * Called each frame to render the bezier gizmos on screen
	 */ 
	void OnDrawGizmosSelected()
	{
		FindControlPoints();
		
		Gizmos.color = Color.green;
		DrawControlLines();
		DrawControlPoints();
		
		Gizmos.color = Color.yellow;
		DrawLine();
		DrawPoints();
	}
	
	/**
	 * Get control point references in Editor
	 */ 
	protected virtual void FindControlPoints()
	{
		if ( m_begin == null )
		{
			m_begin = transform.FindChild("BeginPoint");
		}
		
		if ( m_end == null )
		{
			m_end = transform.FindChild("EndPoint");
		}
	}
	
	/**
	 * Render the line between end points and control points
	 */ 
	protected virtual void DrawControlLines(){}
	
	/**
	 * Render control points
	 */ 
	protected virtual void DrawControlPoints(){}
	
	/**
	 * Render the bezier
	 */ 
	protected virtual void DrawLine(){}
	
	/**
	 * Render begin and end points
	 */ 
	protected void DrawPoints()
	{
		DrawOrientedCube( m_begin );
		DrawOrientedCube( m_end );
	}
		
	/**
	 * Util function used to render an oriendet cube, based on a given transform 
	 */ 
	protected void DrawOrientedCube( Transform transform )
	{
		// Push render config
		Color color = Gizmos.color;
		Gizmos.matrix = transform.localToWorldMatrix;		
		
		// Draw helper guides
		Gizmos.color = Color.blue;
		Gizmos.DrawLine( Vector3.forward, Vector3.forward * 2 );
		
		Gizmos.color = Color.red;
		Gizmos.DrawLine( Vector3.left, Vector3.left * 2 );
		Gizmos.DrawLine( Vector3.right , Vector3.right * 2 );
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine( Vector3.up, Vector3.up * 2 );
		Gizmos.DrawLine( Vector3.down, Vector3.down * 2 );
		
		// Pop cube color
		Gizmos.color = color;
		Gizmos.DrawCube( Vector3.zero, Vector3.one );
		Gizmos.matrix = Matrix4x4.identity;
	}
}
