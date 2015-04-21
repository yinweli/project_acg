using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour 
{
    static public CameraCtrl pthis = null;

    public int iNextRoad = 1;
    public int iLeaderRoad = 1;

    public bool bTestMove = false;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (!bTestMove && !SysMain.pthis.bIsGaming)
            return;

        // 如果沒路就是勝利.
        if (!MapCreater.pthis.GetRoadObj(iNextRoad))
        {
            // 測試模式下重頭走過.
            if (bTestMove)
                StartCoroutine(ReStart());
            else
                SysMain.pthis.Victory();

            return;
        }

        // 檢查隊伍是否為停滯狀態.
        if (!CheckCanMove())
            return;        

        MoveTo(iNextRoad);
    }
    // ------------------------------------------------------------------
    void FixedUpdate()
    {
        if (!bTestMove && !SysMain.pthis.bIsGaming)
            return;

        if (!MapCreater.pthis.GetRoadObj(iNextRoad))
            return;
        // 檢查距離.
        if (Vector2.Distance(transform.position, MapCreater.pthis.GetRoadObj(iNextRoad).transform.position) < 0.018f)
            iNextRoad++;
    }
    // ------------------------------------------------------------------
    public void StartNew()
    {
        bTestMove = false;
        ResetPos();
        // 有舊位子就先移動到舊位子上去.

    }
    // ------------------------------------------------------------------
    public void LoginMove()
    {
        bTestMove = true;
        ResetPos();
    }
    // ------------------------------------------------------------------
    void ResetPos()
    {
        iNextRoad = 1;
        MapCreater.pthis.Refresh(iNextRoad);
        transform.localPosition = Vector3.zero;
        Camera.main.gameObject.transform.localPosition = Vector3.zero;
    }
    // ------------------------------------------------------------------
    void MoveTo(int iRoad)
    {
        if (!MapCreater.pthis.GetRoadObj(iRoad))
            return;

        Vector3 vecDirection = MapCreater.pthis.GetRoadObj(iRoad).transform.position - transform.position;

        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.localPosition += vecDirection.normalized * SysMain.pthis.GetMoveSpeed() * Time.deltaTime;

		Camera.main.gameObject.transform.localPosition += -1 * vecDirection.normalized * SysMain.pthis.GetMoveSpeed() * Time.deltaTime * Camera.main.gameObject.transform.localScale.x;
        MapCreater.pthis.Refresh(iNextRoad);
    }
    // ------------------------------------------------------------------
    public bool CheckCanMove()
    {
        if (bTestMove)
            return true;

        if (SysMain.pthis.CatchRole.Count <= 0)
            return false;

        return true;
    }
    // ------------------------------------------------------------------
    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(2);
        ResetPos();
    }
    // ------------------------------------------------------------------
}
