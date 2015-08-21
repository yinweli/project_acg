using UnityEngine;
using System.Collections;

public class Btn_ClearData : MonoBehaviour {

	void OnClick()
    {
		DataAchievement.pthis.ClearSave();
		DataCollection.pthis.ClearSave();
		DataReward.pthis.ClearSave();
		DataEnemy.pthis.ClearSave();
		DataGame.pthis.ClearSave();
		DataPlayer.pthis.ClearSave();
        SysMain.pthis.NewRoleData();
    }
}
