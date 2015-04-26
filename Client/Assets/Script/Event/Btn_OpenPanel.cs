using UnityEngine;
using System.Collections;

public class Btn_OpenPanel : MonoBehaviour 
{
    public string PanelName = null;

    void OnClick()
    {
        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/" + PanelName);
        pObj.transform.localPosition = new Vector3(0, 0, -1000);
    }
}
