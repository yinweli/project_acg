using UnityEngine;
using System.Collections;

public class G_Feature : MonoBehaviour 
{
    public GameObject ObjGrid = null;
	public UIButton BtnNext = null;
    public G_Info pInfo = null;
    
    public int[] iFeature = null;
    public int[] iEquip = null;

    GameObject[] ObjGroup = null;
    GameObject[] ObjHand = null;

	void Start()
	{
		BtnNext.isEnabled = false;
	}

	public void OpenPage()
    {
        ObjGroup = new GameObject[DataPlayer.pthis.MemberParty.Count];
        ObjHand = new GameObject[DataPlayer.pthis.MemberParty.Count];
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
            
            ObjGrid.GetComponent<UIGrid>().Reposition();

            if (!DataGame.pthis.bVictory)
            {
                // 升級.
                DataPlayer.pthis.MemberParty[i].iLiveStage++;
                iFeature[i] = Rule.GainFeature(i);
				iEquip[i] = Rule.GainEquip(i);
            }
        }

		DataGame.pthis.bVictory = true;

		Rule.AddDamageReset();
		Rule.CriticalStrikeReset();
        SysMain.pthis.SaveGame();

        StartCoroutine(StartGain());
    }

    IEnumerator StartGain()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < DataPlayer.pthis.MemberParty.Count; i++)
        {
            // 建立外觀.
            GameObject ObjHuman = UITool.pthis.CreateRole(ObjGroup[i], DataPlayer.pthis.MemberParty[i].iLooks);
            ObjHand[i] = ObjHuman.AddComponent<G_PLook>().ChangeTo2DSprite((ENUM_Weapon)DataPlayer.pthis.MemberParty[i].iEquip);

            // 顯示升級.
            NGUITools.PlaySound(Resources.Load("Sound/FX/LevelUp") as AudioClip);
            ObjGroup[i].GetComponent<G_ListRole>().ShowLevelUp(DataPlayer.pthis.MemberParty[i].iLiveStage);
            yield return new WaitForSeconds(0.5f);
            ObjGroup[i].GetComponent<G_ListRole>().ChangeLevel(DataPlayer.pthis.MemberParty[i].iLiveStage + 1);

            // 給予角色取得的天賦.
            if (iFeature[i] != 0)
            {
                yield return new WaitForSeconds(0.5f);
                ObjGroup[i].GetComponent<G_ListRole>().ShowFeature(iFeature[i]);
            }

            // 給予角色取得的武器.
            if (iEquip[i] != 0)
            {                
                yield return new WaitForSeconds(0.5f);
                ObjGroup[i].GetComponent<G_ListRole>().ShowEquip(ObjHand[i], iEquip[i]);
            }
            yield return new WaitForSeconds(0.5f);
        }

		BtnNext.isEnabled = true;
    }
}
