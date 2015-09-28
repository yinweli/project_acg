using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StatPickup
{
	public int iInitial = 0; // 初始值
	public int iAvailable = 0; // 可獲得值
	public int iObtain = 0; // 已獲得值
	public int iUsed = 0; // 使用值
}
public class StatDamage
{
	public int iShot = 0; // 發射次數
	public int iHit = 0; // 擊中次數
	public int iDamage = 0; // 傷害值

	public float Average()
	{
		return iHit > 0 ? (float)iDamage / (float)iHit : 0.0f;
	}
	public float HitRate()
	{
		return iShot > 0 ? (float)iHit / (float)iShot : 0.0f;
	}
}

public class Statistics : MonoBehaviour
{
	static public Statistics pthis = null;

	public List<StatPickup> DataResource = new List<StatPickup>(); // 拾取資源列表
	public Dictionary<ENUM_Damage, StatDamage> DataDamage = new Dictionary<ENUM_Damage, StatDamage>(); // 傷害統計列表

	void Awake()
	{
		pthis = this;
	}
	public void ResetResource()
	{
		DataResource.Clear();

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Pickup)))
		{
			StatPickup Temp = new StatPickup();

			switch((ENUM_Pickup)Itor)
			{
			case ENUM_Pickup.Member: Temp.iInitial = DataPlayer.pthis.MemberParty.Count; break;
			case ENUM_Pickup.Currency: Temp.iInitial = DataPlayer.pthis.iCurrency; break;
			case ENUM_Pickup.Battery: Temp.iInitial = DataPlayer.pthis.iBattery; break;
			case ENUM_Pickup.LightAmmo: Temp.iInitial = DataPlayer.pthis.iLightAmmo; break;
			case ENUM_Pickup.HeavyAmmo: Temp.iInitial = DataPlayer.pthis.iHeavyAmmo; break;
			case ENUM_Pickup.Bomb: Temp.iInitial = DataPlayer.pthis.iBomb; break;
			case ENUM_Pickup.Crystal: Temp.iInitial = DataReward.pthis.iCrystal; break;
			default: break;
			}//switch

			foreach(Pickup ItorPickup in DataPickup.pthis.Data)
			{
				if(Itor == ItorPickup.iType)
					Temp.iAvailable += ItorPickup.iCount;
			}//for

			DataResource.Add(Temp);
		}//for
	}
	public void RecordResource(ENUM_Pickup emPickup, int iValue)
	{
		if(iValue == 0)
			return;

		int iPos = (int)emPickup;

		if(iPos >= DataResource.Count)
			return;

		if(iValue > 0)
			DataResource[iPos].iObtain += Mathf.Abs(iValue);
		else
			DataResource[iPos].iUsed += Mathf.Abs(iValue);
	}
	public void ResetDamage()
	{
		DataDamage.Clear();

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Damage)))
			DataDamage.Add((ENUM_Damage)Itor, new StatDamage());
	}
	public void RecordShot(ENUM_Weapon emWeapon)
	{
		switch(emWeapon)
		{
		case ENUM_Weapon.Light: RecordShot(ENUM_Damage.Light); break;
		case ENUM_Weapon.Knife: RecordShot(ENUM_Damage.Knife); break;
		case ENUM_Weapon.Pistol: RecordShot(ENUM_Damage.Pistol); break;
		case ENUM_Weapon.Revolver: RecordShot(ENUM_Damage.Revolver); break;
		case ENUM_Weapon.Eagle: RecordShot(ENUM_Damage.Eagle); break;
		case ENUM_Weapon.SUB: RecordShot(ENUM_Damage.SUB); break;
		case ENUM_Weapon.Rifle: RecordShot(ENUM_Damage.Rifle); break;
		case ENUM_Weapon.LMG: RecordShot(ENUM_Damage.LMG); break;
		default: break;
		}//switch
	}
	public void RecordShot(ENUM_Damage emDamage)
	{
		if(DataDamage.ContainsKey(emDamage))
			DataDamage[emDamage].iShot += 1;
	}
	public void RecordHit(ENUM_Damage emDamage, int iDamage, bool bHit)
	{
		if(DataDamage.ContainsKey(emDamage))
		{
			DataDamage[emDamage].iHit += bHit ? 1 : 0;
			DataDamage[emDamage].iDamage += iDamage;
		}//if
	}
}
