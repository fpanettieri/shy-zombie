using UnityEngine;
using System.Collections;

public class Brainz : MonoBehaviour
{
	private const float MAX_STEP = Mathf.PI * 2;
	public float bounce = 0.01f;
	public float bounceSpeed = 1;
	public float rotationSpeed = 1;
	private float step = 0;

	public void Update ()
	{
		step += bounceSpeed * Time.deltaTime;
		if (step > MAX_STEP) {
			step -= MAX_STEP;
		}
		transform.Rotate (0, rotationSpeed, 0);
		transform.Translate (0, Mathf.Sin (step) * bounce, 0);
	}
}
