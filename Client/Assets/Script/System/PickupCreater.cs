using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class PickupCreater : MonoBehaviour
{
	public static PickupCreater pthis = null;

	private List<GameObject> Data = new List<GameObject>();
	
	void Awake()
	{
		pthis = this;
	}
	// 取得隨機地圖拾取位置
	public MapCoor NextPickup()
	{
		if(DataMap.pthis.DataRoad.Count <= GameDefine.iPickupBorder)
			return new MapCoor();
		
		MapCoor Road = DataMap.pthis.DataRoad[Random.Range(GameDefine.iPickupBorder, DataMap.pthis.DataRoad.Count - GameDefine.iPickupBorder)];
		
		for(int iCount = 0; iCount < GameDefine.iPickupSearch; ++iCount)
		{
			MapCoor Result = new MapCoor(Road.X + Random.Range(-GameDefine.iPackupRange, GameDefine.iPackupRange), Road.Y + Random.Range(0, GameDefine.iPackupRange));
			bool bCheck = true;
			
			foreach(MapCoor Itor in DataMap.pthis.DataRoad)
				bCheck &= (Result.X == Itor.X && Result.Y == Itor.Y) == false;
			
			foreach(Pickup Itor in DataPickup.pthis.Data)
				bCheck &= (Result.X == Itor.Pos.X && Result.Y == Itor.Pos.Y) == false;
			
			if(bCheck)
				return Result;
		}//for
		
		return new MapCoor();
	}
	// 建立拾取
	public void Create()
	{
		DataPickup.pthis.Data.Clear();
		
		// 檢查隊伍裡是否沒有光源裝備
		bool bLight = false;
		
		foreach(Member ItorMember in DataPlayer.pthis.MemberParty)
		{
			DBFEquip Data = GameDBF.pthis.GetEquip(ItorMember.iEquip) as DBFEquip;
			
			if(Data == null)
				continue;
			
			if(Data.Mode == (int)ENUM_ModeEquip.Light)
				bLight = true;
		}//for
		
		// 成員拾取
		if(DataPlayer.pthis.MemberParty.Count < GameDefine.iMaxMemberParty)
		{
			if(bLight == false || Random.Range(0, 100) <= (GameDefine.iMaxMemberParty - DataPlayer.pthis.MemberParty.Count) * GameDefine.iPickupMember)
			{
				Pickup Data = new Pickup();
				
				Data.Pos = NextPickup();
				Data.iType = (int)ENUM_Pickup.Member;
				Data.iCount = 1;
				Data.iLooks = Rule.RandomMemberLooks();
				Data.bPickup = false;
				
				DataPickup.pthis.Data.Add(Data);
			}//if
		}//if
		
		// 總物品拾取次數
		int iPickupLightAmmo = (int)(GameDefine.iPickupItems * GameDefine.fPickupPartLightAmmo);
		int iPickupHeavyAmmo = (int)(GameDefine.iPickupItems * GameDefine.fPickupPartHeavyAmmo);
		int iPickupBattery = (int)(GameDefine.iPickupItems * GameDefine.fPickupPartBattery);
		int iPickupCurrency = GameDefine.iPickupItems - iPickupLightAmmo - iPickupHeavyAmmo - iPickupBattery;
		// 額外拾取價值
		int iExteraValue = (int)(DataPlayer.pthis.iStage * GameDefine.fUpgradePickup);
		
		// 輕型彈藥拾取
		for(int iCount = 0; iCount < iPickupLightAmmo; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = NextPickup();
			Data.iType = (int)ENUM_Pickup.LightAmmo;
			Data.iCount = (GameDefine.iPickupValue + iExteraValue) / GameDefine.iPriceLightAmmo;
			Data.iLooks = 0;
			Data.bPickup = false;
			
			DataPickup.pthis.Data.Add(Data);
		}//for
		
		// 重型彈藥拾取
		for(int iCount = 0; iCount < iPickupHeavyAmmo; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = NextPickup();
			Data.iType = (int)ENUM_Pickup.HeavyAmmo;
			Data.iCount = (GameDefine.iPickupValue + iExteraValue) / GameDefine.iPriceHeavyAmmo;
			Data.iLooks = 0;
			Data.bPickup = false;
			
			DataPickup.pthis.Data.Add(Data);
		}//for
		
		// 電池拾取
		for(int iCount = 0; iCount < iPickupBattery; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = NextPickup();
			Data.iType = (int)ENUM_Pickup.Battery;
			Data.iCount = (GameDefine.iPickupValue + iExteraValue) / GameDefine.iPriceBattery;
			Data.iLooks = 0;
			Data.bPickup = false;
			
			DataPickup.pthis.Data.Add(Data);
		}//for
		
		// 通貨拾取
		for(int iCount = 0; iCount < iPickupCurrency; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = NextPickup();
			Data.iType = (int)ENUM_Pickup.Currency;
			Data.iCount = GameDefine.iPickupValue + iExteraValue;
			Data.iLooks = 0;
			Data.bPickup = false;
			
			DataPickup.pthis.Data.Add(Data);
		}//for
		
		// 絕招拾取
		if(Random.Range(0, 100) <= GameDefine.iPickupProbBomb)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = NextPickup();
			Data.iType = (int)ENUM_Pickup.Bomb;
			Data.iCount = 1;
			Data.iLooks = 0;
			Data.bPickup = false;
			
			DataPickup.pthis.Data.Add(Data);
		}//if
		
		// 水晶拾取
		if(DataPlayer.pthis.iStage >= GameDefine.iCrystalStage)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = NextPickup();
			Data.iType = (int)ENUM_Pickup.Crystal;
			Data.iCount = Rule.AppearBossStage() ? GameDefine.iCrystalBoss : GameDefine.iCrystalNormal;
			Data.iLooks = 0;
			Data.bPickup = false;
			
			DataPickup.pthis.Data.Add(Data);
		}//if
	}
    // ------------------------------------------------------------------
    // 產生肉肉.
    public void CreateMeet(MapCoor pMapCoor)
    {
        Pickup pData = new Pickup();

        pData.Pos = pMapCoor;
        pData.iType = (int)ENUM_Pickup.Meet;
        pData.iCount = 1;
        pData.iLooks = 0;
        pData.bPickup = false;

        DataPickup.pthis.Data.Add(pData);

        GameObject pObjPickup = UITool.pthis.CreatePickup(PlayerCreater.pthis.gameObject, (ENUM_Pickup)pData.iType, 0, 0);
        pObjPickup.transform.position = MapCreater.pthis.GetMapObj(pMapCoor.ToVector2()).transform.position;

        Data.Add(pObjPickup);
        DataPickup.pthis.Save();
    }
    // ------------------------------------------------------------------
	// 顯示拾取
	public void Show(int iRoad)
	{
		GameObject pObjRoad = MapCreater.pthis.GetRoadObj(iRoad);

		if(pObjRoad == null)
			return;

		for(int iItemID = 0; iItemID < DataPickup.pthis.Data.Count; iItemID++ )
		{
			Pickup Itor = DataPickup.pthis.Data[iItemID];
			
			if(Itor.bPickup == false)
			{
				Vector2 Pos = Itor.Pos.ToVector2();
				float fPosX = Pos.x + GameDefine.iBlockSize / 2 - pObjRoad.transform.localPosition.x;
				float fPosY = Pos.y + GameDefine.iBlockSize / 2 - pObjRoad.transform.localPosition.y;
				GameObject pObjPickup = null;

                if ((ENUM_Pickup)Itor.iType == ENUM_Pickup.Member)
                    PlayerCreater.pthis.AddList(iItemID, fPosX, fPosY, Itor.iLooks);
                else
                {
                    pObjPickup = UITool.pthis.CreatePickup(PlayerCreater.pthis.gameObject, (ENUM_Pickup)Itor.iType, fPosX, fPosY);

                    if (pObjPickup && pObjPickup.GetComponent<Btn_GetCurrency>())
                        pObjPickup.GetComponent<Btn_GetCurrency>().iItemID = iItemID;
                    else if (pObjPickup && pObjPickup.GetComponent<Btn_GetBattery>())
                        pObjPickup.GetComponent<Btn_GetBattery>().iItemID = iItemID;
                    else if (pObjPickup && pObjPickup.GetComponent<Btn_GetLightAmmo>())
                        pObjPickup.GetComponent<Btn_GetLightAmmo>().iItemID = iItemID;
                    else if (pObjPickup && pObjPickup.GetComponent<Btn_GetHeavyAmmo>())
                        pObjPickup.GetComponent<Btn_GetHeavyAmmo>().iItemID = iItemID;
                    else if (pObjPickup && pObjPickup.GetComponent<Btn_GetBomb>())
                        pObjPickup.GetComponent<Btn_GetBomb>().iItemID = iItemID;
                    else if (pObjPickup && pObjPickup.GetComponent<Btn_GetCrystal>())
                        pObjPickup.GetComponent<Btn_GetCrystal>().iItemID = iItemID;
                }
				
				if(pObjPickup != null)
					Data.Add(pObjPickup);
			}//if
		}//for
	}
	// 清除拾取
	public void Clear()
	{
		foreach(GameObject Itor in Data)
			Destroy(Itor);
		
		Data.Clear();
		DataPickup.pthis.Clear();
	}
}
