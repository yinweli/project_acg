using UnityEngine;
using System.Collections;

public class Btn_OpenPanel : MonoBehaviour 
{
    public string PanelName = null;

    void OnClick()
    {
        SysUI.pthis.CreatePanel("Prefab/" + PanelName);
    }
}
