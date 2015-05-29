using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PickupStat : MonoBehaviour
{
	static public PickupStat pthis = null;

	public bool bReport = true;
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
			case ENUM_Pickup.Bomb: iValue = PlayerData.pthis.iBomb; break;
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
	public Tuple<int, int> ValueGap()
	{
		int iResultGain = 0;
		int iResultTotal = 0;

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			switch((ENUM_Pickup)Itor)
			{
			case ENUM_Pickup.Member:
				break;

			case ENUM_Pickup.Currency:
				iResultGain += Gain[Itor] - Used[Itor];
				iResultTotal += Total[Itor] - Used[Itor];
				break;

			case ENUM_Pickup.Battery:
				iResultGain += (Gain[Itor] - Used[Itor]) * GameDefine.iPriceBattery;
				iResultTotal += (Total[Itor] - Used[Itor]) * GameDefine.iPriceBattery;
				break;

			case ENUM_Pickup.LightAmmo:
				iResultGain += (Gain[Itor] - Used[Itor]) * GameDefine.iPriceLightAmmo;
				iResultTotal += (Total[Itor] - Used[Itor]) * GameDefine.iPriceLightAmmo;
				break;

			case ENUM_Pickup.HeavyAmmo:
				iResultGain += (Gain[Itor] - Used[Itor]) * GameDefine.iPriceHeavyAmmo;
				iResultTotal += (Total[Itor] - Used[Itor]) * GameDefine.iPriceHeavyAmmo;
				break;

			case ENUM_Pickup.Bomb:
				break;

			default:
				break;
			}//switch
		}//for

		return new Tuple<int, int>(iResultGain, iResultTotal);
	}
	public void Report()
	{
		if(bReport == false)
			return;

		string szReport = string.Format("Day{0} [{1}]\n", PlayerData.pthis.iStage, System.DateTime.Now);

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
			szReport += string.Format("{0,-10}(I:{1,4}, G:{2,4}, U:{3,4}, T:{4,4})\n", (ENUM_Pickup)Itor, Init[Itor], Gain[Itor], Used[Itor], Total[Itor]);

		Tuple<int, int> Gap = ValueGap();

		szReport += string.Format("ValueGap = G:{0,4}, T{1,4}\n", Gap.Item1, Gap.Item2);

		File.AppendAllText(GameDefine.szPickupStat, szReport);
	}
}
