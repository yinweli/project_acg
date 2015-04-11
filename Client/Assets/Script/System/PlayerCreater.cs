using UnityEngine;
using System.Collections;

public class PlayerCreater : MonoBehaviour 
{
    static public PlayerCreater pthis = null;

    public int iCount = 0;

    public GameObject pPrePlayer;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
	public void StartNew() 
    {
        Create();
        StartCoroutine(WaitCreate());
	}
    // ------------------------------------------------------------------
    // 接關時建角色用.
    void CreateAll()
    {

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
        pPrePlayer.GetComponent<G_Player>().iPlayer = iCount;
        SysMain.pthis.Role.Add(pPrePlayer, iCount);
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
