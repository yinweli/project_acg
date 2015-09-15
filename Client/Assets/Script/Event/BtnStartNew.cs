using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class BtnStartNew : MonoBehaviour 
{
    public GameObject pPanel = null;

	void OnClick()
    {
        DataPlayer.pthis.iStage++;

        SysMain.pthis.NewStage();

        if (pPanel.GetComponent<P_Victory>())
            pPanel.GetComponent<P_Victory>().ClearPage();
        Destroy(pPanel);
    }
}
