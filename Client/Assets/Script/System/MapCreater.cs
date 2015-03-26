using UnityEngine;
using LibCSNStandard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// 隨機方向類別
public class RandDir
{
	private CDice<ENUM_Dir> m_Data = new CDice<ENUM_Dir>();
	private ENUM_Dir m_LastDir = ENUM_Dir.Null;

	public ENUM_Dir Get(System.Random Rand)
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

		return m_LastDir = m_Data.Roll(Rand);
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
		return new Vector2(X * GameDefine.iBlockWidth, Y * GameDefine.iBlockHeight);
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
	public int Index = 0;
	public int Width = 0;
	public int Height = 0;
	public GameObject Obj = null;

	public bool Cover(MapCoor Data, int W, int H)
	{
		int x1 = Pos.X, y1 = Pos.Y, w1 = Width, h1 = Height;
		int x2 = Data.X, y2 = Data.Y, w2 = W, h2 = H;
		
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
	private System.Random m_Rand = new System.Random();
	private Vector2 m_LastPos = new Vector2();

	public List<MapRoad> RoadList = new List<MapRoad>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表
	public int Stage = 0; // 關卡編號
	public int Style = 0; // 風格編號

	public static MapCreater This = null;
	
	void Awake()
	{
		This = this;
	}
	void Start()
	{
		//Create();
	}

	// 取得地圖道路預製物件名稱
	private string PrefabRoad()
	{
		return "MapRoad_";
	}
	// 取得地圖物件預製物件名稱
	private string PrefabObjt(int iIndex)
	{
		return "MapObjt_" + iIndex;
	}
	// 取得地圖道路長度
	private int RoadSize()
	{
		return GameDefine.iRoadSizeBase + Stage / GameDefine.iStageLevel * GameDefine.iRoadSizeAdd;
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
		int iLength = Pos == null ? GameDefine.iPathStart : m_Rand.Next(GameDefine.iPathMin, GameDefine.iPathMax);

		while(iLength > 0)
		{
			if(Pos == null)
				Pos = new MapCoor(m_Rand.Next(RoadWidthMin(), RoadWidthMax()), GameDefine.iMapBorderY);
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
	private Tuple<int, MapCoor> NextObjt()
	{
		int iIndex = m_Rand.Next(2, GameDefine.ObjtScale.Count - 1);

		return new Tuple<int, MapCoor>(iIndex, GameDefine.ObjtScale[iIndex]);
	}
	// 建立物件
	private GameObject CreateObject(string szName, Vector2 Pos)
	{
		return UITool.pthis.CreateUIByPos(gameObject, szName, Pos.x, Pos.y);
	}
	// 建立地圖道路
	private void CreateRoad()
	{
		int iRoadSize = RoadSize(); // 取得地圖道路長度
		RandDir Dir = new RandDir();
		
		while(iRoadSize > 0)
		{
			foreach(MapCoor Itor in NextPath(Dir.Get(m_Rand), RoadList.Count > 0 ? RoadList[RoadList.Count - 1].Pos : null))
			{
				if(iRoadSize > 0)
				{
					MapRoad Data = new MapRoad();

					Data.Pos = Itor;
					Data.Obj = null;

					RoadList.Add(Data);
					--iRoadSize; // 減少還需要產生的地圖道路長度
				}//if
			}//for
		}//while
	}
	// 建立地圖物件
	private void CreateObjt()
	{
		foreach(MapRoad Itor in RoadList)
		{
			foreach(ENUM_Dir ItorDir in Enum.GetValues(typeof(ENUM_Dir)))
			{
				int iProb = GameDefine.iObjtProb;
				int iInterval = 1;

				while(iProb > 0)
				{
					if(m_Rand.Next(100) < iProb)
					{
						MapCoor Pos = Itor.Pos.Add(ItorDir, iInterval);

						if(Pos.X >= 0 && Pos.X < GameDefine.iMapWidth && Pos.Y >= 0)
						{
							Tuple<int, MapCoor> Objt = NextObjt();
							MapObjt Data = new MapObjt();

							Data.Pos = Pos;
							Data.Index = Objt.Item1;
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
								ObjtList.Add(Data);
						}//if
					}//if

					iProb -= GameDefine.iObjtDec;
					iInterval++;
				}//while
			}//for
		}//for
	}
	// 建立地圖
	public void Create()
	{
		Clear();
		CreateRoad();
		CreateObjt();

		Debug.Log("map road : " + RoadList.Count);
		Debug.Log("map objt : " + ObjtList.Count);
	}
	// 清除地圖
	public void Clear()
	{
		RoadList.Clear();
		ObjtList.Clear();
	}
	// 更新地圖
	public void Refresh(Vector2 Pos)
	{
		if(Vector2.Distance(Pos, m_LastPos) <= GameDefine.fRangeUpdate)
			return;

		m_LastPos = Pos;

		foreach(MapRoad Itor in RoadList)
		{

		}//for

		foreach(MapObjt Itor in ObjtList)
		{

		}//for
		//CreateObject(PrefabRoad(), Itor.ToVector2());
		//Data.Obj = CreateObject(PrefabObjt(Data.Index), Data.Pos.ToVector2());
	}
}