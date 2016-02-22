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
        if (DataPlayer.pthis.iCurrency < GameDefine.iHeavyAmmoCost)
            GetComponent<UIButtonScale>().enabled = false;
        else
            GetComponent<UIButtonScale>().enabled = true;
    }

    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (DataPlayer.pthis.iCurrency < GameDefine.iHeavyAmmoCost)
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

		GoogleAnalyticsV3.getInstance().LogEvent("Count", "Buy HeavyAmmo", "", 0);

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        DataPlayer.pthis.iCurrency -= GameDefine.iHeavyAmmoCost;
		Rule.HeavyAmmoAdd(GameDefine.iHeavyAmmoCount);
        P_UI.pthis.UpdateCurrency();
        P_UI.pthis.UpdateResource();
        DataPlayer.pthis.Save();
    }

    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}
