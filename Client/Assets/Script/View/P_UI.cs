using UnityEngine;
using System.Collections;

public class P_UI : MonoBehaviour 
{
    static public P_UI pthis = null;

    public int iBattery = 100;

    public UISprite[] pSBattery = new UISprite[5];
    public UILabel[] pLbBullet = new UILabel[(int)ENUM_Resource.Resource_Count-1];

    float fCoolDown = 1;

    void Awake()
    {
        pthis = this;
    }
    void Start()
    {
        UpdateBullet();
    }

    void Update()
    {
        if (fCoolDown <= Time.time)
        {
            SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery]--;
            // 計算冷卻.
            fCoolDown = Time.time + 1.0f;
            UpdateBattery();
            UpdateBullet();
        }        
    }

    public bool UseBullet(WeaponType pType)
    {
        DBFEquip DataEquip = GameDBF.This.GetEquip((int)pType) as DBFEquip;

        if (DataEquip == null)
            return false;

        ENUM_Resource emResource = (ENUM_Resource)DataEquip.Resource;

        if (Rule.ResourceChk(emResource, 1) == false)
            return false;

        Rule.ResourceAdd(emResource, -1);
        UpdateBullet();
        return true;
    }
    public void UpdateBullet()
    {
        for (int i = 0; i < (int)ENUM_Resource.Resource_Count - 1; i++)
            if (pLbBullet[i])
                pLbBullet[i].text = SysMain.pthis.Data.Resource[i+1].ToString();
    }

    public void UpdateBattery()
    {
        // 先關掉.
        for (int i = 0; i < pSBattery.Length; i++)
            pSBattery[i].gameObject.SetActive(false);

        int iActive = (SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery] / (GameDefine.iMaxBattery / 5)) + 1;
        if (iActive > pSBattery.Length)
            iActive = pSBattery.Length;

        for (int i = 0; i < iActive; i++)
            pSBattery[i].gameObject.SetActive(true);
    }
 
}
