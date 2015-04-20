using UnityEngine;
using System.Collections;

public class Lb_Feature : MonoBehaviour 
{
    public UILabel pLb = null;
    public UISprite pSBg = null;

    public void SetFeature(int iFeature)
    {
        // 顯示取得的特性.
        DBFFeature pDBFFeature = GameDBF.This.GetFeature(iFeature) as DBFFeature;
        DBFLanguage pDBFLanguage = GameDBF.This.GetLanguage(pDBFFeature.StrID) as DBFLanguage;

        if (GameData.pthis.Language == ENUM_Language.enUS)
            pLb.text = pDBFLanguage.enUS;
        else if (GameData.pthis.Language == ENUM_Language.zhTW)
        {
            Debug.Log(gameObject.name + " Get Feature zhTW: " + pDBFLanguage.zhTW);
            pLb.text = pDBFLanguage.zhTW;
        }

        pSBg.width = pLb.width + 10;
    }
}
