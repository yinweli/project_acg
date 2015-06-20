using UnityEngine;
using System.Collections;

public class Btn_BuyDmg : MonoBehaviour
{
    public UILabel LbMoney;
    public UILabel LbCount;

    void Start()
    {
        LbMoney.text = Rule.DmgLvMoney().ToString();
        LbCount.text = "All +" + GameDefine.iDmgLvCount + "dmg";
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (PlayerData.pthis.iCurrency < Rule.DmgLvMoney())
            GetComponent<UIButtonScale>().enabled = false;
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (PlayerData.pthis.iCurrency < Rule.DmgLvMoney())
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

        GoogleAnalytics.pthis.LogEvent("Buy", "Bomb", "Day" + PlayerData.pthis.iStage, Rule.DmgLvMoney());

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        PlayerData.pthis.iCurrency -= Rule.DmgLvMoney();
        PlayerData.pthis.iDamageLv++;
        P_UI.pthis.UpdateCurrency();
        PlayerData.pthis.Save();
        // 更新價格.
        LbMoney.text = Rule.DmgLvMoney().ToString();
    }
    // ------------------------------------------------------------------
    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}