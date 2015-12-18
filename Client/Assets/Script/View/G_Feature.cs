using UnityEngine;
using System.Collections;

public class G_Feature : MonoBehaviour 
{
    public GameObject ObjGrid = null;
	public UIButton BtnNext = null;
    public G_Info pInfo = null;
    
    public int[] iFeature = null;
    public int[] iEquip = null;

    public GameObject[] ObjGroup = null;
    GameObject ObjAddMember;
    // ------------------------------------------------------------------
    void Awake()
    {
        ObjGroup = new GameObject[GameDefine.iMaxMemberParty];
    }
    // ------------------------------------------------------------------
	void Start()
	{        
		BtnNext.isEnabled = false;
	}
    // ------------------------------------------------------------------
	public void OpenPage()
    {
        iFeature = new int[DataPlayer.pthis.MemberParty.Count];
        iEquip = new int[DataPlayer.pthis.MemberParty.Count];

        // 有幾個人建幾個人.
        for(int i = 0; i < DataPlayer.pthis.MemberParty.Count; i++)
        {
            // 建立群組.
            ObjGroup[i] = UITool.pthis.CreateUI(ObjGrid, "Prefab/G_ListRole");
            ObjGroup[i].name = string.Format("Role{0:000}", i);
            ObjGroup[i].GetComponent<G_ListRole>().pInfo = pInfo;
            ObjGroup[i].GetComponent<G_ListRole>().iPlayerID = i;

            if (!DataGame.pthis.bVictory)
            {
                // 升級.
                DataPlayer.pthis.MemberParty[i].iLiveStage++;
                iFeature[i] = Rule.GainFeature(i);
				iEquip[i] = Rule.GainEquip(i);
            }
        }

        RefreshMember();        

        ObjGrid.GetComponent<UIGrid>().Reposition();

		DataGame.pthis.bVictory = true;

		Rule.AddDamageReset();
		Rule.CriticalStrikeReset();
        SysMain.pthis.SaveGame();

        StartCoroutine(StartGain());
    }
    // ------------------------------------------------------------------
    public void AddChr(int index)
    {
        Debug.Log("index: " + index);
        ObjGroup[index] = UITool.pthis.CreateUI(ObjGrid, "Prefab/G_ListRole");
        ObjGroup[index].name = string.Format("Role{0:000}", index);
        ObjGroup[index].GetComponent<G_ListRole>().pInfo = pInfo;
        ObjGroup[index].GetComponent<G_ListRole>().iPlayerID = index;
        ObjGroup[index].GetComponent<G_ListRole>().ShowNow();

        ObjGrid.GetComponent<UIGrid>().enabled = true;

        RefreshMember();
    }
    // ------------------------------------------------------------------
    public void DelChr(int index)
    {
        Destroy(ObjGroup[index]);
        ObjGroup[index] = null;

       // 重新整理資料.
        for (int i = index; i < DataPlayer.pthis.MemberParty.Count; i++)
       {
			if (ObjGroup[i] == null && i + 1 < ObjGroup.Length)
           {
               ObjGroup[i] = ObjGroup[i + 1];
               ObjGroup[i + 1] = null;
           }

           if (ObjGroup[i] && ObjGroup[i].GetComponent<G_ListRole>())
               ObjGroup[i].GetComponent<G_ListRole>().iPlayerID = i;
       }      

        RefreshMember();
    }
    // ------------------------------------------------------------------
    public void RefreshMember()
    {
        if (ObjAddMember)
            Destroy(ObjAddMember);

        if (DataPlayer.pthis.MemberParty.Count < GameDefine.iMaxMemberParty)
        {
            ObjAddMember = UITool.pthis.CreateUI(ObjGrid, "Prefab/G_ListRole");
            ObjAddMember.name = "AddRole";
            ObjAddMember.GetComponent<G_ListRole>().iPlayerID = -1;
        }

        ObjGrid.GetComponent<UIGrid>().enabled = true;
    }
    // ------------------------------------------------------------------
    IEnumerator StartGain()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < DataPlayer.pthis.MemberParty.Count; i++)
        {
            G_ListRole pRole = ObjGroup[i].GetComponent<G_ListRole>();

            // 顯示升級.
            NGUITools.PlaySound(Resources.Load("Sound/FX/LevelUp") as AudioClip);
            pRole.ShowLevelUp(DataPlayer.pthis.MemberParty[i].iLiveStage);
            yield return new WaitForSeconds(0.35f);
            pRole.ChangeLevel(DataPlayer.pthis.MemberParty[i].iLiveStage + 1);

            // 給予角色取得的天賦.
            if (iFeature[i] != 0)
            {
                yield return new WaitForSeconds(0.5f);
                pRole.ShowFeature(iFeature[i]);
            }

            // 給予角色取得的武器.
            if (iEquip[i] != 0)
            {                
                yield return new WaitForSeconds(0.5f);
                pRole.ShowEquip(iEquip[i]);
            }                        
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < DataPlayer.pthis.MemberParty.Count; i++)
            ObjGroup[i].GetComponent<G_ListRole>().ObjInfo.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < DataPlayer.pthis.MemberParty.Count; i++)
            ObjGroup[i].GetComponent<G_ListRole>().ObjFire.SetActive(true);

		BtnNext.isEnabled = true;
    }
}
