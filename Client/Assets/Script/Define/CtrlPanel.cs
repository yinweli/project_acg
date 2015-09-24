using UnityEngine;
using System.Collections;

public class CtrlPanel : MonoBehaviour
{
	/* 提供給 Debug 版本裝特定的控制按鈕用 */

	public void ClearData()
	{
		DataEnemy.pthis.ClearSave();
		DataGame.pthis.ClearSave();
		DataPlayer.pthis.ClearSave();
		SysMain.pthis.NewRoleData();
	}
	public void ClearAchievement()
	{
		DataAchievement.pthis.ClearSave();
	}
	public void ClearCollection()
	{
		DataCollection.pthis.ClearSave();
	}
	public void ClearRecord()
	{
		DataRecord.pthis.ClearSave();
	}
	public void ClearReward()
	{
		DataReward.pthis.ClearSave();
	}
}
