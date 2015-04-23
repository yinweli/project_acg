using UnityEngine;
using System.Collections;

public class G_FeatureInfo : MonoBehaviour 
{
    public UISprite S_Icon = null;
    public UILabel Lb_Src = null;

    public void SetInfo(int iIndex, int iID)
    {
        if (iIndex >= PlayerData.pthis.Members[iID].Feature.Count)
        {
            gameObject.SetActive(false);
            return;
        }

        DBFFeature pDBFFeature = GameDBF.This.GetFeature(PlayerData.pthis.Members[iID].Feature[iIndex]) as DBFFeature;

        S_Icon.spriteName = string.Format("ui_feature_{0:000}", PlayerData.pthis.Members[iID].Feature[iIndex]);
        Lb_Src.text = GameDBF.This.GetLanguage(pDBFFeature.StrID) + "\n" + GameDBF.This.GetLanguage(pDBFFeature.Description);

    }
}
