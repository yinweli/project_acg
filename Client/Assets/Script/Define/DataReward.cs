using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataReward : MonoBehaviour 
{
	static public DataReward pthis = null;
	
	/* Save */
	public HashSet<int> MemberLooks = new HashSet<int>(); // 角色外觀列表
	public HashSet<int> MemberInits = new HashSet<int>(); // 初始角色列表
	public Dictionary<int, int> WeaponLevel = new Dictionary<int, int>(); // 武器等級列表
	public int iInitCurrency = 0; // 初始金錢
	public int iInitBattery = 0; // 初始電池
	public int iInitLightAmmo = 0; // 初始輕型彈藥
	public int iInitHeavyAmmo = 0; // 初始重型彈藥
	public int iInitBomb = 0; // 初始絕招

	public int iCrystal = 0; // 水晶, 用來購買水晶商人的貨品
	
	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		List<string> WeaponLevelTemp = new List<string>();
		
		foreach(KeyValuePair<int, int> Itor in WeaponLevel)
			WeaponLevelTemp.Add(Itor.Key + "_" + Itor.Value);

		SaveReward Temp = new SaveReward();
		
		Temp.MemberLooks = MemberLooks.ToList().ToArray();
		Temp.MemberInits = MemberInits.ToList().ToArray();
		Temp.WeaponLevel = WeaponLevelTemp.ToArray();
		Temp.iInitCurrency = iInitCurrency;
		Temp.iInitBattery = iInitBattery;
		Temp.iInitLightAmmo = iInitLightAmmo;
		Temp.iInitHeavyAmmo = iInitHeavyAmmo;
		Temp.iInitBomb = iInitBomb;
		Temp.iCrystal = iCrystal;
		
		PlayerPrefs.SetString(GameDefine.szSaveReward, Json.ToString(Temp));
	}
	// 讀檔.
	public bool Load()
	{
		SaveReward Temp = null;

		if(PlayerPrefs.HasKey(GameDefine.szSaveReward))
			Temp = Json.ToObject<SaveReward>(PlayerPrefs.GetString(GameDefine.szSaveReward));

		if(Temp != null)
		{
			MemberLooks = new HashSet<int>(Temp.MemberLooks);
			MemberInits = new HashSet<int>(Temp.MemberInits);
			
			foreach(string Itor in Temp.WeaponLevel)
			{
				string[] szTemp = Itor.Split(new char[] {'_'});
				
				if(szTemp.Length >= 2)
					WeaponLevel.Add(System.Convert.ToInt32(szTemp[0]), System.Convert.ToInt32(szTemp[1]));
			}//for
			
			iInitCurrency = Temp.iInitCurrency;
			iInitBattery = Temp.iInitBattery;
			iInitLightAmmo = Temp.iInitLightAmmo;
			iInitHeavyAmmo = Temp.iInitHeavyAmmo;
			iInitBomb = Temp.iInitBomb;
			iCrystal = Temp.iCrystal;
		}
		else
		{
			MemberLooks = new HashSet<int>(GameInit.InitMemberLooks);
			MemberInits = new HashSet<int>();
			WeaponLevel = new Dictionary<int, int>();
			iInitCurrency = GameInit.iInitCurrency;
			iInitBattery = GameInit.iInitBattery;
			iInitLightAmmo = GameInit.iInitLightAmmo;
			iInitHeavyAmmo = GameInit.iInitHeavyAmmo;
			iInitBomb = GameInit.iInitBomb;
			iCrystal = 0;
		}//if
		
		return true;
	}
	// 清除資料
	public void Clear()
	{
		MemberLooks.Clear();
		MemberInits.Clear();
		WeaponLevel.Clear();
		iInitCurrency = 0;
     	iInitBattery = 0;
     	iInitLightAmmo = 0;
     	iInitHeavyAmmo = 0;
     	iInitBomb = 0;
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSaveReward);
	}
}