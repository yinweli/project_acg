using UnityEngine;
using System.Collections;

public class Btn_BuyBomb : MonoBehaviour 
{
    public UILabel LbMoney;
    public UILabel LbCount;

    void Start()
    {
        LbMoney.text = GameDefine.iPriceBomb.ToString();
        LbCount.text = "x" + GameDefine.iBombCount;
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (PlayerData.pthis.iCurrency < GameDefine.iPriceBomb)
            GetComponent<UIButtonScale>().enabled = false;
        else
            GetComponent<UIButtonScale>().enabled = true;
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (PlayerData.pthis.iCurrency < GameDefine.iPriceBomb)
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

		GoogleAnalytics.pthis.LogEvent("Count", "Buy Bomb", "", 0);

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        PlayerData.pthis.iCurrency -= GameDefine.iPriceBomb;
		Rule.BombAdd(GameDefine.iBombCount);
        P_UI.pthis.UpdateCurrency();
        PlayerData.pthis.Save();
    }
    // ------------------------------------------------------------------
    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}
