using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class P_UI : MonoBehaviour 
{
    static public P_UI pthis = null;

    public int iBattery = 100;

    public UILabel pLbCurrency = null;
    public UILabel pLbStamina = null;
    public UISprite[] pSBattery = new UISprite[5];
    public UISprite[] pSStamina = new UISprite[10];
    public UILabel[] pLbBullet = new UILabel[(int)ENUM_Resource.Resource_Count-1];
    public List<G_Light> pListLight = new List<G_Light>();

    float fCoolDown = 1;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    void Start()
    {
        UpdateResource();
        UpdateCurrency();
        UpdateStamina();
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        // 電源冷卻計算.
        if (fCoolDown <= Time.time && AddBattery(-1))
            fCoolDown = Time.time + 1.0f;
    }
    // ------------------------------------------------------------------
    public bool UseBullet(WeaponType pType)
    {
        DBFEquip DataEquip = GameDBF.This.GetEquip((int)pType) as DBFEquip;

        if (DataEquip == null)
            return false;

        ENUM_Resource emResource = (ENUM_Resource)DataEquip.Resource;

        if (Rule.ResourceChk(emResource, 1) == false)
            return false;

        Rule.ResourceAdd(emResource, -1);
        UpdateResource();
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateResource()
    {
        for (int i = 0; i < (int)ENUM_Resource.Resource_Count - 1; i++)
            if (pLbBullet[i])
                pLbBullet[i].text = SysMain.pthis.Data.Resource[i+1].ToString();
    }

    // ------------------------------------------------------------------
    public bool AddBattery(int iValue)
    {
        float fBattery = SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery];
        float fMaxBattery = GameDefine.iMaxBattery;

        if (SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery] + iValue < 0)
        {
            // 沒電時關掉燈光.
            for (int i = 0; i < pListLight.Count; i++)
                pListLight[i].gameObject.SetActive(false);
            return false;
        }

        Rule.ResourceAdd(ENUM_Resource.Battery, iValue);
        UpdateBattery();
        UpdateResource();

        if (iValue > 0)
        {
            for (int i = 0; i < pListLight.Count; i++)
                pListLight[i].gameObject.SetActive(true);
        }

        // 增加電量時要確認是否需要閃爍.
        if (iValue > 0 && fBattery / fMaxBattery >= 0.005f)
        {
            for (int i = 0; i < pListLight.Count; i++)
                pListLight[i].pAni.Play("Wait");
        }
        // 電量低於5%，燈光開始閃爍.
        else if (fBattery / fMaxBattery < 0.005f)
        {
            for (int i = 0; i < pListLight.Count; i++)
                pListLight[i].pAni.Play("NoPower");
        }
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateBattery()
    {
        // 先關掉.
        for (int i = 0; i < pSBattery.Length; i++)
        {
            pSBattery[i].spriteName = "ui_com_004";
            pSBattery[i].gameObject.SetActive(false);
        }

        if (SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery] <= 0)
            return;

        int iActive = (SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery] / (GameDefine.iMaxBattery / 5));

        if (iActive > pSBattery.Length)
            iActive = pSBattery.Length;
        else if (iActive == 0 && SysMain.pthis.Data.Resource[(int)ENUM_Resource.Battery] > 0)
            iActive = 1;

        for (int i = 0; i < iActive; i++)
        {
            if (iActive == 1)
                pSBattery[i].spriteName = "ui_com_003";

            pSBattery[i].gameObject.SetActive(true);
        }
    }
    // ------------------------------------------------------------------
    public bool AddCurrency(int iValue)
    {
        if(iValue < 0 && SysMain.pthis.Data.iCurrency + iValue < 0)
            return false;

        Rule.CurrencyAdd(iValue);
        UpdateCurrency();
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateCurrency()
    {
        pLbCurrency.text = SysMain.pthis.Data.iCurrency.ToString();
    }
    // ------------------------------------------------------------------
    public void UpdateStamina()
    {
        pLbStamina.text = SysMain.pthis.Data.iStamina.ToString() + "/" + SysMain.pthis.Data.iStaminaLimit;

        for (int i = 0; i < pSStamina.Length; i++)
            pSStamina[i].gameObject.SetActive(false);

        int iActive = (SysMain.pthis.Data.iStamina / (SysMain.pthis.Data.iStaminaLimit / pSStamina.Length));

        if (iActive > pSStamina.Length)
            iActive = pSStamina.Length;
        else if (iActive == 0 && SysMain.pthis.Data.iStamina > 0)
            iActive = 1;

        for (int i = 0; i < iActive; i++)
            pSStamina[i].gameObject.SetActive(true);
    }
    // ------------------------------------------------------------------
}
