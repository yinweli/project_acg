using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
    public GameObject CameraCtrl;
    public int iNextRoad = 1;
    // ------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // 如果沒路當做勝利.
        if (!MapCreater.This.GetRoadObj(iNextRoad))
        {
            return;
        }

        // 檢查距離.
        if (Vector2.Distance(transform.position, MapCreater.This.GetRoadObj(iNextRoad).transform.position) < 0.005f)
            iNextRoad++;

        MoveTo(iNextRoad);
    }
    // ------------------------------------------------------------------
    void MoveTo(int iRoad)
    {
        Vector3 vecDirection = MapCreater.This.GetRoadObj(iRoad).transform.position - transform.position;

        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.localPosition += vecDirection.normalized * GameDefine.fMoveSpeed * Time.deltaTime;

        if (CameraCtrl)
        {
            Camera.main.gameObject.transform.localPosition += -1 * vecDirection.normalized * GameDefine.fMoveSpeed * Time.deltaTime;
            MapCreater.This.Refresh(iNextRoad);
        }
    }
}
