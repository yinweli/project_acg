using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class G_HireList : MonoBehaviour
{
    public GameObject ObjShow = null;
    public Dictionary<int,GameObject> ObjHire = new Dictionary<int,GameObject>();
    
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start ()
    {
        Refresh();

        if (DataPlayer.pthis.MemberDepot.Count > 0)
            P_AddMember.pthis.MoveSelect(0);
	}
    // ------------------------------------------------------------------
    public void Refresh()
    {
        for (int i = 0; i < DataPlayer.pthis.MemberDepot.Count; i++)
        {
            if (!ObjHire.ContainsKey(i))
            {
                ObjHire.Add(i, P_AddMember.pthis.CreateRole(gameObject, DataPlayer.pthis.MemberDepot[i]));
                ObjHire[i].transform.localPosition = new Vector3(120 * (i % 7), -145 * (i / 7), 0);
                BoxCollider2D pCollider = ObjHire[i].AddComponent<BoxCollider2D>();
                pCollider.size = new Vector2(100, 100);
                ObjHire[i].AddComponent<Btn_SelectMember>().iListID = i;
            }
        }

        if (DataPlayer.pthis.MemberDepot.Count <= GameDefine.iMaxMemberDepot)
        {
            ObjShow.SetActive(true);
            ObjShow.transform.localPosition = new Vector3(120 * (DataPlayer.pthis.MemberDepot.Count % 7), -145 * (DataPlayer.pthis.MemberDepot.Count / 7) + 21, 0);
        }
        else
            ObjShow.SetActive(false);

        if(P_AddMember.pthis.iNowSelect == -1)
            P_AddMember.pthis.MoveSelect(0);
    }
    // ------------------------------------------------------------------
    public void PickNew()
    {
        GameObject pObj = UITool.pthis.CreateUI(ObjHire[ObjHire.Count - 1], "Prefab/S_Weapon");
        pObj.transform.localPosition = new Vector2(0,-73);

        S_Weapon pScript =pObj.GetComponent<S_Weapon>();
        if (pScript)
        {
            pScript.SetLbActive(false);
            pScript.pS_Bg.gameObject.transform.localPosition = new Vector2(0, 30);
            pScript.pLb_Src.gameObject.transform.localPosition = new Vector2(0, 30);
        }
    }
    // ------------------------------------------------------------------
    public void ClearList()
    {
        // 清除資料.
        foreach (KeyValuePair<int, GameObject> itor in ObjHire)
            Destroy(itor.Value);
        ObjHire.Clear();

        Refresh();

        if (DataPlayer.pthis.MemberDepot.Count > 0 && P_AddMember.pthis.iNowSelect < DataPlayer.pthis.MemberDepot.Count)
            P_AddMember.pthis.MoveSelect(P_AddMember.pthis.iNowSelect);
        else if (DataPlayer.pthis.MemberDepot.Count > 0 && P_AddMember.pthis.iNowSelect >= DataPlayer.pthis.MemberDepot.Count)
            P_AddMember.pthis.MoveSelect(P_AddMember.pthis.iNowSelect - 1);
    }
}
