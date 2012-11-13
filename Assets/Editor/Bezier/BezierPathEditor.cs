using UnityEngine;
using UnityEditor;

/**
 * Custom editor using SerializedProperties to manipulate the BezierPath.
 */ 
[CustomEditor(typeof(BezierPath))]
public class BezierPathEditor : Editor
{
	private BezierPath m_path;
	
	/**
	 * Get the properties
	 */ 
	public void OnEnable()
	{
		m_path = (BezierPath)target;
    }
	
	/**
	 * Draw the bezier path editor
	 */ 
	override public void OnInspectorGUI()
	{
		EditorGUILayout.LabelField("Curves", m_path.GetCountString());
		EditorGUILayout.Separator();
		
		EditorGUILayout.LabelField("Add Bezier Curve");
		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button("Add Linear") )
		{
			m_path.AddCurve( BezierCurve.LINEAR );
		}
		
		if (GUILayout.Button("Add Quadratic") )
		{
			m_path.AddCurve( BezierCurve.QUADRATIC );
		}
		
		if (GUILayout.Button("Add Cubic") )
		{
			m_path.AddCurve( BezierCurve.CUBIC );
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Separator();
		
		EditorGUILayout.LabelField("Connect Curves");
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Copy Begin Points") )
		{
			BezierUtils.CopyBeginPoints(m_path.GetCurvesArray());
		}
		
		if (GUILayout.Button("Close Gaps") )
		{
			BezierUtils.CloseGaps(m_path.GetCurvesArray());
		}
		
		if (GUILayout.Button("Copy End Points") )
		{
			BezierUtils.CopyEndPoints(m_path.GetCurvesArray());
		}
		EditorGUILayout.EndHorizontal();
	}
}