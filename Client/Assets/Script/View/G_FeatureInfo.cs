using UnityEngine;
using System.Collections;

public class G_FeatureInfo : MonoBehaviour 
{
    public UISprite S_Icon = null;
    public UILabel Lb_Src = null;

    public void SetInfo(int iIndex, int iID)
    {
        if (iIndex >= DataPlayer.pthis.Members[iID].Feature.Count)
        {
            gameObject.SetActive(false);
            return;
        }
        else
            gameObject.SetActive(true);

        DBFFeature pDBFFeature = GameDBF.pthis.GetFeature(DataPlayer.pthis.Members[iID].Feature[iIndex]) as DBFFeature;

        S_Icon.spriteName = string.Format("ui_feature_{0:000}", DataPlayer.pthis.Members[iID].Feature[iIndex]);
        Lb_Src.text = GameDBF.pthis.GetLanguage(pDBFFeature.StrID) + "\n" + GameDBF.pthis.GetLanguage(pDBFFeature.Description);

    }
}
