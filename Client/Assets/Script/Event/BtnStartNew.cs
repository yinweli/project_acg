using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class BtnStartNew : MonoBehaviour 
{
    public GameObject pPanel = null;

	void OnClick()
    {
        PlayerData.pthis.iStage++;

        SysMain.pthis.NewGame();
        Destroy(pPanel);
    }
}
