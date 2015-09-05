using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class G_HireList : MonoBehaviour
{
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
                ObjHire[i].transform.localPosition = new Vector3(107 * (i % 6), -155 * (i / 6), 0);
                BoxCollider2D pCollider = ObjHire[i].AddComponent<BoxCollider2D>();
                pCollider.size = new Vector2(100, 100);
                ObjHire[i].AddComponent<Btn_SelectMember>().iListID = i;
            }
        }

        if(P_AddMember.pthis.iNowSelect == -1)
            P_AddMember.pthis.MoveSelect(0);
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
