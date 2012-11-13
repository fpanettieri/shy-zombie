using UnityEngine;
using UnityEditor;

/**
 * BezierPath custom editor
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
		EditorGUILayout.LabelField("Length", m_path.GetLengthString());
		EditorGUILayout.Separator();
		
		EditorGUILayout.LabelField("Add Bezier Curve");
		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button("Add Linear") )
		{
			Undo.RegisterSceneUndo("Add Linear");
			m_path.AddCurve( BezierCurve.LINEAR );
		}
		
		if (GUILayout.Button("Add Quadratic") )
		{
			Undo.RegisterSceneUndo("Add Quadratic");
			m_path.AddCurve( BezierCurve.QUADRATIC );
		}
		
		if (GUILayout.Button("Add Cubic") )
		{
			Undo.RegisterSceneUndo("Add Cubic");
			m_path.AddCurve( BezierCurve.CUBIC );
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Separator();
		
		EditorGUILayout.LabelField("Connect Curves");
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Copy Begin Points") )
		{
			Undo.RegisterSceneUndo("Copy Begin Points");
			BezierUtils.CopyBeginPoints(m_path.GetCurvesArray());
		}
		
		if (GUILayout.Button("Close Gaps") )
		{
			Undo.RegisterSceneUndo("Close Gaps");
			BezierUtils.CloseGaps(m_path.GetCurvesArray());
		}
		
		if (GUILayout.Button("Copy End Points") )
		{
			Undo.RegisterSceneUndo("Copy End Points");
			BezierUtils.CopyEndPoints(m_path.GetCurvesArray());
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Update curve length") )
		{
			Undo.RegisterSceneUndo("Update curve length");
			BezierUtils.UpdateLength(m_path);
		}		
		EditorGUILayout.EndHorizontal();
	}
}