using UnityEngine;
using System.Collections;

public class G_Stamina : MonoBehaviour 
{
    public UILabel pLbStamina = null;
    public UISprite[] pSStamina = new UISprite[10];
    // ------------------------------------------------------------------
    void Start()
    {
        P_UI.pthis.pSta = this;
    }
    // ------------------------------------------------------------------
    public void UpdateStamina()
    {
        pLbStamina.text = DataPlayer.pthis.iStamina.ToString() + "/" + DataPlayer.pthis.iStaminaLimit;

        for (int i = 0; i < pSStamina.Length; i++)
            pSStamina[i].gameObject.SetActive(false);

        int iActive = DataPlayer.pthis.iStaminaLimit > 0 ? (int)(DataPlayer.pthis.iStamina / ((float)DataPlayer.pthis.iStaminaLimit / pSStamina.Length)) : 0;

        if (iActive > pSStamina.Length)
            iActive = pSStamina.Length;
        else if (iActive == 0 && DataPlayer.pthis.iStamina > 0)
            iActive = 1;

        for (int i = 0; i < iActive; i++)
        {
            if (iActive <= 1)
                pSStamina[i].spriteName = "ui_com_003";
            else if (iActive <= 5)
                 pSStamina[i].spriteName = "ui_com_004";
            else
                pSStamina[i].spriteName = "ui_com_005";

            pSStamina[i].gameObject.SetActive(true);
        }
    }
}
