using UnityEngine;
using System.Collections;

public class Btn_ClearData : MonoBehaviour {

	void OnClick()
    {
        DataPlayer.pthis.Clear();
        DataGame.pthis.Clear();
        SysMain.pthis.NewRoleData();
    }
}
