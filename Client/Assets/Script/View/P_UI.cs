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
    public GameObject ObjBomb = null;

    public BtnRun pBtn = null;
    public G_Stamina pSta = null;
    public UILabel pLbDayNum = null;
    public UILabel pLbDis = null;
    public UILabel pLbCurrency = null;
    public UISprite[] pSBattery = new UISprite[5];
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
    public void SetAlpha(float fValue)
    {
        GetComponent<UIPanel>().alpha = fValue;
    }
    // ------------------------------------------------------------------
    void Update()
    {
        pLbDis.text = string.Format("{0}m",(DataMap.pthis.DataRoad.Count - CameraCtrl.pthis.iNextRoad) * 10);

        if (!SysMain.pthis.bIsGaming)
            return;

        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_Pause");
            pObj.transform.localPosition = new Vector3(0, 0, -1000);
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Escape))
            Time.timeScale = 1;

        // 電源冷卻計算.
        if (fCoolDown <= Time.time && AddBattery(GameDefine.iBatteryTimeCost))
            fCoolDown = Time.time + GameDefine.fBatteryTime;
    }
    // ------------------------------------------------------------------
    public void StartNew()
    {
        // 設定關卡Title.
        pLbDayNum.text = DataPlayer.pthis.iStage.ToString();

        GetComponent<UIPanel>().alpha = 1;
		AddBattery(0);
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
    public bool UseBullet(ENUM_Weapon pType)
    {
        DBFEquip DataEquip = GameDBF.pthis.GetEquip((int)pType) as DBFEquip;

        if (DataEquip == null)
            return false;

        ENUM_Resource emResource = (ENUM_Resource)DataEquip.Resource;

        // 輕機槍升級後不再消耗子彈，改為消耗電力.
        if (pType == ENUM_Weapon.LMG && SysMain.pthis.iWLevel[(int)ENUM_Weapon.LMG] > 0)
            emResource = ENUM_Resource.Battery;

        if (Rule.ResourceChk(emResource, 0) == false)
            return false;

        Rule.ResourceAdd(emResource, -1);
        UpdateResource();
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateResource()
    {
        for (int i = 1; i < DataPlayer.pthis.Resource.Count; i++)
			if (pLbBullet[i-1])
                pLbBullet[i-1].text = DataPlayer.pthis.Resource[i].ToString();
    }
    // ------------------------------------------------------------------
    public bool AddBattery(int iValue)
    {
        if (iValue <=0 && pListLight.Count <= 0)
			return false;

		Rule.ResourceAdd(ENUM_Resource.Battery, iValue);
		UpdateBattery();
		UpdateResource();

		if(Rule.ResourceChk(ENUM_Resource.Battery, 0))
		{
			// 打開燈光
			for(int i = 0; i < pListLight.Count; i++)
				pListLight[i].gameObject.SetActive(true);

			// 判斷燈光是否需要閃爍
			if(Rule.ResourceChk(ENUM_Resource.Battery, 30))
			{
				for(int i = 0; i < pListLight.Count; i++)
					pListLight[i].pAni.Play("Wait");
			}
			else
			{
                if (iValue < 0 && DataPlayer.pthis.Resource[(int)ENUM_Resource.Battery] == 30)
                    SysMain.pthis.AllRoleTalk("Battery");
				for(int i = 0; i < pListLight.Count; i++)
					pListLight[i].pAni.Play("NoPower");
			}//if

			return true;
		}
		else
		{
			// 關閉燈光
			for (int i = 0; i < pListLight.Count; i++)
				pListLight[i].gameObject.SetActive(false);

			return false;
		}//if
    }
    // ------------------------------------------------------------------
    public void UpdateBattery()
    {
        // 先關掉.
        for (int i = 0; i < pSBattery.Length; i++)
            pSBattery[i].gameObject.SetActive(false);

        if (DataPlayer.pthis.Resource[(int)ENUM_Resource.Battery] <= 0)
            return;

        int iActive = DataPlayer.pthis.Resource[(int)ENUM_Resource.Battery] / (GameDefine.iMaxBattery / 5);

        if (DataPlayer.pthis.Resource[(int)ENUM_Resource.Battery] > 0)
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
        if(iValue < 0 && DataPlayer.pthis.iCurrency + iValue < 0)
            return false;

        Rule.CurrencyAdd(iValue);
        UpdateCurrency();
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateCurrency()
    {
        pLbCurrency.text = DataPlayer.pthis.iCurrency.ToString();
    }
    // ------------------------------------------------------------------
    public void UpdateStamina()
    {
        if (pSta)
            pSta.UpdateStamina();
    }
    // ------------------------------------------------------------------
}
