using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class P_UI : MonoBehaviour 
{
    static public P_UI pthis = null;

    public int iBattery = 100;

    public GameObject ObjCurrency = null;
    public GameObject ObjBattery = null;
    public GameObject ObjAmmoLight = null;
    public GameObject ObjAmmoHeavy = null;

    public BtnRun pBtn = null;
    public UILabel pLbDayNum = null;
    public UILabel pLbDis = null;
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
        GetComponent<UIPanel>().alpha = 0;
    }
    // ------------------------------------------------------------------
    void Update()
    {
        pLbDis.text = string.Format("{0}m",(GameData.pthis.RoadList.Count - CameraCtrl.pthis.iNextRoad) * 10);

        if (!SysMain.pthis.bIsGaming)
            return;

        // 電源冷卻計算.
        if (fCoolDown <= Time.time && AddBattery(-1))
            fCoolDown = Time.time + 1.0f;
    }
    // ------------------------------------------------------------------
    public void StartNew()
    {
        // 設定關卡Title.
        pLbDayNum.text = PlayerData.pthis.iStage.ToString();

        GetComponent<UIPanel>().alpha = 1;
        UpdateResource();
        UpdateCurrency();
        UpdateStamina();
    }
    public void StartRecoverSta()
    {
        if (pBtn)
            pBtn.StartNew();
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
        for (int i = 1; i < PlayerData.pthis.Resource.Count; i++)
			if (pLbBullet[i-1])
                pLbBullet[i-1].text = PlayerData.pthis.Resource[i].ToString();
    }
    // ------------------------------------------------------------------
    public bool AddBattery(int iValue)
    {
        if (PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] + iValue < 0)
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
        if (iValue > 0 && PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] > 30)
        {
            for (int i = 0; i < pListLight.Count; i++)
                pListLight[i].pAni.Play("Wait");
        }
        // 電量低於30，燈光開始閃爍.
        else if (PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] < 30)
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
            pSBattery[i].gameObject.SetActive(false);

        if (PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] <= 0)
            return;

        int iActive = PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] / (GameDefine.iMaxBattery / 5);

        if (PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] > 0)
            iActive++;
 
        if (iActive > pSBattery.Length)
            iActive = pSBattery.Length;
        
        for (int i = 0; i < iActive; i++)
        {
            if (iActive == 1)
                pSBattery[i].spriteName = "ui_com_003";
            else
                pSBattery[i].spriteName = "ui_com_004";

            pSBattery[i].gameObject.SetActive(true);
        }
    }
    // ------------------------------------------------------------------
    public bool AddCurrency(int iValue)
    {
        if(iValue < 0 && PlayerData.pthis.iCurrency + iValue < 0)
            return false;

        Rule.CurrencyAdd(iValue);
        UpdateCurrency();
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateCurrency()
    {
        pLbCurrency.text = PlayerData.pthis.iCurrency.ToString();
    }
    // ------------------------------------------------------------------
    public void UpdateStamina()
    {
        pLbStamina.text = PlayerData.pthis.iStamina.ToString() + "/" + PlayerData.pthis.iStaminaLimit;

        for (int i = 0; i < pSStamina.Length; i++)
            pSStamina[i].gameObject.SetActive(false);

        int iActive = (PlayerData.pthis.iStamina / (PlayerData.pthis.iStaminaLimit / pSStamina.Length));

        if (iActive > pSStamina.Length)
            iActive = pSStamina.Length;
        else if (iActive == 0 && PlayerData.pthis.iStamina > 0)
            iActive = 1;

        for (int i = 0; i < iActive; i++)
        {
            if (iActive <= 2)
                pSStamina[i].spriteName = "ui_com_003";
            else
                pSStamina[i].spriteName = "ui_com_005";

            pSStamina[i].gameObject.SetActive(true);
        }
    }
    // ------------------------------------------------------------------
}
