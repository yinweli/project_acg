using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// 隨機方向類別
public class RandDir
{
	private CDice<ENUM_Dir> m_Data = new CDice<ENUM_Dir>();
	private ENUM_Dir m_LastDir = ENUM_Dir.Null;

	public ENUM_Dir Get()
	{
		m_Data.Clear();

		if(m_LastDir == ENUM_Dir.Null)
			m_Data.Set(ENUM_Dir.Up, 1);
		else
		{
			switch(m_LastDir)
			{
			case ENUM_Dir.Up:
				m_Data.Set(ENUM_Dir.Up, 2);
				m_Data.Set(ENUM_Dir.Left, 3);
				m_Data.Set(ENUM_Dir.Right, 3);
				break;
				
			case ENUM_Dir.Left:
				m_Data.Set(ENUM_Dir.Up, 3);
				m_Data.Set(ENUM_Dir.Left, 2);
				break;
				
			case ENUM_Dir.Right:
				m_Data.Set(ENUM_Dir.Up, 3);
				m_Data.Set(ENUM_Dir.Right, 2);
				break;
				
			default:
				break;
			}//switch
		}//if

		return m_LastDir = m_Data.Roll();
	}
}

// 地圖座標類別
public class MapCoor
{
	public int X = 0;
	public int Y = 0;
	
	public MapCoor() {}
	public MapCoor(int x, int y)
	{
		X = x;
		Y = y;
	}
	public MapCoor Add(ENUM_Dir emDir, int iInterval)
	{
		MapCoor Result = new MapCoor(X, Y);

		switch(emDir)
		{
		case ENUM_Dir.Up: Result.Y += iInterval; break;
		case ENUM_Dir.Left: Result.X -= iInterval; break;
		case ENUM_Dir.Right: Result.X += iInterval; break;
		case ENUM_Dir.Down: Result.Y -= iInterval; break;
		default: break;
		}//switch

		return Result;
	}
	public Vector2 ToVector2()
	{
		return new Vector2(X * GameDefine.iBlockSize, Y * GameDefine.iBlockSize);
	}
}

// 地圖道路類別
public class MapRoad
{
	public MapCoor Pos = new MapCoor();
	public GameObject Obj = null;
}

// 地圖物件類別
public class MapObjt
{
	public MapCoor Pos = new MapCoor();
	public int Type = 0;
	public int Width = 0;
	public int Height = 0;
	public GameObject Obj = null;

	public bool Cover(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
	{
		if(x1 >= x2 && x1 >= x2 + w2)
			return false;
		else if(x1 <= x2 && x1 + w1 <= x2)
			return false;
		else if(y1 >= y2 && y1 >= y2 + h2)
			return false;
		else if(y1 <= y2 && y1 + h1 <= y2)
			return false;
		
		return true;
	}
	public bool Cover(MapCoor Data, int W, int H)
	{
		return Cover(Pos.X, Pos.Y, Width, Height, Data.X, Data.Y, W, H);
	}
	public bool Cover(MapRoad Data)
	{
		return Cover(Data.Pos, 1, 1);
	}
	public bool Cover(MapObjt Data)
	{
		return Cover(Data.Pos, Data.Width, Data.Height);
	}
}

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	public List<MapRoad> RoadList = new List<MapRoad>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表
	public int iWidth = GameDefine.iMapWidth; // 地圖寬度
	public int iHeight = 0; // 地圖高度
	public int iStage = 1; // 關卡編號

	public GameObject MapBaseObject = null;

	public static MapCreater This = null;

	void Awake()
	{
		This = this;
	}

