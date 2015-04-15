﻿using UnityEngine;
using System.Collections;

public class Btn_BuyLAmmo : MonoBehaviour 
{
    void Update()
    {
        if (PlayerData.pthis.iCurrency < GameDefine.iLightAmmoCost)
            GetComponent<UIButtonScale>().enabled = false;
    }

    void OnClick()
    {
        // 檢查金錢是否足夠.
        if (PlayerData.pthis.iCurrency < GameDefine.iLightAmmoCost)
        {
            // 錢不夠要表演叭叭.
            GetComponent<Animator>().Play("CantBuy");
            return;
        }

        NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
        PlayerData.pthis.iCurrency -= GameDefine.iLightAmmoCost;
        PlayerData.pthis.Resource[(int)ENUM_Resource.LightAmmo] += GameDefine.iLightAmmoCount;
        P_UI.pthis.UpdateCurrency();
        P_UI.pthis.UpdateResource();
        PlayerData.pthis.Save();
    }

    public void PlaySound()
    {
        NGUITools.PlaySound(Resources.Load("Sound/FX/CantBuy") as AudioClip);
    }
}