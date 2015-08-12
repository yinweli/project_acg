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
    public void AddList(int iItemID, float fPosX, float fPosY, int iLooks)
    {
        // 增加玩家資料.
        Member temp = new Member();

        temp.iLooks = iLooks;

        GameObject pObj = UITool.pthis.CreateUIByPos(gameObject, "G_Player", fPosX, fPosY);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY, -0.1f * (DataPlayer.pthis.MemberParty.Count + CatchList.Count));
        pObj.name = string.Format("Role{0:000}", DataPlayer.pthis.MemberParty.Count + CatchList.Count);
        pObj.GetComponent<AIPlayer>().iPlayer = DataPlayer.pthis.MemberParty.Count + CatchList.Count;
        pObj.GetComponent<AIPlayer>().Init(false, iItemID, temp);

        CatchList.Add(pObj, temp);        
    }
    // ------------------------------------------------------------------
    public void SaveRole(GameObject pObj)
    {
        pPrePlayer = pObj;

		DataPlayer.pthis.MemberParty.Add(CatchList[pObj]);        
        SysMain.pthis.Role.Add(pObj, iCount);
        ToolKit.CatchRole.Add(pObj, Rule.MemberThreat(iCount));
        SysMain.pthis.SaveGame();

		iCount++;

        CatchList.Remove(pObj);
    }
    // ------------------------------------------------------------------
    public void CreateByRoad(int iRoad)
    {
        iCount = 0;

        if (DataPlayer.pthis.MemberParty.Count <= 0)
            return;

        for (int i = 0; i < DataPlayer.pthis.MemberParty.Count; i++)
        {
            // 建立第一個玩家.
            pPrePlayer = UITool.pthis.CreateUI(gameObject, "Prefab/G_Player");
            pPrePlayer.name = string.Format("Role{0:000}", iCount);
            pPrePlayer.GetComponent<AIPlayer>().iPlayer = iCount;
            pPrePlayer.GetComponent<AIPlayer>().Init(true, 0, DataPlayer.pthis.MemberParty[iCount]);
            pPrePlayer.transform.position = MapCreater.pthis.GetRoadObj(iRoad - i).transform.position;
            
            // 加入玩家佇列.
            SysMain.pthis.Role.Add(pPrePlayer, iCount);
            ToolKit.CatchRole.Add(pPrePlayer, Rule.MemberThreat(iCount));
            iCount++;
        }            
    }
    // ------------------------------------------------------------------
    void Create()
    {
        if (DataPlayer.pthis.MemberParty.Count <= 0)
            return;

        // 建立第一個玩家.
        pPrePlayer = UITool.pthis.CreateUI(gameObject, "Prefab/G_Player");
        pPrePlayer.name = string.Format("Role{0:000}", iCount);
        pPrePlayer.GetComponent<AIPlayer>().iPlayer = iCount;
        pPrePlayer.GetComponent<AIPlayer>().Init(true, 0, DataPlayer.pthis.MemberParty[iCount]);

        // 加入玩家佇列.
        SysMain.pthis.Role.Add(pPrePlayer, iCount);
        ToolKit.CatchRole.Add(pPrePlayer, Rule.MemberThreat(iCount));
        iCount ++;
    }
    // ------------------------------------------------------------------
    IEnumerator WaitCreate()
    {
		while (iCount < DataPlayer.pthis.MemberParty.Count)
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
