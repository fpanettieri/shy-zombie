using UnityEngine;
using System.Collections.Generic;

/**
 * Collection of useful functions that can be used 
 * to manipulate bezier curves
 */
public class BezierUtils
{
	/**
	 * curve[i].end_point.transform = curve[i + 1].begin_point.transform
	 */
	public static void CopyBeginPoints(BezierCurve[] curves)
	{
		for( int i = 0; i < curves.Length - 1; i++ )
		{
			CopyBeginPoint(curves[i + 1], curves[i]);
		}
	}
	
	/**
	 * curve[i].end_point.transform = Lerp(end_point, begin_point, 0.5f)
	 * curve[i + 1].begin_point.transform = Lerp(end_point, begin_point, 0.5f)
	 */ 
	public static void CloseGaps(BezierCurve[] curves)
	{
		for( int i = 0; i < curves.Length - 1; i++ )
		{
			CloseGap(curves[i], curves[i + 1]);
		}
	}
	
	/**
	 * curve[i + 1].begin_point.transform = curve[i].end_point.transform
	 */
	public static void CopyEndPoints(BezierCurve[] curves)
	{
		for( int i = 0; i < curves.Length - 1; i++ )
		{
			CopyEndPoint(curves[i], curves[i + 1]);
		}
	}
	
	/**
	 * curve[i].end_point.transform = curve[i + 1].begin_point.transform
	 */
	public static void CopyBeginPoint(BezierCurve src, BezierCurve dst)
	{
		dst.EndPoint.position = src.BeginPoint.position;
		dst.EndPoint.rotation = src.BeginPoint.rotation;
	}
	
	/**
	 * curve[i].end_point.transform = Lerp(end_point, begin_point, 0.5f)
	 * curve[i + 1].begin_point.transform = Lerp(end_point, begin_point, 0.5f)
	 */ 
	public static void CloseGap(BezierCurve src, BezierCurve dst)
	{
		Vector3 p = Vector3.Lerp(src.EndPoint.position, dst.BeginPoint.position, 0.5f);
		Quaternion r = Quaternion.Slerp(src.EndPoint.rotation, dst.BeginPoint.rotation, 0.5f);
		src.EndPoint.position = p;
		src.EndPoint.rotation = r;
		dst.BeginPoint.position = p;
		dst.BeginPoint.rotation = r;
	}
	
	/**
	 * curve[i + 1].begin_point.transform = curve[i].end_point.transform
	 */
	public static void CopyEndPoint(BezierCurve src, BezierCurve dst)
	{
		dst.BeginPoint.position = src.EndPoint.position;
		dst.BeginPoint.rotation = src.EndPoint.rotation;
	}

	/**
	 * Update the length of the path and inner curves.
	 */
	public static void UpdateLength(BezierPath path)
	{
		path.UpdateLength();
	}
}