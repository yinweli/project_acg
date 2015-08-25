using UnityEngine;
using System.Collections;

public class G_Upgrade : MonoBehaviour 
{
    public ENUM_Weapon pWeapon = ENUM_Weapon.Null;

    public UISprite pS_Icon;
    public UISprite pS_AbilityBg;
    public UISprite[] pS_Collection = new UISprite[5];
    public UILabel[] pLb_Collection = new UILabel[5];

	public UILabel pLb_Name;
	public UILabel pLb_Lv;
    public UILabel pLb_Desc;
	public UILabel pLb_EffectNow;
	public UILabel pLb_EffectNext;

	void Start()
	{
		Refresh();
	}
	public void Refresh()
	{
		int iLevel = Mathf.Max(Mathf.Min(Rule.GetWeaponLevel(pWeapon), GameDefine.iMaxCollectionLv), 0);
		int iLevelNext = iLevel + 1;

		pLb_Name.text = GameDBF.pthis.GetLanguage(4000 + (int)pWeapon);
		pLb_Lv.text = iLevel.ToString();
		pLb_Desc.text = GameDBF.pthis.GetLanguage(5000 + (int)pWeapon);
		pLb_EffectNow.text = "";
		pLb_EffectNext.text = "";

		string szHelp = GameDBF.pthis.GetLanguage(6000 + (int)pWeapon);
		
		if(pWeapon == ENUM_Weapon.Light)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponLight(iLevel).Item1);
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponLight(iLevelNext).Item1);
		}//if
		
		if(pWeapon == ENUM_Weapon.Knife)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponKnife(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponKnife(iLevelNext));
		}//if
		
		if(pWeapon == ENUM_Weapon.Pistol)
		{
			int iValue = (int)((1.0f - Rule.UpgradeWeaponPistol(iLevel)) * 100.0f);
			int iValueNext = (int)((1.0f - Rule.UpgradeWeaponPistol(iLevelNext)) * 100.0f);
			
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, iValue);
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, iValueNext);
		}//if
		
		if(pWeapon == ENUM_Weapon.Revolver)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponRevolver(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponRevolver(iLevelNext));
		}//if
		
		if(pWeapon == ENUM_Weapon.Eagle)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponEagle(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponEagle(iLevelNext));
		}//if
		
		if(pWeapon == ENUM_Weapon.SUB)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponSUB(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponSUB(iLevelNext));
		}//if
		
		if(pWeapon == ENUM_Weapon.Rifle)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponRifle(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponRifle(iLevelNext));
		}//if
		
		if(pWeapon == ENUM_Weapon.LMG)
		{
			if(iLevel > 0)
				pLb_EffectNow.text = string.Format("Effect\n" + szHelp, Rule.UpgradeWeaponLMG(iLevel));
			
			if(iLevelNext <= GameDefine.iMaxCollectionLv)
				pLb_EffectNext.text = string.Format("Next Level\n" + szHelp, Rule.UpgradeWeaponLMG(iLevelNext));
		}//if
	}
}
