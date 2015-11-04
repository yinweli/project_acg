using UnityEngine;
using System.Collections;

public class P_WeaponLvUp : MonoBehaviour
{
    public ENUM_Weapon pWeapon = ENUM_Weapon.Null;
    public UISprite S_Weapon = null;

	// Use this for initialization
	void Start () 
    {
        S_Weapon.spriteName = string.Format("ui_w{0:00}", (int)pWeapon);
        S_Weapon.MakePixelPerfect();
        S_Weapon.gameObject.transform.localScale = new Vector3(2, 2, 1);
	}
}
