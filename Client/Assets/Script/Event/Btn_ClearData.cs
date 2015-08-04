using UnityEngine;
using System.Collections;

public class Btn_ClearData : MonoBehaviour {

	void OnClick()
    {
        DataPlayer.pthis.ClearSave();
        DataGame.pthis.ClearSave();
        SysMain.pthis.NewRoleData();
    }
}
