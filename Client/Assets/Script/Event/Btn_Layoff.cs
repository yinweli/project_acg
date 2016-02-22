using UnityEngine;
using System.Collections;

public class Btn_Layoff : MonoBehaviour
{
    public P_DelChr pData = null;
    // ------------------------------------------------------------------
    void Update()
    {
        // 如果錢不夠按鈕要變暗.
        if (DataPlayer.pthis.iCurrency < GameDefine.iPriceLayoff)
            GetComponent<UIButton>().isEnabled = false;
    }
    // ------------------------------------------------------------------
	void OnClick()
    {
        // 檢查金錢是否足夠.
        if (DataPlayer.pthis.iCurrency < GameDefine.iPriceLayoff)
            return;

        GoogleAnalyticsV3.getInstance().LogEvent("Count", "Layoff", "", 0);

        DataPlayer.pthis.iCurrency -= GameDefine.iPriceLayoff;
        pData.pData.Layoff();

        P_UI.pthis.UpdateCurrency();
        DataPlayer.pthis.Save();
    }
}
