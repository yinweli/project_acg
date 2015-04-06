using UnityEngine;
using System.Collections;

public class PlayerCreater : MonoBehaviour 
{
    public int iCount = 0;

    public GameObject pPrePlayer;
	// Use this for initialization

	void Start () 
    {
        Create();
        StartCoroutine(WaitCreate());
	}

    void Create()
    {
        if (SysMain.pthis.Data.Data.Count <= 0)
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

    IEnumerator WaitCreate()
    {
        while (iCount < SysMain.pthis.Data.Data.Count)
        {
            yield return new WaitForEndOfFrame();
            // 檢查上一個玩家是否距離已到.
            if (Vector2.Distance(pPrePlayer.transform.position, MapCreater.This.GetRoadObj(0).transform.position) > 0.195f)
                Create();
        }       
    }
}
