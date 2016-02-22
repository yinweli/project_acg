using UnityEngine;
using System.Collections;

public class Btn_BuyDmg : MonoBehaviour
{
    public UILabel LbMoney;
    public UILabel LbCount;

    void Start()
    {
        LbMoney.text = Rule.DmgLvMoney().ToString();
        LbCount.text = "All+" + GameDefine.iDmgLvCount + "dmg";
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (DataPlayer.pthis.iCurrency < Rule.DmgLvMoney())
            GetComponent<UIButtonScale>().enabled = false;
        else
            GetComponent<UIButtonScale>().enabled = true;
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (DataPlayer.pthis.iCurrency < Rule.DmgLvMoney())
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

		GoogleAnalyticsV3.getInstance().LogEvent("Count", "Buy AddDmg", "", 0);

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        DataPlayer.pthis.iCurrency -= Rule.DmgLvMoney();
        DataPlayer.pthis.iDamageLv++;
        P_UI.pthis.UpdateCurrency();
        DataPlayer.pthis.Save();
        // 更新價格.
        LbMoney.text = Rule.DmgLvMoney().ToString();
    }
    // ------------------------------------------------------------------
    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}