	// 取得地圖道路長度
	private int RoadSize()
	{
		return GameDefine.iRoadSizeBase + iStage / GameDefine.iStageLevel * GameDefine.iRoadSizeAdd;
	}
	// 取得最小地圖道路寬度
	private int RoadWidthMin()
	{
		return GameDefine.iMapBorderX;
	}
	// 取得最大地圖道路寬度
	private int RoadWidthMax()
	{
		return GameDefine.iMapWidth - GameDefine.iMapBorderX;
	}
	// 依方向取得座標列表
	private List<MapCoor> NextPath(ENUM_Dir emDir, MapCoor Pos)
	{
		List<MapCoor> Result = new List<MapCoor>();
		int iLength = Pos == null ? GameDefine.iPathStart : Random.Range(GameDefine.iPathMin, GameDefine.iPathMax);

		while(iLength > 0)
		{
			if(Pos == null)
				Pos = new MapCoor(Random.Range(RoadWidthMin(), RoadWidthMax()), GameDefine.iMapBorderY);
			else
				Pos = Pos.Add(emDir, 1);

			if(Pos.X >= RoadWidthMin() && Pos.X < RoadWidthMax() && Pos.Y >= 1)
			{
				Result.Add(Pos);
				--iLength;
			}
			else
				iLength = 0;
		}//while
		
		return Result;
	}
	// 取得隨機物件
	private Tuple<ENUM_Map, MapCoor> NextObjt()
	{
		int iIndex = Random.Range(0, GameDefine.ObjtScale.Count);

		return new Tuple<ENUM_Map, MapCoor>((ENUM_Map)(iIndex + ENUM_Map.MapObjt_0), GameDefine.ObjtScale[iIndex]);
	}
	// 建立物件
	private GameObject CreateObject(ENUM_Map emMap, Vector2 Pos, int iWidth, int iHeight)
	{
		return UITool.pthis.CreateMap(gameObject, emMap.ToString(), GameData.pthis.iStyle, Pos.x + iWidth / 2, Pos.y + iHeight / 2);
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
		int iRoadSize = RoadSize(); // 取得地圖道路長度
		RandDir Dir = new RandDir();
		
		while(iRoadSize > 0)
		{
			foreach(MapCoor Itor in NextPath(Dir.Get(), RoadList.Count > 0 ? RoadList[RoadList.Count - 1].Pos : null))
			{
				if(iRoadSize > 0)
				{
					MapRoad Data = new MapRoad();

					Data.Pos = Itor;
					Data.Obj = null;

					UpdateSize(Itor);
					RoadList.Add(Data);
					--iRoadSize; // 減少還需要產生的地圖道路長度
				}//if
			}//for
		}//while
	}
	// 建立地圖起點
	private void CreateStart()
	{
		if(RoadList.Count > 0)
		{
			MapRoad Road = RoadList[0];

			if(Road.Pos.Y > 0)
			{
				MapObjt Data = new MapObjt();
				
				Data.Pos = new MapCoor(Road.Pos.X, Road.Pos.Y - 1);
				Data.Type = (int)ENUM_Map.MapStart;
				Data.Width = GameDefine.ObjtStart.X;
				Data.Height = GameDefine.ObjtStart.Y;
				Data.Obj = null;

				ObjtList.Add(Data);
				UpdateSize(Data.Pos);
			}//if
		}//if
	}
	// 建立地圖終點
	private void CreateEnd()
	{
		if(RoadList.Count > 0)
		{
			MapRoad Road = RoadList[RoadList.Count - 1];
			
			MapObjt Data = new MapObjt();
			
			Data.Pos = new MapCoor(Road.Pos.X - 1, Road.Pos.Y + 1);
			Data.Type = (int)ENUM_Map.MapEnd;
			Data.Width = GameDefine.ObjtEnd.X;
			Data.Height = GameDefine.ObjtEnd.Y;
			Data.Obj = null;
			
			ObjtList.Add(Data);
			UpdateSize(Data.Pos);
		}//if
	}
	// 建立地圖物件
	private void CreateObjt()
	{
		foreach(MapRoad Itor in RoadList)
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
							Tuple<ENUM_Map, MapCoor> Objt = NextObjt();
							MapObjt Data = new MapObjt();

							Data.Pos = Pos;
							Data.Type = (int)Objt.Item1;
							Data.Width = Objt.Item2.X;
							Data.Height = Objt.Item2.Y;
							Data.Obj = null;

							bool bCheck = true;
							
							foreach(MapRoad ItorRoad in RoadList)
							{
								if(Data.Cover(ItorRoad))
									bCheck &= false;
							}//for
							
							foreach(MapObjt ItorObjt in ObjtList)
							{
								if(Data.Cover(ItorObjt))
									bCheck &= false;
							}//for
							
							if(bCheck)
							{
								ObjtList.Add(Data);
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
				
				foreach(MapRoad ItorRoad in RoadList)
				{
					if(Data.Cover(ItorRoad))
						bCheck &= false;
				}//for
				
				foreach(MapObjt ItorObjt in ObjtList)
				{
					if(Data.Cover(ItorObjt))
						bCheck &= false;
				}//for
				
				if(bCheck)
					ObjtList.Add(Data);
			}//for
		}//for
	}
	// 建立地圖
	public void Create()
	{
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
		RoadList.Clear();
		ObjtList.Clear();
		iWidth = 0;
		iHeight = 0;
	}
	// 更新地圖
	public void Refresh(int iRoad)
	{
		MapCoor RoadPos = RoadList.Count > iRoad ? RoadList[iRoad].Pos : new MapCoor();
		MapCoor ChkPos = new MapCoor(RoadPos.X - GameDefine.iBlockUpdate / 2, RoadPos.Y - GameDefine.iBlockUpdate / 2);

		foreach(MapRoad Itor in RoadList)
		{
			MapObjt Temp = new MapObjt();
			
			Temp.Pos = Itor.Pos;
			Temp.Width = 1;
			Temp.Height = 1;

			if(Temp.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(Itor.Obj == null)
					Itor.Obj = CreateObject(ENUM_Map.MapRoad, Itor.Pos.ToVector2(), GameDefine.iBlockSize, GameDefine.iBlockSize);
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
		
		foreach(MapObjt Itor in ObjtList)
		{
			if(Itor.Cover(ChkPos, GameDefine.iBlockUpdate, GameDefine.iBlockUpdate))
			{
				if(Itor.Obj == null)
					Itor.Obj = CreateObject((ENUM_Map)Itor.Type, Itor.Pos.ToVector2(), Itor.Width * GameDefine.iBlockSize, Itor.Height * GameDefine.iBlockSize);
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
		return RoadList.Count > iRoad ? RoadList[iRoad].Obj : null;
	}
	// 檢查是否為空
	public bool IsEmpty()
	{
		return RoadList.Count <= 0 || ObjtList.Count <= 0;
	}
}