using UnityEngine;
using System.Collections;

public class G_Info : MonoBehaviour 
{
    public G_FeatureInfo[] pFInfo = new G_FeatureInfo[6];

    public void SetInfo(int iID)
    {
        for (int i = 0; i < pFInfo.Length; i++)
            pFInfo[i].SetInfo(i, iID);
    }
}
