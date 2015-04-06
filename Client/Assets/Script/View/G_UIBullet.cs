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
	    
	}

    public void UseBullet(WeaponType pType)
    {

    }
}
