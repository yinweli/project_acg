using UnityEngine;
using System.Collections;

public class Btn_ClearData : MonoBehaviour {

	void OnClick()
    {
        PlayerPrefs.DeleteAll();
        SysMain.pthis.NewRoleData();
    }
}
