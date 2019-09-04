using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a class that will evaluate a Catmull-Rom spline.  This will result in a smooth
// continuous curve between a series of controlpoints/waypoints in the world.
// used by npcs for patrols, also commonly used for camera paths in game
public class splineMgr : MonoBehaviour {
	
	public LineRenderer debugPath;
	public List<GameObject> ControlPoints;
	const int kDebugLength = 1024;
	public float dt = 0.01f;
	private float t = 0.0f;
	public float dynamicdt;
	public float target_arclength;
	public float epsilon;
	private int nHead = 0;
	private Vector3 vOut;
	public enum eSplineType
	{
		ST_CATMULLROM
	}
	public eSplineType spline_type = eSplineType.ST_CATMULLROM;
	public GameObject HeadObj;
	public GameObject TargetObj;
	
	public GameObject splineNodeRoot;
	private bool controlPointsInstalled = false;

	public enum ePlaybackType
	{
		invalid = -1,
		none = 0,
		const_dt = 1,
		const_dist = 2
	};
	public ePlaybackType type = ePlaybackType.const_dt;

	public enum ePlaybackMode
	{
		invalid = -1,
		none = 0,
		oneshot = 1,
		loop = 2,
		oneshot_finished = 4,
		computingDebugPath = 5,
		computingDebugPath_finished = 6,
		paused = 7
	};
	public ePlaybackMode playbackMode = ePlaybackMode.loop;

	// Use this for initialization
	void Start () {
		TargetObj = new GameObject();
		nHead = 0;
		if (splineNodeRoot)
			InstallSplineNodes();
	}
	
	public void reset()
	{
		nHead = 0;
		t = 0.0f;
	}
	
	public bool GetHasControlPoints()
	{
		return controlPointsInstalled;
	}

	public void InstallSplineNodes()
	{
		if (splineNodeRoot)
		{
			ControlPoints.Clear ();
			Transform[] allChildren = splineNodeRoot.GetComponentsInChildren<Transform>();
    		foreach (Transform child in allChildren)
			{
    			// do what you want with the transform
				if (child != splineNodeRoot.transform)
					ControlPoints.Add(child.gameObject);
    		}
			controlPointsInstalled = true;
		}
	}

	public void computeDebugPath()
	{
		if (GetHasControlPoints() == false)
		{
			InstallSplineNodes();
		}

		// store settings
		ePlaybackMode pbm = this.playbackMode;
		ePlaybackType pbt = this.type;
		
		// apply settings for path calculation
		this.playbackMode = ePlaybackMode.computingDebugPath;
		this.type = ePlaybackType.const_dt;

		SetPlaybackMode(splineMgr.ePlaybackMode.computingDebugPath);
	
		for (int i = 0 ; i < kDebugLength; i++)
		{
			Vector3 p = getPointOnCurve();
			debugPath.SetPosition(i, p);
			if (IsFinished() == true)
			{
				debugPath.SetVertexCount(i-1);
				break;
			}
		}
		
		// restore values
		playbackMode = pbm;
		type = pbt;
	}

	public void SetPlaybackMode(ePlaybackMode mode)
	{
		playbackMode = mode;
	}
	
	public Vector3 getPointOnCurve()
	{
		eval ();
		return vOut;
	}
	
	public bool IsFinished()
	{
		bool rval = (playbackMode == ePlaybackMode.oneshot_finished ? true : false);
		return rval;
	}

	Vector3 PointOnCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		//
		//	The spline passes through all of the control points.
		//	The spline is C1 continuous, meaning that there are no discontinuities in the tangent direction and magnitude.
		//	The spline is not C2 continuous.  The second derivative is linearly interpolated within each segment, causing the curvature to vary linearly over the length of the segment.
		//	Points on a segment may lie outside of the domain of P1 -> P2.
		Vector3 vOut = new Vector3(0.0f, 0.0f, 0.0f);

