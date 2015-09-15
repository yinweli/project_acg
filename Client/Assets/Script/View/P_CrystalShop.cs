using UnityEngine;
using System.Collections;

public class P_CrystalShop : MonoBehaviour
{
    public Btn_CrystalShop pMan;
    public UILabel pLbCrystal = null;
    public UILabel pLbTime = null;
    public UILabel pSrc = null;

    public GameObject[] ObjItem = new GameObject[2];
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < DataGame.pthis.iWeaponType.Length; i++)
        {
            ENUM_Weapon pType = (ENUM_Weapon)DataGame.pthis.iWeaponType[i];

            // 如果沒R到物品就隱藏
            if (pType == ENUM_Weapon.Null)               
                continue;

            ObjItem[i].SetActive(true);
            
            if (ObjItem[i] && ObjItem[i].GetComponent<G_WeaponItem>())
                ObjItem[i].GetComponent<G_WeaponItem>().SetInfo(pType, WeaponTransIndex(DataGame.pthis.iWeaponIndex[i]));
        }
        // 更動位置.
        if (ObjItem[0] && ObjItem[0].GetComponent<G_WeaponItem>())
            ObjItem[0].GetComponent<G_WeaponItem>().SetPos(ObjItem[1].activeSelf);

        if (ObjItem[1] && ObjItem[1].GetComponent<G_WeaponItem>())
            ObjItem[1].GetComponent<G_WeaponItem>().SetPos(ObjItem[0].activeSelf);
    }
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        if (!pMan)
        {
            Destroy(gameObject);
            return;
        }

        if (!ObjItem[0].activeSelf && !ObjItem[1].activeSelf)
            pSrc.text = "Sold Out!!!";

        pLbTime.text = string.Format("{0:00}:{1:00}", pMan.iTimeCount / 60, pMan.iTimeCount % 60);
        pLbCrystal.text = DataReward.pthis.iCrystal.ToString();
	}
    // ------------------------------------------------------------------
    public string WeaponTransIndex(int iIndex)
    {
        if (iIndex == 1)
            return "A";
        else if (iIndex == 2)
            return "B";
        else if (iIndex == 3)
            return "C";
        else if (iIndex == 4)
            return "D";
        else
            return "E";
    }
}
