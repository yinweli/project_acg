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
    public GameObject ObjCrystal = null;

    public BtnRun pBtn = null;
    public G_Stamina pSta = null;
    public UILabel pLbDayNum = null;
    public UILabel pLbDis = null;
    public UILabel pLbCurrency = null;
    public UISprite[] pSBattery = new UISprite[5];
	public UILabel pLBBattery = null;
	public UILabel pLBLightAmmo = null;
	public UILabel pLBHeavyAmmo = null;
	public UILabel pLBBomb = null;
    public UILabel pLbCrystal = null;

    public Animator pCrystalAni = null;

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

        if (ObjBomb.GetComponent<Btn_Bomb>())
            ObjBomb.GetComponent<Btn_Bomb>().ResetBomb();

        GetComponent<UIPanel>().alpha = 1;
		AddBattery(0);
        UpdateResource();
        UpdateCurrency();
        UpdateStamina();
        UpdateCrystal();
    }
    // ------------------------------------------------------------------
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
        if (pType == ENUM_Weapon.LMG && Rule.GetWeaponLevel(ENUM_Weapon.LMG) > 0)
            emResource = ENUM_Resource.Battery;

		bool bResult = false;

		switch(emResource)
		{
		case ENUM_Resource.Null:
			bResult = true;
			break;

		case ENUM_Resource.Battery:
			{
				if(Rule.BatteryChk(1))
				{
					Rule.BatteryAdd(-1);
					bResult = true;
				}//if
			}
			break;

		case ENUM_Resource.LightAmmo:
			{
				if(Rule.LightAmmoChk(1))
				{
					Rule.LightAmmoAdd(-1);
					bResult = true;
				}//if
			}
			break;

		case ENUM_Resource.HeavyAmmo:
			{
				if(Rule.HeavyAmmoChk(1))
				{
					Rule.HeavyAmmoAdd(-1);
					bResult = true;
				}//if
			}
			break;

		default:
			break;
		}//switch

		if(bResult)
			UpdateResource();

		return bResult;
    }
    // ------------------------------------------------------------------
    public void UpdateResource()
    {
		pLBBattery.text = DataPlayer.pthis.iBattery.ToString();
		pLBLightAmmo.text = DataPlayer.pthis.iLightAmmo.ToString();
		pLBHeavyAmmo.text = DataPlayer.pthis.iHeavyAmmo.ToString();
		pLBBomb.text = DataPlayer.pthis.iBomb.ToString();
    }
    // ------------------------------------------------------------------
    public bool AddBattery(int iValue)
    {
        if (iValue < 0 && pListLight.Count <= 0)
			return false;

		Rule.BatteryAdd(iValue);
		UpdateBattery();
		UpdateResource();

		if(Rule.BatteryChk(1))
		{
			// 打開燈光
			for(int i = 0; i < pListLight.Count; i++)
				pListLight[i].gameObject.SetActive(true);

			// 判斷燈光是否需要閃爍
			if(Rule.BatteryChk(30))
			{
				for(int i = 0; i < pListLight.Count; i++)
					pListLight[i].pAni.Play("Wait");
			}
			else
			{
                if (iValue < 0 && DataPlayer.pthis.iBattery == 29)
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

        if (DataPlayer.pthis.iBattery <= 0)
            return;

		int iActive = DataPlayer.pthis.iBattery / (GameDefine.iMaxBattery / 5);

		if (DataPlayer.pthis.iBattery > 0)
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
    public bool AddCrystal(int iValue)
    {
        if (iValue < 0 && DataReward.pthis.iCrystal + iValue < 0)
            return false;

        Rule.CrystalAdd(iValue);
        UpdateCrystal();
        return true;
    }
    // ------------------------------------------------------------------
    public void UpdateCurrency()
    {
        pLbCurrency.text = DataPlayer.pthis.iCurrency.ToString();
    }
    // ------------------------------------------------------------------
    public void UpdateCrystal()
    {
        pLbCrystal.text = DataReward.pthis.iCrystal.ToString();
    }
    // ------------------------------------------------------------------
    public void UpdateStamina()
    {
        if (pSta)
            pSta.UpdateStamina();
    }
    // ------------------------------------------------------------------
    public void ShowCrystal()
    {
        pCrystalAni.Play("ShowCrystal", -1, 0f);
    }
}
