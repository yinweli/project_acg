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
			case ENUM_Pickup.Member: Temp.iInitial = DataPlayer.pthis.MemberParty.Count; break;
			case ENUM_Pickup.Currency: Temp.iInitial = DataPlayer.pthis.iCurrency; break;
			case ENUM_Pickup.Battery: Temp.iInitial = DataPlayer.pthis.iBattery; break;
			case ENUM_Pickup.LightAmmo: Temp.iInitial = DataPlayer.pthis.iLightAmmo; break;
			case ENUM_Pickup.HeavyAmmo: Temp.iInitial = DataPlayer.pthis.iHeavyAmmo; break;
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
}
