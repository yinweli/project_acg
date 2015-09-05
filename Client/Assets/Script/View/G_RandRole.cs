using UnityEngine;
using System.Collections;

public class G_RandRole : MonoBehaviour 
{
    public GameObject[] pObjRole = new GameObject[3];
    public GameObject[] pObjRoleChild = new GameObject[3];
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 3; i++)
            pObjRole[i].SetActive(false);
	}
    // ------------------------------------------------------------------
    public void StartRand()
    {
        // 先清再建.
        ClearRand();

        Member pMember = new Member();
        // 角色外觀.
        pMember.iLooks = Rule.RandomMemberLooks();
        // 裝備.
        pMember.iEquip = Rule.RandomEquipParty(false, 0);

        DataPlayer.pthis.MemberDepot.Add(pMember);

        // 先新增在第一個角色.
        pObjRoleChild[0] = P_AddMember.pthis.CreateRole(pObjRole[0], pMember);
        pObjRole[0].SetActive(true);

        /*
        for (int i = 0; i < 3; i++)
        {
            pObjRole[i].SetActive(true);
            CreateRole(pObjRole[i]);
        }*/
    }
    // ------------------------------------------------------------------
    public void ClearRand()
    {
        for (int i = 0; i < pObjRoleChild.Length; i++)
            Destroy(pObjRoleChild[i]);
    }
   
}
