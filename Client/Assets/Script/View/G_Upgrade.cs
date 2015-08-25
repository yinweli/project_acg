using UnityEngine;
using System.Collections;

public class G_Upgrade : MonoBehaviour 
{
    public ENUM_Weapon pWeapon = ENUM_Weapon.Null;

    public UISprite pS_Icon;
    public UILabel pLb_Lv;

    public UILabel pLb_Ability;
    public UISprite pS_AbilityBg;

    public UISprite[] pS_Collection = new UISprite[5];
    public UILabel[] pLb_Collection = new UILabel[5];

    public UILabel pLb_Src;

	// Use this for initialization
	void Start()
	{
		int iLevel = Mathf.Max(Mathf.Min(Rule.GetWeaponLevel(pWeapon), GameDefine.iMaxCollectionLv), 0);
		int iLevelNext = iLevel + 1;
		string szNote = GameDBF.pthis.GetLanguage(5000 + (int)pWeapon);
		string szHelp = GameDBF.pthis.GetLanguage(6000 + (int)pWeapon);
		string szResult = "";

		pLb_Lv.text = iLevel.ToString();
		pLb_Ability.text = GameDBF.pthis.GetLanguage(4000 + (int)pWeapon);

		if(pWeapon == ENUM_Weapon.Light)
		{
			szResult = szNote;

			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponLight(iLevel).Item1);

			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponLight(iLevelNext).Item1);
		}//if

		if(pWeapon == ENUM_Weapon.Knife)
		{
			szResult = szNote;
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponKnife(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponKnife(iLevelNext));
		}//if

		if(pWeapon == ENUM_Weapon.Pistol)
		{
			szResult = szNote;

			int iValue = (int)((1.0f - Rule.UpgradeWeaponPistol(iLevel)) * 100.0f);
			int iValueNext = (int)((1.0f - Rule.UpgradeWeaponPistol(iLevelNext)) * 100.0f);
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, iValue);
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", iValueNext);
		}//if

		if(pWeapon == ENUM_Weapon.Revolver)
		{
			szResult = szNote;
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponRevolver(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponRevolver(iLevelNext));
		}//if

		if(pWeapon == ENUM_Weapon.Eagle)
		{
			szResult = szNote;
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponEagle(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponEagle(iLevelNext));
		}//if

		if(pWeapon == ENUM_Weapon.SUB)
		{
			szResult = szNote;
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponSUB(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponSUB(iLevelNext));
		}//if

		if(pWeapon == ENUM_Weapon.Rifle)
		{
			szResult = szNote;
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponRifle(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponRifle(iLevelNext));
		}//if

		if(pWeapon == ENUM_Weapon.LMG)
		{
			szResult = szNote;
			
			if(iLevel > 0)
				szResult += string.Format("\nEffect\n" + szHelp, Rule.UpgradeWeaponLMG(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				szResult += string.Format("\nNext Level\n[b2b2b2ff]" + szHelp + "[-]", Rule.UpgradeWeaponLMG(iLevelNext));
		}//if

		pLb_Src.text = szResult;
	}
}
