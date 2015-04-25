using UnityEngine;
using System.Collections;

public class Btn_ClearData : MonoBehaviour {

	void OnClick()
    {
        PlayerData.pthis.Clear();
        GameData.pthis.Clear();
        SysMain.pthis.NewRoleData();
    }
}
