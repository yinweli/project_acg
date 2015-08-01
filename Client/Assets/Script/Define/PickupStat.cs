using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PickupData
{
	public int iInitial = 0; // 初始值
	public int iAvailable = 0; // 可獲得值
	public int iObtain = 0; // 已獲得值
	public int iUsed = 0; // 使用值
}

public class PickupStat : MonoBehaviour
{
	static public PickupStat pthis = null;

	public List<PickupData> Data = new List<PickupData>(); // 資源列表

	void Awake()
	{
		pthis = this;
	}
	public void Reset()
	{
		Data.Clear();

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			PickupData Temp = new PickupData();

			switch((ENUM_Pickup)Itor)
			{
			case ENUM_Pickup.Member: Temp.iInitial = DataPlayer.pthis.Members.Count; break;
			case ENUM_Pickup.Currency: Temp.iInitial = DataPlayer.pthis.iCurrency; break;
			case ENUM_Pickup.Battery: Temp.iInitial = DataPlayer.pthis.Resource[(int)ENUM_Resource.Battery]; break;
			case ENUM_Pickup.LightAmmo: Temp.iInitial = DataPlayer.pthis.Resource[(int)ENUM_Resource.LightAmmo]; break;
			case ENUM_Pickup.HeavyAmmo: Temp.iInitial = DataPlayer.pthis.Resource[(int)ENUM_Resource.HeavyAmmo]; break;
			case ENUM_Pickup.Bomb: Temp.iInitial = DataPlayer.pthis.iBomb; break;
			default: break;
			}//switch

			foreach(Pickup ItorPickup in DataGame.pthis.PickupList)
			{
				if(Itor == ItorPickup.iType)
					Temp.iAvailable += ItorPickup.iCount;
			}//for

			Data.Add(Temp);
		}//for
	}
	public void Record(ENUM_Pickup emPickup, int iValue)
	{
		if(iValue == 0)
			return;

		int iPos = (int)emPickup;

		if(iPos >= Data.Count)
			return;

		if(iValue > 0)
			Data[iPos].iObtain += Mathf.Abs(iValue);
		else
			Data[iPos].iUsed += Mathf.Abs(iValue);
	}
	public void Report()
	{
		string szReport = string.Format("Day{0} [{1}]\n", DataPlayer.pthis.iStage, System.DateTime.Now);

		szReport += string.Format("Name, Initial, Available, Obtain, Used\n");

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
			szReport += string.Format("{0}, {1}, {2}, {3}, {4}\n", (ENUM_Pickup)Itor, Data[Itor].iInitial, Data[Itor].iAvailable, Data[Itor].iObtain, Data[Itor].iUsed);

		Debug.Log(szReport);
	}
}
