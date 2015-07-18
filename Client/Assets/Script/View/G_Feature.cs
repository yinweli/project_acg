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
        ObjGroup = new GameObject[PlayerData.pthis.Members.Count];
        ObjHand = new GameObject[PlayerData.pthis.Members.Count];
        iFeature = new int[PlayerData.pthis.Members.Count];
        iEquip = new int[PlayerData.pthis.Members.Count];

        // 有幾個人建幾個人.
        for(int i = 0; i < PlayerData.pthis.Members.Count; i++)
        {
            // 建立群組.
            ObjGroup[i] = UITool.pthis.CreateUI(ObjGrid, "Prefab/G_ListRole");
            ObjGroup[i].name = string.Format("Role{0:000}", i);
            ObjGroup[i].GetComponent<G_ListRole>().pInfo = pInfo;
            ObjGroup[i].GetComponent<G_ListRole>().iPlayerID = i;
            
            ObjGrid.GetComponent<UIGrid>().Reposition();

            if (!GameData.pthis.bVictory)
            {
                // 升級.
                PlayerData.pthis.Members[i].iLiveStage++;
                iFeature[i] = Rule.GainFeature(i);
                iEquip[i] = Rule.GainEquip(i);
            }
        }

		GameData.pthis.bVictory = true;

		Rule.AddDamageReset();
		Rule.CriticalStrikeReset();
        SysMain.pthis.SaveGame();

        StartCoroutine(StartGain());
    }

    IEnumerator StartGain()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < PlayerData.pthis.Members.Count; i++)
        {
            // 建立外觀.
            GameObject ObjHuman = UITool.pthis.CreateRole(ObjGroup[i], PlayerData.pthis.Members[i].iSex, PlayerData.pthis.Members[i].iLook);
            ObjHand[i] = ObjHuman.GetComponent<G_PLook>().ChangeTo2DSprite((ENUM_Weapon)PlayerData.pthis.Members[i].iEquip);

            // 顯示升級.
            ObjGroup[i].GetComponent<G_ListRole>().ShowLevelUp(PlayerData.pthis.Members[i].iLiveStage);
            yield return new WaitForSeconds(0.5f);
            ObjGroup[i].GetComponent<G_ListRole>().ChangeLevel(PlayerData.pthis.Members[i].iLiveStage + 1);

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
