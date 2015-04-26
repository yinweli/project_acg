using UnityEngine;
using System.Collections;

public class Btn_BuyHAmmo : MonoBehaviour 
{
    public UILabel LbMoney;
    public UILabel LbCount;

    void Start()
    {
        LbMoney.text = GameDefine.iHeavyAmmoCost.ToString();
        LbCount.text = "x" + GameDefine.iHeavyAmmoCount;
    }

    void Update()
    {
        if (PlayerData.pthis.iCurrency < GameDefine.iHeavyAmmoCost)
            GetComponent<UIButtonScale>().enabled = false;
    }

    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (PlayerData.pthis.iCurrency < GameDefine.iHeavyAmmoCost)
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

        GoogleAnalytics.pthis.LogEvent("Buy", "HeavyAmmo", "Day" + PlayerData.pthis.iStage, GameDefine.iHeavyAmmoCost);

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        PlayerData.pthis.iCurrency -= GameDefine.iHeavyAmmoCost;
        //PlayerData.pthis.Resource[(int)ENUM_Resource.HeavyAmmo] += GameDefine.iHeavyAmmoCount;
		Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, GameDefine.iHeavyAmmoCount);
        P_UI.pthis.UpdateCurrency();
        P_UI.pthis.UpdateResource();
        PlayerData.pthis.Save();
    }

    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}
