using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour 
{
    static public CameraCtrl pthis = null;

    public int iNextRoad = 1;
    public int iLeaderRoad = 1;
    public bool bCanMove = true;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // 如果沒路就是勝利.
        if (!MapCreater.This.GetRoadObj(iNextRoad))
        {
            return;
        }

        // 檢查隊伍是否為停滯狀態.
        if (!bCanMove)
            return;

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
        transform.localPosition += vecDirection.normalized * SysMain.pthis.GetMoveSpeed() * Time.deltaTime;

        Camera.main.gameObject.transform.localPosition += -1 * vecDirection.normalized * SysMain.pthis.GetMoveSpeed() * Time.deltaTime;
        MapCreater.This.Refresh(iNextRoad);
    }
}
