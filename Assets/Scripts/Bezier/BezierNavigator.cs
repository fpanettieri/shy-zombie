using UnityEngine;

/**
 * BezierNavigator is a script that allows any game object to move 
 * along a given BezierPath
 * 
 * After being attached on a GameObject, is has a method (navigate) 
 * that recives a BezierPath that will be navigated.
 * 
 * This class also presents 3 callbacks that can be used to execute
 * code in multiple events: When the navigation starts, each time a curve
 * has completed, and when the navigator get's to the end of the curves array
 */
public class BezierNavigator : MonoBehaviour
{
	public delegate void Callback();
	public Callback OnStart;
	public Callback OnCurve;
	public Callback OnComplete;
	
	// inspector properties
	public float MinSpeed = 0.1f;
	public float MaxSpeed = 0.5f;
	public float Acceleration = 0.05f;
	public float Deceleration = 0.05f;
	
	// internal state
	private bool m_paused = false;
	private bool m_done = true;
	
	private BezierCurve[] m_curves;
	private BezierCurve m_curve;
	private int m_curveIndex;
	private bool m_waitingForSkip = false;
	
	public float m_step = 0;
	public float m_speed = 0;
	

	
	void Update()
	{
		if( m_done || m_paused )
		{
			return;
		}
		AdjustSpeed();
		TakeStep();
		MoveObject();
		CheckEndOfCurve();
	}
	
	/**
	 * Start navigating a given curve
	 */ 
	public void Navigate(BezierCurve[] curves)
	{
		if( curves.Length < 1 )
		{
			Debug.LogWarning("Curves array is empty");
			return;
		}
		
		m_curves = curves;
		MoveToCurve( 0 );
		ExecuteCallback( OnStart );
		m_done = false;
	}
	
	/**
	 * Move the navigator to the given curve
	 */ 
	private void MoveToCurve( int index )
	{
		if( index >= m_curves.Length )
		{
			Debug.LogError("Curve index out of bounds " + index.ToString());
			return;
		}

		m_curveIndex = index;
		m_curve = m_curves[ m_curveIndex ];
		
		// TODO: check if using the interpolated step makes the transition smoother
		//transform.position = m_curve.BeginPoint.position;
		//transform.rotation = m_curve.BeginPoint.rotation;
		m_step = 0;
		
		BezierSpeed curveSpeed = m_curve.GetComponent<BezierSpeed>();
		if( curveSpeed )
		{
			MinSpeed = curveSpeed.MinSpeed;
			MaxSpeed = curveSpeed.MaxSpeed;
			Acceleration = curveSpeed.Acceleration;
			Deceleration = curveSpeed.Deceleration;
		}
	}
	
	/**
	 * Execute navigator callbacks only if they are defined
	 */ 
	private void ExecuteCallback( Callback callback )
	{
		if (callback != null)
		{
			callback();	
		}
	}
	
	/**
	 * Adjust the speed of the navigator based on current configuration
	 * and elapsed time
	 */ 
	private void AdjustSpeed()
	{
		if( m_speed < MaxSpeed )
		{
			m_speed += Acceleration * Time.deltaTime;
		}
		else if ( m_speed > MaxSpeed )
		{
			m_speed -= Deceleration * Time.deltaTime;
		}
		
		if( m_speed < MinSpeed )
		{
			m_speed = MinSpeed;
		}
	}
	
	/**
	 * Move the Navigator a step forward
	 */ 
	private void TakeStep()
	{
		m_step += m_speed * Time.deltaTime / m_curve.m_length;
		if( m_step > 1 )
		{
			m_step = 1;	
		}
	}
	
	/**
	 * Move the navigable 
	 */ 
	private void MoveObject()
	{
		transform.position = m_curve.GetPosition( m_step );
		transform.rotation = m_curve.GetRotation( m_step );
	}
	
	/**
	 * Check if the navigator has arrived to the end of the curve
	 * Call the OnComplete callback if its the last curve, or move 
	 * to the next curve if not.
	 */ 
	private void CheckEndOfCurve()
	{
		if( m_waitingForSkip || m_step < 1 )
		{
			return;
		}
		
		m_step = 0;
		ExecuteCallback( OnCurve );
		
		if( m_curveIndex < m_curves.Length - 1 )
		{
			MoveToCurve( m_curveIndex + 1 );
		}
		else
		{
			m_done = true;
			m_curves = null;
			m_curve = null;
			m_curveIndex = 0;
			ExecuteCallback( OnComplete );
		}
	}
	
	public void Pause()
	{
		m_paused = true;
	}
	
	public void Resume()
	{
		m_paused = false;
	}
}
