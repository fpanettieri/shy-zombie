using UnityEngine;

/**
 * This class can be added to BezierCurves to modify it's
 * navigation sped properties: speed, accel, etc.
 * 
 * BezierCurves that don't have specific BezierSpeed will retain
 * previous speed properties. If no previous curve has defined
 * speed properties, the Navigator will use it's default configuration.
 */
public class BezierSpeed : MonoBehaviour
{
	public float MinSpeed = 0;
	public float MaxSpeed = 1;
	public float Acceleration = 0.1f;
	public float Deceleration = 0.05f;
}
