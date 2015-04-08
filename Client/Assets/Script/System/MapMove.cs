using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
    public GameObject pCamCtrl;
	public int RoadIndex = 0;
	public Vector3 NextPos = new Vector3();

    // ------------------------------------------------------------------
	void Start()
	{        
		MapCreater.This.Create(SysMain.pthis.Data.iStage, 0);
        NewGame();
	}
    // ------------------------------------------------------------------
    void NewGame()
    {
        transform.localPosition = new Vector3(-MapCreater.This.GetRoadObj(0).transform.localPosition.x, -MapCreater.This.GetRoadObj(0).transform.localPosition.y, 0);
    }
    // ------------------------------------------------------------------
}