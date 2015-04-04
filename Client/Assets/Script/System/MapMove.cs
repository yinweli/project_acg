using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
	public int RoadIndex = 0;
	public Vector3 NextPos = new Vector3();

    public int NextRoad = 1;
    // ------------------------------------------------------------------
	void Start()
	{        
		MapCreater.This.Create();
        NewGame();
	}
    // ------------------------------------------------------------------
	void Update()
    {/*
        if (Vector2.Distance(Vector3.zero, MapCreater.This.GetRoadObj(NextRoad).transform.position) < 0.01f)
            NextRoad++;

        MoveTo(NextRoad);
		MapCreater.This.Refresh(NextRoad);*/
	}
    // ------------------------------------------------------------------
    void NewGame()
    {
        NextRoad = 1;
        transform.localPosition = new Vector3(-MapCreater.This.GetRoadObj(0).transform.localPosition.x, -MapCreater.This.GetRoadObj(0).transform.localPosition.y, 0);
    }
    // ------------------------------------------------------------------
    void MoveTo(int iRoad)
    {
        Vector3 vecDirection = MapCreater.This.GetRoadObj(iRoad - 1).transform.localPosition - MapCreater.This.GetRoadObj(iRoad).transform.localPosition;

        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.localPosition += vecDirection.normalized * GameDefine.fMoveSpeed * 2 * Time.deltaTime;
    }
}
