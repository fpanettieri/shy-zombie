using UnityEditor;
using UnityEngine;

/**
 * This class adds a new GameObject type called Bezier
 * 
 * It represents a bezier path, a group of ordered bezier curves
 */ 
public class MenuTest : MonoBehaviour {

    [MenuItem ("GameObject/Create Other/BezierPath")]
    static void CreateBezierPath () {
        GameObject obj = new GameObject("BezierPath");
		obj.AddComponent<BezierPath>();
    }
	
}