		float t2 = t * t;
		float t3 = t2 * t;
		vOut = 0.5f * ((2.0f*p1) + (-p0+p2)*t + (2.0f*p0 - 5.0f*p1 + 4.0f*p2 - p3)*t2 + (-p0 + 3.0f*p1 - 3.0f*p2 + p3)*t3);
		return vOut;
	}
	
	public void eval()
	{
		if (playbackMode == ePlaybackMode.paused)
		{
			vOut = Vector3.zero;
			Vector3 xp0 = ControlPoints[nHead].transform.position;
			Vector3 xp1 = ControlPoints[nHead+1].transform.position;
			Vector3 xp2 = ControlPoints[nHead+2].transform.position;
			Vector3 xp3 = ControlPoints[nHead+3].transform.position;

			// update headObj
			vOut = PointOnCurve(t, xp0, xp1, xp2, xp3);
			if (HeadObj)
				HeadObj.transform.position = vOut;
			
			return;
		}

		// update state, wrap when t exceeds end of curve segment
		if ((playbackMode != ePlaybackMode.oneshot_finished) && (playbackMode != ePlaybackMode.computingDebugPath_finished))
		{
			if (type == ePlaybackType.const_dt)
				t += dt;
			else if (type == ePlaybackType.const_dist)
			{
				// find the delta that moves the arc_length by const_dist
				if (epsilon <= 0.0001f)
					epsilon = 0.0001f;
				//dynamicdt = epsilon;//dhdh
				Vector3 tp0 = ControlPoints[nHead].transform.position;
				Vector3 tp1 = ControlPoints[nHead+1].transform.position;
				Vector3 tp2 = ControlPoints[nHead+2].transform.position;
				Vector3 tp3 = ControlPoints[nHead+3].transform.position;
				float sampleDist = 0.0f;
				while (sampleDist < target_arclength)
				{
					Vector3 sample1 = PointOnCurve (t, tp0, tp1, tp2, tp3);
					Vector3 sample2 = PointOnCurve (t+dynamicdt, tp0, tp1, tp2, tp3);
					Vector3 vDisplacement = (sample1 - sample2);
					sampleDist = vDisplacement.magnitude;
					if (sampleDist < target_arclength)
						dynamicdt += epsilon;
					else if (sampleDist > target_arclength)
						dynamicdt -= epsilon;
				}
				t += dynamicdt;
			}
		}

		if (t > 1.0f)
		{
			t -= 1.0f;
			nHead++;
			dynamicdt = epsilon;
		}
		
		// extract interpolated point from spline
		//Vector3 vOut = new Vector3(0.0f, 0.0f, 0.0f);
		vOut = Vector3.zero;
		Vector3 p0 = ControlPoints[nHead].transform.position;
		Vector3 p1 = ControlPoints[nHead+1].transform.position;
		Vector3 p2 = ControlPoints[nHead+2].transform.position;
		Vector3 p3 = ControlPoints[nHead+3].transform.position;
		
		// end case of path compute mode
		if (playbackMode == ePlaybackMode.computingDebugPath)
		{
			if (nHead == (ControlPoints.Count - 4))
			{
				t = 0.0f;
				playbackMode = ePlaybackMode.computingDebugPath_finished;
			}
		}
		
		// wrap nHead once we reach end
		if (nHead == (ControlPoints.Count-4))
		{
			if (playbackMode == ePlaybackMode.loop)
			{
				t = 0.0f;
				nHead = 0;
			}
			else if (playbackMode == ePlaybackMode.oneshot)
			{
				t = 0.0f;
				playbackMode = ePlaybackMode.oneshot_finished;
			}
			else if (playbackMode == ePlaybackMode.computingDebugPath)
			{
				t = 0.0f;
				playbackMode = ePlaybackMode.computingDebugPath_finished;
			}
		}
		
		// update headObj
		vOut = PointOnCurve(t, p0, p1, p2, p3);
		if (HeadObj)
			HeadObj.transform.position = vOut;

		// update lookObj
		if (TargetObj)
		{
			Vector3 tgtPos = Vector3.zero;
			tgtPos = PointOnCurve (t+dt, p0, p1, p2, p3);
			TargetObj.transform.position = tgtPos;
		}
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if ((playbackMode != ePlaybackMode.computingDebugPath) && (playbackMode != ePlaybackMode.none))
			eval ();
	}
}
