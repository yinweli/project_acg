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

    public void UseBullet(WeaponType pType)
    {
        // 從DBF中取得需要消耗資源.
    }
}
