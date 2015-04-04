using UnityEngine;
using System.Collections;

public class G_Player : MonoBehaviour 
{
    public Sprite Weapon01;
    public Sprite Weapon02;
    public Sprite Weapon03;

    public UI2DSprite pSBody;
    public UI2DSprite pSFootR;
    public UI2DSprite pSFootL;

    public GameObject pSWeapon;

    public void InitPlayer(WeaponType pType)
    {
        if (pType == WeaponType.Weapon_null)
            pSWeapon.SetActive(false);
        else if (pType == WeaponType.Weapon_Light)
            pSWeapon.GetComponent<UI2DSprite>().sprite2D = Weapon01;
        else if (pType == WeaponType.Weapon_001)
            pSWeapon.GetComponent<UI2DSprite>().sprite2D = Weapon02;
        else if (pType == WeaponType.Weapon_002)
            pSWeapon.GetComponent<UI2DSprite>().sprite2D = Weapon03;

    }
}
