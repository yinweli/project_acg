using UnityEngine;
using LibCSNStandard;
using System;
using System.Collections;
using System.Collections.Generic;

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
	public override string ToString()
	{
		return string.Format("({0}, {1})", X, Y);
	}	
	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}	
	public override bool Equals(object obj)
	{
		return Equals(obj as MapCoor);
	}	
	public bool Equals(MapCoor obj)
	{
		return obj != null && this.X == obj.X && this.Y == obj.Y;
	}
}

// 地圖格類別
public class MapBlock
{
	public const char ModeStart = '#';
	public const char ModeEnd = '@';
	public const char ModeNormal = ' ';
	public const char ModeRoad = 'R';
	public const char ModeBlock = 'B';

	public MapCoor Next = new MapCoor();
	public char Mode = ModeNormal;
}

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	private const int RoadSizeBase = 500; // 地圖道路基礎長度
	private const int RoadSizeAdd = 10; // 地圖道路增加長度
	private const int MapWidth = 80; // 地圖寬度
	private const int MapBorderX = 20; // 地圖X軸邊框長度
	private const int MapBorderY = 10; // 地圖Y軸邊框長度
	private const int PathStart = 10; // 起點路徑長度
	private const int PathMin = 5; // 最小路徑長度
	private const int PathMax = 20; // 最長路徑長度
	private enum ENUM_Dir { Up, Left, Right, }

	private System.Random m_Rand = new System.Random();
	private Dictionary<MapCoor, MapBlock> m_Data = new Dictionary<MapCoor, MapBlock>();
	private ENUM_Dir m_emDir = ENUM_Dir.Up;
	private int m_iSizeX = 0;
	private int m_iSizeY = 0;

	// 取得道路長度
	private int RoadSize(int iStage)
	{
		return RoadSizeBase + iStage / 5 * RoadSizeAdd;
	}
	// 取得最小地圖寬度
	private int MapWidthMin()
	{
		return MapBorderX;
	}
	// 取得最大地圖寬度
	private int MapWidthMax()
	{
		return MapWidth - MapBorderX;
	}
	// 取得路徑長度
	private int PathLength(bool bStart)
	{
		return bStart ? PathStart : m_Rand.Next(PathMin, PathMax);
	}
	// 取得起點座標
	private MapCoor StartPos()
	{
		return new MapCoor(m_Rand.Next(MapWidthMin(), MapWidthMax()), MapBorderY);
	}
	// 檢查座標是否正確
	private bool CheckCoor(MapCoor Pos)
	{
		return Pos.X >= MapWidthMin() && Pos.X < MapWidthMax() && Pos.Y >= 1;
	}
	// 依方向取得座標
	private MapCoor NextCoor(ENUM_Dir Dir, MapCoor Pos)
	{
		if(CheckCoor(Pos) == false)
			return null;

		MapCoor Result = new MapCoor(Pos.X, Pos.Y);
		
		switch(Dir)
		{
		case ENUM_Dir.Up: ++Result.Y; break;
		case ENUM_Dir.Left: --Result.X; break;
		case ENUM_Dir.Right: ++Result.X; break;
		default: break;
		}//switch

		return CheckCoor(Result) ? Result : null;
	}
	// 依方向取得座標列表
	private List<MapCoor> NextPath(ENUM_Dir Dir, MapCoor Pos, bool bStart)
	{
		int iLength = PathLength(bStart);
		List<MapCoor> Result = new List<MapCoor>();

		while(iLength > 0)
		{
			if((Pos = NextCoor(Dir, Pos)) != null)
			{
				Result.Add(Pos);
				--iLength;
			}
			else
				iLength = 0;
		}//while

		return Result;
	}
	// 建立地圖道路
	private void CreateRoad(int iStage)
	{
		int iRoadSize = RoadSize(iStage); // 取得道路長度
		bool bStart = true; // 起點旗標
		List<MapCoor> Road = new List<MapCoor>(); // 道路總表
		
		while(iRoadSize > 0)
		{
			// 建立骰子
			CDice<ENUM_Dir> Dice = new CDice<ENUM_Dir>();
			
			if(bStart)
				Dice.Set(ENUM_Dir.Up, 1);
			else
			{
				switch(m_emDir)
				{
				case ENUM_Dir.Up:
					Dice.Set(ENUM_Dir.Up, 1);
					Dice.Set(ENUM_Dir.Left, 1);
					Dice.Set(ENUM_Dir.Right, 1);
					break;
					
				case ENUM_Dir.Left:
					Dice.Set(ENUM_Dir.Up, 1);
					Dice.Set(ENUM_Dir.Left, 1);
					break;
					
				case ENUM_Dir.Right:
					Dice.Set(ENUM_Dir.Up, 1);
					Dice.Set(ENUM_Dir.Right, 1);
					break;
					
				default:
					break;
				}//switch
			}//if

			// 取得起點/上次的終點
			MapCoor Coor = bStart ? StartPos() : Road[Road.Count - 1];
			// 取得道路列表
			List<MapCoor> Temp = NextPath(m_emDir = Dice.Roll(m_Rand), Coor, bStart);

			// 把道路列表新增回道路總表
			foreach(MapCoor Itor in Temp)
			{
				if(iRoadSize > 0)
				{
					Road.Add(Itor);
					--iRoadSize; // 減少還需要產生的道路長度
				}//if
			}//for

			// 關閉起點旗標
			if(bStart)
				bStart = false;
		}//while

		// 把道路設定到地圖上
		foreach(MapCoor Itor in Road)
		{
			MapBlock Block = new MapBlock();
			
			Block.Mode = MapBlock.ModeRoad;
			
			m_iSizeX = Math.Max(MapWidth, Itor.X + 1);
			m_iSizeY = Math.Max(m_iSizeY, Itor.Y + 1);
			m_Data[Itor] = Block;
		}//for
	}
	// 建立地圖
	public void Create(int iStage)
	{
		// 清除資料
		m_Data.Clear();
		m_iSizeX = 0;
		m_iSizeY = 0;

		// 建立道路
		CreateRoad(iStage);

		// 測試用的輸出
		List<string> MapData = new List<string>();

		for(int iY = m_iSizeY - 1; iY >= 0; --iY)
		{
			string szOutput = "";

			for(int iX = 0; iX < m_iSizeX; ++iX)
			{
				MapCoor Pos = new MapCoor(iX, iY);

				szOutput += m_Data.ContainsKey(Pos) ? m_Data[Pos].Mode : ' ';
			}//for

			MapData.Add(szOutput);
		}//for

		Tool.SaveTextFile("d:\\map.txt", MapData);
		Debug.Log("Road : " + RoadSize(iStage));
	}

	void Start()
	{
		Create(100);
	}
}