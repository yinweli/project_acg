using UnityEngine;
using System.Collections;

public class Btn_CrystalBuy : MonoBehaviour
{
    public int iIndex = 0;
    public UILabel pMoney = null;

    // ------------------------------------------------------------------
    void Start()
    {
        pMoney.text = GameDefine.iPriceWeaponItem.ToString();

        // 水晶不足要block按鈕.
        if (GetComponent<UIButton>() && DataReward.pthis.iCrystal < GameDefine.iPriceWeaponItem)
            GetComponent<UIButton>().isEnabled = false;
    }
    // ------------------------------------------------------------------
	void OnClick()
    {
        // 水晶不足.
        if (DataReward.pthis.iCrystal < GameDefine.iPriceWeaponItem)
            return;

        if(iIndex >= DataGame.pthis.iWeaponType.Length || DataGame.pthis.iWeaponType[iIndex] == (int)ENUM_Weapon.Null)
            return;
        
        ENUM_Weapon pType = (ENUM_Weapon)DataGame.pthis.iWeaponType[iIndex];
        int iLevel = Rule.GetWeaponLevel(pType) + 1;
        int iPos = DataGame.pthis.iWeaponIndex[iIndex];

        // 如果已經有這個東西了.
        if (DataCollection.pthis.IsExist(pType, iLevel, iPos) && GetComponent<UIButton>())
        {
            GetComponent<UIButton>().isEnabled = false;
            return;
        }

        DataReward.pthis.iCrystal -= GameDefine.iPriceWeaponItem;

        DataCollection.pthis.Add(pType, iLevel, iPos);

        if (GetComponent<UIButton>())
            GetComponent<UIButton>().isEnabled = false;

        DataGame.pthis.iWeaponType[iIndex] = (int)ENUM_Weapon.Null;

        DataReward.pthis.Save();
        DataCollection.pthis.Save();
        DataGame.pthis.Save();
    }
}
