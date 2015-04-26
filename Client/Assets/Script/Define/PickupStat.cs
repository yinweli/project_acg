using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class PickupStat : MonoBehaviour
{
	static public PickupStat pthis = null;

	public List<int> Init = new List<int>(); // 初始資源
	public List<int> Gain = new List<int>(); // 獲得資源
	public List<int> Used = new List<int>(); // 使用資源
	public List<int> Total = new List<int>(); // 總共資源

	void Awake()
	{
		pthis = this;
	}
	void Start()
	{
		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			Init.Add(0);
			Gain.Add(0);
			Used.Add(0);
			Total.Add(0);
		}//for
	}
	public void Reset()
	{
		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			int iValue = 0;

			switch((ENUM_Pickup)Itor)
			{
			case ENUM_Pickup.Member: iValue = PlayerData.pthis.Members.Count; break;
			case ENUM_Pickup.Currency: iValue = PlayerData.pthis.iCurrency; break;
			case ENUM_Pickup.Battery: iValue = PlayerData.pthis.Resource[(int)ENUM_Resource.Battery]; break;
			case ENUM_Pickup.LightAmmo: iValue = PlayerData.pthis.Resource[(int)ENUM_Resource.LightAmmo]; break;
			case ENUM_Pickup.HeavyAmmo: iValue = PlayerData.pthis.Resource[(int)ENUM_Resource.HeavyAmmo]; break;
			default: break;
			}//switch

			int iTotal = 0;

			foreach(Pickup ItorPickup in GameData.pthis.PickupList)
			{
				if(Itor == ItorPickup.iType)
					iTotal += ItorPickup.iCount;
			}//for

			Init[Itor] = iValue;
			Gain[Itor] = 0;
			Used[Itor] = 0;
			Total[Itor] = iTotal;
		}//for
	}
	public void Record(ENUM_Pickup emPickup, int iValue)
	{
		if(iValue == 0)
			return;

		if(iValue > 0)
			Gain[(int)emPickup] += Mathf.Abs(iValue);
		else
			Used[(int)emPickup] += Mathf.Abs(iValue);
	}
	public int ValueGap()
	{
		int iResult = 0;

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			switch((ENUM_Pickup)Itor)
			{
			case ENUM_Pickup.Member: break;
			case ENUM_Pickup.Currency: iResult += Gain[Itor] - Used[Itor]; break;
			case ENUM_Pickup.Battery: iResult += (Gain[Itor] - Used[Itor]) * GameDefine.iPriceBattery; break;
			case ENUM_Pickup.LightAmmo: iResult += (Gain[Itor] - Used[Itor]) * GameDefine.iPriceLightAmmo; break;
			case ENUM_Pickup.HeavyAmmo: iResult += (Gain[Itor] - Used[Itor]) * GameDefine.iPriceHeavyAmmo; break;
			default: break;
			}//switch
		}//for

		return iResult;
	}
}
