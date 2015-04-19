using UnityEngine;
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
		int iRoadSize = GameDefine.iRoadSizeBase + PlayerData.pthis.iStage / GameDefine.iStageLevel * GameDefine.iRoadSizeAdd; // 取得地圖道路長度
		RandDir Dir = new RandDir();
		
		while(iRoadSize > 0)
		{
			foreach(MapCoor Itor in Rule.NextPath(Dir.Get(), GameData.pthis.RoadList.Count > 0 ? GameData.pthis.RoadList[GameData.pthis.RoadList.Count - 1] : null))
			{
				if(iRoadSize > 0)
				{
					UpdateSize(Itor);
					GameData.pthis.RoadList.Add(Itor);
					--iRoadSize; // 減少還需要產生的地圖道路長度
				}//if
			}//for
		}//while
	}
	// 建立地圖起點
	private void CreateStart()
	{
		if(GameData.pthis.RoadList.Count > 0)
		{
			MapCoor Road = GameData.pthis.RoadList[0];

			if(Road.Y > 0)
			{
				MapObjt Data = new MapObjt();
				
				Data.Pos = new MapCoor(Road.X, Road.Y - 1);
				Data.Type = (int)ENUM_Map.MapStart;
				Data.Width = GameDefine.ObjtStart.X;
				Data.Height = GameDefine.ObjtStart.Y;

				GameData.pthis.ObjtList.Add(Data);
				UpdateSize(Data.Pos);
			}//if
		}//if
	}
	// 建立地圖終點
	private void CreateEnd()
	{
		if(GameData.pthis.RoadList.Count > 0)
		{
			MapCoor Road = GameData.pthis.RoadList[GameData.pthis.RoadList.Count - 1];
			MapObjt Data = new MapObjt();
			
			Data.Pos = new MapCoor(Road.X - 1, Road.Y + 1);
			Data.Type = (int)ENUM_Map.MapEnd;
			Data.Width = GameDefine.ObjtEnd.X;
			Data.Height = GameDefine.ObjtEnd.Y;
			
			GameData.pthis.ObjtList.Add(Data);
			UpdateSize(Data.Pos);
		}//if
	}
	// 建立地圖物件
	private void CreateObjt()
	{
		foreach(MapCoor Itor in GameData.pthis.RoadList)
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
							
							foreach(MapCoor ItorRoad in GameData.pthis.RoadList)
							{
								if(Data.Cover(ItorRoad))
									bCheck &= false;
							}//for
							
							foreach(MapObjt ItorObjt in GameData.pthis.ObjtList)
							{
								if(Data.Cover(ItorObjt))
									bCheck &= false;
							}//for
							
							if(bCheck)
							{
								GameData.pthis.ObjtList.Add(Data);
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
		
		// 成員拾取
		if(PlayerData.pthis.Members.Count < GameDefine.iMaxMember)
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
		
		// 物品拾取
		for(int iCount = 0, iMax = Random.Range(GameDefine.iMinPickupItems, GameDefine.iMaxPickupItems); iCount < iMax; ++iCount)
		{
			int iValue = Random.Range(GameDefine.iMinPickupValue, GameDefine.iMaxPickupValue) + PlayerData.pthis.iStage / GameDefine.iStageLevel * GameDefine.iAddPickupValue;
			Pickup Data = new Pickup();
			
			Data.Pos = Rule.NextPickup();
			Data.iType = Random.Range((int)ENUM_Pickup.Currency, System.Enum.GetValues(typeof(ENUM_Pickup)).Length);
			
			switch((ENUM_Pickup)Data.iType)
			{
			case ENUM_Pickup.Currency: Data.iCount = iValue; break;
			case ENUM_Pickup.Battery: Data.iCount = iValue / GameDefine.iPriceBattery; break;
			case ENUM_Pickup.LightAmmo: Data.iCount = iValue / GameDefine.iPriceLightAmmo; break;
			case ENUM_Pickup.HeavyAmmo: Data.iCount = iValue / GameDefine.iPriceHeavyAmmo; break;
			default: Data.iCount = 0; break;
			}//switch
			
			Data.iSex = 0;
			Data.iLook = 0;
			Data.bPickup = false;
			
			GameData.pthis.PickupList.Add(Data);
		}//for

		for(int i = 0; i < GameData.pthis.PickupList.Count; i++ )
		{
            Pickup itor = GameData.pthis.PickupList[i];

            Vector2 Pos = itor.Pos.ToVector2();
			float fPosX = Pos.x + GameDefine.iBlockSize / 2 -GetRoadObj(0).transform.localPosition.x;
			float fPosY = Pos.y + GameDefine.iBlockSize / 2 -GetRoadObj(0).transform.localPosition.y;
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
		}//for
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
				
				foreach(MapCoor ItorRoad in GameData.pthis.RoadList)
				{
					if(Data.Cover(ItorRoad))
						bCheck &= false;
				}//for
				
				foreach(MapObjt ItorObjt in GameData.pthis.ObjtList)
				{
					if(Data.Cover(ItorObjt))
						bCheck &= false;
				}//for
				
				if(bCheck)
					GameData.pthis.ObjtList.Add(Data);
			}//for
		}//for
	}
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
		Refresh(0);
		transform.localPosition = new Vector3(-GetRoadObj(0).transform.localPosition.x, -GetRoadObj(0).transform.localPosition.y, 0);
	}
	// 清除地圖
	public void Clear()
	{
		foreach(KeyValuePair<Vector2, GameObject> Itor in ObjectList)
			Destroy(Itor.Value);

		foreach(GameObject Itor in PickupList)
			Destroy(Itor);

		ObjectList.Clear();
		PickupList.Clear();
		GameData.pthis.RoadList.Clear();
        GameData.pthis.ObjtList.Clear();
	}
	// 更新地圖
	public void Refresh(int iRoad)
	{
		MapCoor RoadPos = GameData.pthis.RoadList.Count > iRoad ? GameData.pthis.RoadList[iRoad] : new MapCoor();
		MapCoor ChkPos = new MapCoor(RoadPos.X - GameDefine.iBlockUpdate / 2, RoadPos.Y - GameDefine.iBlockUpdate / 2);

		foreach(MapCoor Itor in GameData.pthis.RoadList)
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

					ObjectList.Add(Pos, UITool.pthis.CreateMap(gameObject, ENUM_Map.MapRoad.ToString(), GameData.pthis.iStyle, fPosX, fPosY));
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
		
		foreach(MapObjt Itor in GameData.pthis.ObjtList)
		{
			Vector2 Pos = Itor.Pos.ToVector2();

			if(Itor.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(ObjectList.ContainsKey(Pos) == false)
				{
					float fPosX = Pos.x + (Itor.Width * GameDefine.iBlockSize) / 2;
					float fPosY = Pos.y + (Itor.Height * GameDefine.iBlockSize) / 2;

					ObjectList.Add(Pos, UITool.pthis.CreateMap(gameObject, ((ENUM_Map)Itor.Type).ToString(), GameData.pthis.iStyle, fPosX, fPosY));
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
		if(GameData.pthis.RoadList.Count <= iRoad)
			return null;

		Vector2 Pos = GameData.pthis.RoadList[iRoad].ToVector2();

		if(ObjectList.ContainsKey(Pos) == false)
			return null;

		return ObjectList[Pos];
	}
}