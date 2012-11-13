using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
	private const int ROWS = 4;
	private const int COLUMNS = 8;
	private const int LIMIT = COLUMNS + 1;
	private const int FRAMES = ROWS * COLUMNS;
	
	public int frameSkip;
	
	private bool visible;
	private int frame;
	private int skip;
	private Vector2 tiling;
	private Vector2 offset;
	
	public void Start()
	{
		visible = false;
		frame = 0;
		tiling = new Vector2( 1.0f / COLUMNS, 1.0f / ROWS );
		offset = new Vector2( 0, 0 );
	}
	
	public void FixedUpdate()
	{
		if(visible){ return; }
		visible = CharacterBehaviour.brainz > 0;
		if(visible) { renderer.enabled = true; }
	}
	
	public void Update ()
	{
		if(!visible){ return; }
		if (++skip >= frameSkip){
			skip = 0;
			frame = ++frame % FRAMES;
			offset.Set( tiling.x * (frame % COLUMNS), tiling.y * Mathf.Floor(frame / COLUMNS));
			renderer.material.SetTextureOffset("_MainTex", offset);
		}
	}
}
