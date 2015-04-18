using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCreater : MonoBehaviour 
{
    static public PlayerCreater pthis = null;

    public int iCount = 0;

    public GameObject pPrePlayer;

    public Dictionary<GameObject, Member> CatchList = new Dictionary<GameObject, Member>();
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
	public void StartNew() 
    {
        iCount = 0;
        Create();
        StartCoroutine(WaitCreate());
	}
    // ------------------------------------------------------------------
    public void ClearList()
    {
        foreach (KeyValuePair<GameObject, Member> itor in CatchList)
            Destroy(itor.Key);

        CatchList.Clear();
    }
    // ------------------------------------------------------------------
    // 接關時建角色用.
    void CreateAll()
    {

    }
    // ------------------------------------------------------------------
    public void AddList(float fPosX, float fPosY, int iSex, int iLook)
    {
        // 增加玩家資料.
        Member temp = new Member();
        temp.iSex = iSex;
        temp.iLook = iLook;

        GameObject pObj = UITool.pthis.CreateUIByPos(gameObject, "G_Player", fPosX, fPosY);
        pObj.name = string.Format("Role{0:000}", PlayerData.pthis.Members.Count + CatchList.Count);
        pObj.GetComponent<AIPlayer>().iPlayer = PlayerData.pthis.Members.Count + CatchList.Count;
        pObj.GetComponent<AIPlayer>().Init(false, temp);

        CatchList.Add(pObj, temp);        
    }
    // ------------------------------------------------------------------
    public void SaveRole(GameObject pObj)
    {
        pPrePlayer = pObj;
        iCount++;

        SysMain.pthis.Role.Add(pObj, iCount);
        SysMain.pthis.CatchRole.Add(pObj, iCount);
        CatchList.Remove(pObj);
    }
    // ------------------------------------------------------------------
    void Create()
    {
        if (PlayerData.pthis.Members.Count <= 0)
        {
            Debug.Log("No Player Data!!");
            return;
        }
        // 建立第一個玩家.
        pPrePlayer = UITool.pthis.CreateUI(gameObject, "Prefab/G_Player");
        pPrePlayer.name = string.Format("Role{0:000}", iCount);
        pPrePlayer.GetComponent<AIPlayer>().iPlayer = iCount;
        pPrePlayer.GetComponent<AIPlayer>().Init(true, PlayerData.pthis.Members[iCount]);

        // 加入玩家佇列.
        SysMain.pthis.Role.Add(pPrePlayer, iCount);
        SysMain.pthis.CatchRole.Add(pPrePlayer, iCount);
        iCount ++;
    }
    // ------------------------------------------------------------------
    IEnumerator WaitCreate()
    {
		while (iCount < PlayerData.pthis.Members.Count)
        {
            if (SysMain.pthis.bIsGaming)
            {
                yield return new WaitForEndOfFrame();
                // 檢查上一個玩家是否距離已到.
                if (Vector2.Distance(pPrePlayer.transform.position, MapCreater.pthis.GetRoadObj(0).transform.position) > 0.195f)
                    Create();
            }
        }       
    }
}
