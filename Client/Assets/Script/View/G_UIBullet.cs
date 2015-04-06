using UnityEngine;
using System.Collections;

public class G_UIBullet : MonoBehaviour 
{
    static public G_UIBullet pthis = null;

    public UILabel[] pLbBullet = new UILabel[GameDefine.iBulletType];

    void Awake () 
    {
	    pthis = this;
	}

	// Use this for initialization
	void Start () 
    {
        //for (int i = 0; i < (int)GameDefine.iBulletType; i++)
            //pLbBullet[i].text = SysMain.pthis.Data.iAmmo[i];
	}

    public bool UseBullet(WeaponType pType)
    {
		DBFEquip DataEquip = GameDBF.This.GetEquip((int)pType) as DBFEquip;

		if(DataEquip == null)
			return false;

		ENUM_Resource emResource = (ENUM_Resource)DataEquip.Resource;

		if(Rule.ResourceChk(emResource, 1) == false)
			return false;

		Rule.ResourceAdd(emResource, -1);

		return true;
    }
}
