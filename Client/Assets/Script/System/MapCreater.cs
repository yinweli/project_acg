using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	public static MapCreater pthis = null;

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
			foreach(MapCoor Itor in Rule.NextPath(Dir.Get(), GameData.pthis.RoadList.Count > 0 ? GameData.pthis.RoadList[GameData.pthis.RoadList.Count - 1].Pos : null))
			{
				if(iRoadSize > 0)
				{
					MapRoad Data = new MapRoad();

					Data.Pos = Itor;
					Data.Obj = null;

					UpdateSize(Itor);
					GameData.pthis.RoadList.Add(Data);
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
			MapRoad Road = GameData.pthis.RoadList[0];

			if(Road.Pos.Y > 0)
			{
				MapObjt Data = new MapObjt();
				
				Data.Pos = new MapCoor(Road.Pos.X, Road.Pos.Y - 1);
				Data.Type = (int)ENUM_Map.MapStart;
				Data.Width = GameDefine.ObjtStart.X;
				Data.Height = GameDefine.ObjtStart.Y;
				Data.Obj = null;

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
			MapRoad Road = GameData.pthis.RoadList[GameData.pthis.RoadList.Count - 1];
			
			MapObjt Data = new MapObjt();
			
			Data.Pos = new MapCoor(Road.Pos.X - 1, Road.Pos.Y + 1);
			Data.Type = (int)ENUM_Map.MapEnd;
			Data.Width = GameDefine.ObjtEnd.X;
			Data.Height = GameDefine.ObjtEnd.Y;
			Data.Obj = null;
			
			GameData.pthis.ObjtList.Add(Data);
			UpdateSize(Data.Pos);
		}//if
	}
	// 建立地圖物件
	private void CreateObjt()
	{
		foreach(MapRoad Itor in GameData.pthis.RoadList)
		{
			foreach(ENUM_Dir ItorDir in System.Enum.GetValues(typeof(ENUM_Dir)))
			{
				int iProb = GameDefine.iObjtProb;
				int iInterval = 1;

				while(iProb > 0)
				{
					if(Random.Range(0, 100) < iProb)
					{
						MapCoor Pos = Itor.Pos.Add(ItorDir, iInterval);

						if(Pos.X >= 0 && Pos.X < GameDefine.iMapWidth && Pos.Y >= 0)
						{
							Tuple<ENUM_Map, MapCoor> Objt = Rule.NextObjt();
							MapObjt Data = new MapObjt();

							Data.Pos = Pos;
							Data.Type = (int)Objt.Item1;
							Data.Width = Objt.Item2.X;
							Data.Height = Objt.Item2.Y;
							Data.Obj = null;

							bool bCheck = true;
							
							foreach(MapRoad ItorRoad in GameData.pthis.RoadList)
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
				Data.Obj = null;
				
				bool bCheck = true;
				
				foreach(MapRoad ItorRoad in GameData.pthis.RoadList)
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
	}
	// 清除地圖
	public void Clear()
	{
        foreach (MapRoad itor in GameData.pthis.RoadList)
            Destroy(itor.Obj);

		GameData.pthis.RoadList.Clear();

        foreach (MapObjt itor in GameData.pthis.ObjtList)
            Destroy(itor.Obj);

        GameData.pthis.ObjtList.Clear();
	}
	// 更新地圖
	public void Refresh(int iRoad)
	{
		MapCoor RoadPos = GameData.pthis.RoadList.Count > iRoad ? GameData.pthis.RoadList[iRoad].Pos : new MapCoor();
		MapCoor ChkPos = new MapCoor(RoadPos.X - GameDefine.iBlockUpdate / 2, RoadPos.Y - GameDefine.iBlockUpdate / 2);

		foreach(MapRoad Itor in GameData.pthis.RoadList)
		{
			MapObjt Temp = new MapObjt();
			
			Temp.Pos = Itor.Pos;
			Temp.Width = 1;
			Temp.Height = 1;

			if(Temp.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(Itor.Obj == null)
				{
					Vector2 Pos = Itor.Pos.ToVector2();
					float fPosX = Pos.x + GameDefine.iBlockSize / 2;
					float fPosY = Pos.y + GameDefine.iBlockSize / 2;

					Itor.Obj = UITool.pthis.CreateMap(gameObject, ENUM_Map.MapRoad.ToString(), GameData.pthis.iStyle, fPosX, fPosY);
				}//if
			}
			else
			{
				if(Itor.Obj != null)
				{
					Destroy(Itor.Obj);
					Itor.Obj = null;
				}//if
			}//if
		}//for
		
		foreach(MapObjt Itor in GameData.pthis.ObjtList)
		{
			if(Itor.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(Itor.Obj == null)
				{
					Vector2 Pos = Itor.Pos.ToVector2();
					float fPosX = Pos.x + (Itor.Width * GameDefine.iBlockSize) / 2;
					float fPosY = Pos.y + (Itor.Height * GameDefine.iBlockSize) / 2;

					Itor.Obj = UITool.pthis.CreateMap(gameObject, ((ENUM_Map)Itor.Type).ToString(), GameData.pthis.iStyle, fPosX, fPosY);
				}//if
			}
			else
			{
				if(Itor.Obj != null)
				{
					Destroy(Itor.Obj);
					Itor.Obj = null;
				}//if
			}//if
		}//for
	}
	// 取得道路物件
    public GameObject GetRoadObj(int iRoad)
	{
		return GameData.pthis.RoadList.Count > iRoad ? GameData.pthis.RoadList[iRoad].Obj : null;
	}
}