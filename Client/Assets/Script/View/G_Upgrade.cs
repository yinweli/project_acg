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
	void Start () {
	
	}
}
