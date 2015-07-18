using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolKit : MonoBehaviour 
{
    // 可抓人物佇列.
    static public Dictionary<GameObject, int> CatchRole = new Dictionary<GameObject, int>();
    // ------------------------------------------------------------------
	static public GameObject GetEnemyTarget()
    {
        if (CatchRole.Count <= 0)
            return null;

        int iTotal = 0;

        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
            iTotal += itor.Value;

        int iPick = Random.Range(0, iTotal);

        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
        {
            if (iPick < itor.Value)
                return itor.Key;

            iPick -= itor.Value;
        }
        return null;
    }
    // ------------------------------------------------------------------
    static public void ClearCatchRole()
    {
        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
            Destroy(itor.Key);

        CatchRole.Clear();
    }
    // ------------------------------------------------------------------
    static public void SetLayer(int iLayer, SpriteRenderer[] pSprite)
    {
        // 切換layer.
        for (int i = 0; i < pSprite.Length; i++)
            pSprite[i].sortingOrder = pSprite[i].sortingOrder + (iLayer * 30);
    }
    // ------------------------------------------------------------------
    static public void MoveTo(GameObject pObj, Vector3 vecDirection, float fSpeed)
    {
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        pObj.transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
    // ------------------------------------------------------------------
}
