using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
    static public MapMove pthis = null;

    public GameObject pCamCtrl;
	public int RoadIndex = 0;
	public Vector3 NextPos = new Vector3();
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    public void StartNew()
    {
        transform.localPosition = new Vector3(-MapCreater.This.GetRoadObj(0).transform.localPosition.x, -MapCreater.This.GetRoadObj(0).transform.localPosition.y, 0);
    }
    // ------------------------------------------------------------------
}