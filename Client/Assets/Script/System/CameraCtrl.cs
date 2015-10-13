using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour 
{
    static public CameraCtrl pthis = null;

    public int iNextRoad = 1;
    public int iLeaderRoad = 1;

    public bool bTestMove = false;

    public bool bTest = false;

    public GameObject ObjObstacle = null;
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
        {
            if (!bTestMove)
                DataGame.pthis.iRoad = iNextRoad;
            iNextRoad++;
        }
    }
    // ------------------------------------------------------------------
    public void StartNew()
    {
        bTestMove = false;

        // 有舊位子就先移動到舊位子上去.
        if (DataGame.pthis.iRoad != 0)
            SetPos(DataGame.pthis.iRoad);
        else
            ResetPos();
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            AIEnemy pAI = other.gameObject.GetComponent<AIEnemy>();
            if (pAI && (ENUM_ModeMonster)pAI.DBFData.Mode == ENUM_ModeMonster.NoMove)
            {
                ObjObstacle = other.gameObject;
                if (SysMain.pthis.AtkEnemy.ContainsKey(other.gameObject) == false)
                    SysMain.pthis.AtkEnemy.Add(other.gameObject, pAI.GetTheat());
            }
        }
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
    void SetPos(int iRoad)
    {
        iNextRoad = iRoad + 1;
        transform.localPosition = MapCreater.pthis.GetRoadObj(iRoad).transform.position;
        Camera.main.gameObject.transform.localPosition = -1 * MapCreater.pthis.GetRoadObj(iRoad).transform.position;
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
        if (bTest)
            return false;

        if (bTestMove)
            return true;

        if (ToolKit.CatchRole.Count <= 0)
            return false;

        if (ObjObstacle)
            return false;
        
        return true;
    }
    // ------------------------------------------------------------------
    public GameObject GetMyObj()
    {
        return gameObject;
    }
    // ------------------------------------------------------------------
    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(2);
        ResetPos();
    }
    // ------------------------------------------------------------------
}
