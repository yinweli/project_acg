using UnityEngine;
using System.Collections;

public class G_Feature : MonoBehaviour 
{
    public GameObject ObjGrid = null;
    public Shader pShader = null;

    public int[] iFeature = null;
    public int[] iEquip = null;

    GameObject[] ObjGroup = null;
    GameObject[] ObjHand = null;

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
            
            ObjGrid.GetComponent<UIGrid>().Reposition();

            iFeature[i] = Rule.GainFeature(i);
            iEquip[i] = Rule.GainFeature(i);
        }

        PlayerData.pthis.Save();

        StartCoroutine(StartGain());
    }

    IEnumerator StartGain()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < PlayerData.pthis.Members.Count; i++)
        {
            // 建立外觀.
            GameObject ObjHuman = UITool.pthis.CreateRole(ObjGroup[i], PlayerData.pthis.Members[i].iSex, PlayerData.pthis.Members[i].iLook);
            ObjHand[i] = ObjHuman.GetComponent<G_PLook>().SetShader(pShader, (ENUM_Weapon)PlayerData.pthis.Members[i].iEquip);

            float fWaitSec = 0.5f;
            Debug.Log(ObjGroup[i].name + " Feature: " + iFeature[i] + " Equip: " + (ENUM_Weapon)iEquip[i]);
            if (iFeature[i] != 0)
            {
                ObjGroup[i].GetComponent<G_ListRole>().ShowFeature(iFeature[i]);
                fWaitSec += 0.5f;
            }

            if (iEquip[i] != 0)
            {
                // 給予角色取得的武器.
                ObjGroup[i].GetComponent<G_ListRole>().ShowEquip(ObjHand[i], iEquip[i], pShader);
                fWaitSec += 0.5f;
            }

            yield return new WaitForSeconds(fWaitSec);
        }
    }
}
