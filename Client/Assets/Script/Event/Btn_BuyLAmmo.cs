using UnityEngine;
using System.Collections;

public class Btn_BuyLAmmo : MonoBehaviour 
{
    public UILabel LbMoney;
    public UILabel LbCount;
    void Start()
    {
        LbMoney.text = GameDefine.iLightAmmoCost.ToString();
        LbCount.text = "x" + GameDefine.iLightAmmoCount;
    }

    void Update()
    {
        if (DataPlayer.pthis.iCurrency < GameDefine.iLightAmmoCost)
            GetComponent<UIButtonScale>().enabled = false;
        else
            GetComponent<UIButtonScale>().enabled = true;
    }

    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (DataPlayer.pthis.iCurrency < GameDefine.iLightAmmoCost)
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

		GoogleAnalyticsV3.getInstance().LogEvent("Count", "Buy LightAmmo", "", 0);

        NGUITools.PlaySound(P_Victory.pthis.Clip_Buy);
        DataPlayer.pthis.iCurrency -= GameDefine.iLightAmmoCost;
		Rule.LightAmmoAdd(GameDefine.iLightAmmoCount);
        P_UI.pthis.UpdateCurrency();
        P_UI.pthis.UpdateResource();
        DataPlayer.pthis.Save();
    }

    public void PlaySound()
    {
        NGUITools.PlaySound(P_Victory.pthis.Clip_CantBuy);
    }
}
