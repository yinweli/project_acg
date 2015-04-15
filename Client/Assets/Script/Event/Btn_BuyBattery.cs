using UnityEngine;
using System.Collections;

public class Btn_BuyBattery : MonoBehaviour 
{
    void Update()
    {
        if (PlayerData.pthis.iCurrency < GameDefine.iBatteryCost)
            GetComponent<UIButtonScale>().enabled = false;
    }

    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (PlayerData.pthis.iCurrency < GameDefine.iBatteryCost)
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        PlayerData.pthis.iCurrency -= GameDefine.iBatteryCost;
        PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] += GameDefine.iBatteryCount;
        P_UI.pthis.UpdateCurrency();
        P_UI.pthis.UpdateResource();
        P_UI.pthis.UpdateBattery();
        PlayerData.pthis.Save();
    }

    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}

