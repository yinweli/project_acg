using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
    public GameObject pCamCtrl;
    public GameObject pObjBg;
	public int RoadIndex = 0;
	public Vector3 NextPos = new Vector3();

    public int NextRoad = 1;
    // ------------------------------------------------------------------
	void Start()
	{        
		MapCreater.This.Create();
        NewGame();
	}
    void Update()
    {
        pObjBg.transform.position = pCamCtrl.transform.position;
    }
    // ------------------------------------------------------------------
    void NewGame()
    {
        NextRoad = 1;
        transform.localPosition = new Vector3(-MapCreater.This.GetRoadObj(0).transform.localPosition.x, -MapCreater.This.GetRoadObj(0).transform.localPosition.y, 0);
    }
    // ------------------------------------------------------------------
    
}
