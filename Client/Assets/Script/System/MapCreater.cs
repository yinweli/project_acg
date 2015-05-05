﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	public static MapCreater pthis = null;

	private Dictionary<Vector2, GameObject> ObjectList = new Dictionary<Vector2, GameObject>();
	private List<GameObject> PickupList = new List<GameObject>();
	private int iWidth = 0;
	private int iHeight = 0;

	void Awake()
	{
		pthis = this;
	}

	// 更新地圖尺寸
	private void UpdateSize(MapCoor Pos)
	{
		iWidth = Mathf.Max(iWidth, Pos.X + 1);
		iHeight = Mathf.Max(iHeight, Pos.Y + 1);
	}
	// 建立地圖道路
	private void CreateRoad()
	{
		int iRoadSize = GameDefine.iRoadSizeBase + (int)(PlayerData.pthis.iStage * GameDefine.fUpgradeRoad); // 取得地圖道路長度
		RandDir Dir = new RandDir();
		
		while(iRoadSize > 0)
		{
			foreach(MapCoor Itor in Rule.NextPath(Dir.Get(), MapData.pthis.RoadList.Count > 0 ? MapData.pthis.RoadList[MapData.pthis.RoadList.Count - 1] : null))
			{
				if(iRoadSize > 0)
				{
					UpdateSize(Itor);
					MapData.pthis.RoadList.Add(Itor);
					--iRoadSize; // 減少還需要產生的地圖道路長度
				}//if
			}//for
		}//while
	}
	// 建立地圖起點
	private void CreateStart()
	{
		if(MapData.pthis.RoadList.Count > 0)
		{
			MapCoor Road = MapData.pthis.RoadList[0];

			if(Road.Y > 0)
			{
				MapObjt Data = new MapObjt();
				
				Data.Pos = new MapCoor(Road.X, Road.Y - 1);
				Data.Type = (int)ENUM_Map.MapStart;
				Data.Width = GameDefine.ObjtStart.X;
				Data.Height = GameDefine.ObjtStart.Y;

				MapData.pthis.ObjtList.Add(Data);
				UpdateSize(Data.Pos);
			}//if
		}//if
	}
	// 建立地圖終點
	private void CreateEnd()
	{
		if(MapData.pthis.RoadList.Count > 0)
		{
			MapCoor Road = MapData.pthis.RoadList[MapData.pthis.RoadList.Count - 1];
			MapObjt Data = new MapObjt();
			
			Data.Pos = new MapCoor(Road.X - 1, Road.Y + 1);
			Data.Type = (int)ENUM_Map.MapEnd;
			Data.Width = GameDefine.ObjtEnd.X;
			Data.Height = GameDefine.ObjtEnd.Y;
			
			MapData.pthis.ObjtList.Add(Data);
			UpdateSize(Data.Pos);
		}//if
	}
	// 建立地圖物件
	private void CreateObjt()
	{
		foreach(MapCoor Itor in MapData.pthis.RoadList)
		{
			foreach(ENUM_Dir ItorDir in System.Enum.GetValues(typeof(ENUM_Dir)))
			{
				int iProb = GameDefine.iObjtProb;
				int iInterval = 1;

				while(iProb > 0)
				{
					if(Random.Range(0, 100) < iProb)
					{
						MapCoor Pos = Itor.Add(ItorDir, iInterval);

						if(Pos.X >= 0 && Pos.X < GameDefine.iMapWidth && Pos.Y >= 0)
						{
							Tuple<ENUM_Map, MapCoor> Objt = Rule.NextObjt();
							MapObjt Data = new MapObjt();

							Data.Pos = Pos;
							Data.Type = (int)Objt.Item1;
							Data.Width = Objt.Item2.X;
							Data.Height = Objt.Item2.Y;

							bool bCheck = true;
							
							foreach(MapCoor ItorRoad in MapData.pthis.RoadList)
							{
								if(Data.Cover(ItorRoad))
									bCheck &= false;
							}//for
							
							foreach(MapObjt ItorObjt in MapData.pthis.ObjtList)
							{
								if(Data.Cover(ItorObjt))
									bCheck &= false;
							}//for
							
							if(bCheck)
							{
								MapData.pthis.ObjtList.Add(Data);
								UpdateSize(Data.Pos);
							}//if
						}//if
					}//if

					iProb -= GameDefine.iObjtDec;
					iInterval++;
				}//while
			}//for
		}//for
	}
	// 建立地圖拾取
	public void CreatePickup()
	{
		GameData.pthis.PickupList.Clear();

		// 檢查隊伍裡是否沒有光源裝備
		bool bLight = false;
		
		foreach(Member ItorMember in PlayerData.pthis.Members)
		{
			DBFEquip Data = GameDBF.This.GetEquip(ItorMember.iEquip) as DBFEquip;
			
			if(Data == null)
				continue;
			
			if(Data.Mode == (int)ENUM_ModeEquip.Light)
				bLight = true;
		}//for
		
		// 成員拾取
		if(PlayerData.pthis.Members.Count < GameDefine.iMaxMember)
		{
			if(bLight == false || Random.Range(0, 100) <= (GameDefine.iMaxMember - PlayerData.pthis.Members.Count) * GameDefine.iPickupMember)
			{
				Pickup Data = new Pickup();
				
				Data.Pos = Rule.NextPickup();
				Data.iType = (int)ENUM_Pickup.Member;
				Data.iCount = 1;
				Data.iSex = Random.Range(0, GameDefine.iMaxSex);
				Data.iLook = Random.Range(0, GameDefine.iMaxLook);
				Data.bPickup = false;
				
				GameData.pthis.PickupList.Add(Data);
			}//if
		}//if

		// 總物品拾取次數
		int iPickupTotal = Random.Range(GameDefine.iMinPickupItems, GameDefine.iMaxPickupItems);
		int iPickupLightAmmo = (int)(iPickupTotal * GameDefine.fPickupPartLightAmmo);
		int iPickupHeavyAmmo = (int)(iPickupTotal * GameDefine.fPickupPartHeavyAmmo);
		int iPickupBattery = (int)(iPickupTotal * GameDefine.fPickupPartBattery);
		int iPickupCurrency = iPickupTotal - iPickupLightAmmo - iPickupHeavyAmmo - iPickupBattery;
		// 額外拾取價值
		int iExteraValue = (int)(PlayerData.pthis.iStage * GameDefine.fUpgradePickup);

		// 輕型彈藥拾取
		for(int iCount = 0; iCount < iPickupLightAmmo; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = Rule.NextPickup();
			Data.iType = (int)ENUM_Pickup.LightAmmo;
			Data.iCount = (Random.Range(GameDefine.iMinPickupValue, GameDefine.iMaxPickupValue) + iExteraValue) / GameDefine.iPriceLightAmmo;
			Data.iSex = 0;
			Data.iLook = 0;
			Data.bPickup = false;
			
			GameData.pthis.PickupList.Add(Data);
		}//for

		// 重型彈藥拾取
		for(int iCount = 0; iCount < iPickupHeavyAmmo; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = Rule.NextPickup();
			Data.iType = (int)ENUM_Pickup.HeavyAmmo;
			Data.iCount = (Random.Range(GameDefine.iMinPickupValue, GameDefine.iMaxPickupValue) + iExteraValue) / GameDefine.iPriceHeavyAmmo;
			Data.iSex = 0;
			Data.iLook = 0;
			Data.bPickup = false;
			
			GameData.pthis.PickupList.Add(Data);
		}//for

		// 電池拾取
		for(int iCount = 0; iCount < iPickupBattery; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = Rule.NextPickup();
			Data.iType = (int)ENUM_Pickup.Battery;
			Data.iCount = (Random.Range(GameDefine.iMinPickupValue, GameDefine.iMaxPickupValue) + iExteraValue) / GameDefine.iPriceBattery;
			Data.iSex = 0;
			Data.iLook = 0;
			Data.bPickup = false;
			
			GameData.pthis.PickupList.Add(Data);
		}//for

		// 通貨拾取
		for(int iCount = 0; iCount < iPickupCurrency; ++iCount)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = Rule.NextPickup();
			Data.iType = (int)ENUM_Pickup.Currency;
			Data.iCount = Random.Range(GameDefine.iMinPickupValue, GameDefine.iMaxPickupValue) + iExteraValue;
			Data.iSex = 0;
			Data.iLook = 0;
			Data.bPickup = false;
			
			GameData.pthis.PickupList.Add(Data);
		}//for

		// 絕招拾取
		if(Random.Range(0, 100) <= GameDefine.iPickupProbBomb)
		{
			Pickup Data = new Pickup();
			
			Data.Pos = Rule.NextPickup();
			Data.iType = (int)ENUM_Pickup.Bomb;
			Data.iCount = 1;
			Data.iSex = 0;
			Data.iLook = 0;
			Data.bPickup = false;
			
			GameData.pthis.PickupList.Add(Data);
		}//if
	}
	// 填滿地圖
	private void Fill()
	{
		for(int iY = 0; iY < iHeight; ++iY)
		{
			for(int iX = 0; iX < iWidth; ++iX)
			{
				MapObjt Data = new MapObjt();
				
				Data.Pos = new MapCoor(iX, iY);
				Data.Type = (int)ENUM_Map.MapBase;
				Data.Width = GameDefine.ObjtBase.X;
				Data.Height = GameDefine.ObjtBase.Y;
				
				bool bCheck = true;
				
				foreach(MapCoor ItorRoad in MapData.pthis.RoadList)
				{
					if(Data.Cover(ItorRoad))
						bCheck &= false;
				}//for
				
				foreach(MapObjt ItorObjt in MapData.pthis.ObjtList)
				{
					if(Data.Cover(ItorObjt))
						bCheck &= false;
				}//for
				
				if(bCheck)
					MapData.pthis.ObjtList.Add(Data);
			}//for
		}//for
	}
    // ------------------------------------------------------------------
	// 建立地圖
	public void Create()
	{
		iWidth = GameDefine.iMapWidth;
		iHeight = 0;

		Clear();
		CreateRoad();
		CreateStart();
		CreateEnd();
		CreateObjt();		
		Fill();		
	}
    // ------------------------------------------------------------------
    public void ShowMap(int iRoad)
    {
        Refresh(iRoad);
        transform.localPosition = new Vector3(-GetRoadObj(iRoad).transform.localPosition.x, -GetRoadObj(iRoad).transform.localPosition.y, 0);
    }
	public void ShowPickup(int iRoad)
	{
		for(int i = 0; i < GameData.pthis.PickupList.Count; i++ )
		{
			Pickup itor = GameData.pthis.PickupList[i];

			if(itor.bPickup == false)
			{
				Vector2 Pos = itor.Pos.ToVector2();
				float fPosX = Pos.x + GameDefine.iBlockSize / 2 - GetRoadObj(iRoad).transform.localPosition.x;
				float fPosY = Pos.y + GameDefine.iBlockSize / 2 - GetRoadObj(iRoad).transform.localPosition.y;
				GameObject Obj = null;
				
				if ((ENUM_Pickup)itor.iType == ENUM_Pickup.Member)
					PlayerCreater.pthis.AddList(i, fPosX, fPosY, itor.iSex, itor.iLook);
				else
				{                
					Obj = UITool.pthis.CreatePickup(PlayerCreater.pthis.gameObject, (ENUM_Pickup)itor.iType, fPosX, fPosY);
					
					if (Obj && Obj.GetComponent<Btn_GetResource>())
						Obj.GetComponent<Btn_GetResource>().iItemID = i;
					else if (Obj && Obj.GetComponent<Btn_GetCurrency>())
						Obj.GetComponent<Btn_GetCurrency>().iItemID = i;
				}
				
				if(Obj != null)
					PickupList.Add(Obj);
			}//if
		}//for
	}
    // ------------------------------------------------------------------
	// 清除地圖
	public void Clear()
	{
		foreach(KeyValuePair<Vector2, GameObject> Itor in ObjectList)
			Destroy(Itor.Value);

		foreach(GameObject Itor in PickupList)
			Destroy(Itor);

		ObjectList.Clear();
		PickupList.Clear();
		MapData.pthis.RoadList.Clear();
        MapData.pthis.ObjtList.Clear();
	}
	// 更新地圖
	public void Refresh(int iRoad)
	{
		MapCoor RoadPos = MapData.pthis.RoadList.Count > iRoad ? MapData.pthis.RoadList[iRoad] : new MapCoor();
		MapCoor ChkPos = new MapCoor(RoadPos.X - GameDefine.iBlockUpdate / 2, RoadPos.Y - GameDefine.iBlockUpdate / 2);

		foreach(MapCoor Itor in MapData.pthis.RoadList)
		{
			Vector2 Pos = Itor.ToVector2();
			MapObjt Temp = new MapObjt();
			
			Temp.Pos = Itor;
			Temp.Width = 1;
			Temp.Height = 1;

			if(Temp.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(ObjectList.ContainsKey(Pos) == false)
				{
					float fPosX = Pos.x + GameDefine.iBlockSize / 2;
					float fPosY = Pos.y + GameDefine.iBlockSize / 2;

					ObjectList.Add(Pos, UITool.pthis.CreateMap(gameObject, ENUM_Map.MapRoad.ToString(), PlayerData.pthis.iStyle, fPosX, fPosY));
				}//if
			}
			else
			{
				if(ObjectList.ContainsKey(Pos))
				{
					Destroy(ObjectList[Pos]);
					ObjectList.Remove(Pos);
				}//if
			}//if
		}//for
		
		foreach(MapObjt Itor in MapData.pthis.ObjtList)
		{
			Vector2 Pos = Itor.Pos.ToVector2();

			if(Itor.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(ObjectList.ContainsKey(Pos) == false)
				{
					float fPosX = Pos.x + (Itor.Width * GameDefine.iBlockSize) / 2;
					float fPosY = Pos.y + (Itor.Height * GameDefine.iBlockSize) / 2;

					ObjectList.Add(Pos, UITool.pthis.CreateMap(gameObject, ((ENUM_Map)Itor.Type).ToString(), PlayerData.pthis.iStyle, fPosX, fPosY));
				}//if
			}
			else
			{
				if(ObjectList.ContainsKey(Pos))
				{
					Destroy(ObjectList[Pos]);
					ObjectList.Remove(Pos);
				}//if
			}//if
		}//for
	}
	// 取得道路物件
    public GameObject GetRoadObj(int iRoad)
	{
		if(iRoad < 0)
			iRoad = 0;

		if(MapData.pthis.RoadList.Count <= iRoad)
			return null;

		Vector2 Pos = MapData.pthis.RoadList[iRoad].ToVector2();

		if(ObjectList.ContainsKey(Pos) == false)
			return null;

		return ObjectList[Pos];
	}
}