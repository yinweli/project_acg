using UnityEngine;
using System.Collections;

public class G_RandRole : MonoBehaviour 
{
    public GameObject[] pObjRole = new GameObject[3];
    public GameObject[] pObjRoleChild = new GameObject[3];

    public int iRightIndex = 0;
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

        iRightIndex = Random.Range(0, 3);

        // 骰個三人組
        for (int i = 0; i < 3; i++)
        {
            if (i == iRightIndex)
                DataPlayer.pthis.MemberDepot.Add(CreateNewMember(i));
            else
                CreateNewMember(i);
        }        
    }
    // ------------------------------------------------------------------
    Member CreateNewMember(int iIndex)
    {
		bool bLight = false;
		
		foreach(Member ItorMember in DataPlayer.pthis.MemberParty)
		{
			DBFEquip Data = GameDBF.pthis.GetEquip(ItorMember.iEquip) as DBFEquip;
			
			if(Data != null && Data.Mode == (int)ENUM_ModeEquip.Light)
				bLight = true;
		}//for

        Member ptempMember = new Member();
        // 角色外觀.
        ptempMember.iLooks = Rule.RandomMemberLooks();
        // 裝備.
		ptempMember.iEquip = Rule.RandomEquipParty(bLight == false, 0);
        // 角色反應時間.
        ptempMember.fReactTime = Random.Range(0.00f, 0.20f);

        pObjRoleChild[iIndex] = P_AddMember.pthis.CreateRole(pObjRole[iIndex], ptempMember);

        return ptempMember;
    }
    // ------------------------------------------------------------------
    public void ClearRand()
    {
        for (int i = 0; i < pObjRoleChild.Length; i++)
        {
            Destroy(pObjRoleChild[i]);
            pObjRole[i].SetActive(true);
        }        
    }   
}
