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

// 地圖單元類別
public class MapUnit
{
	public const char Null = 'N';
	public const char Start = 'S';
	public const char End = 'E';
	public const char Normal = ' ';
	public const char Road = 'R';
	public const char Block = 'B';

	private Dictionary<MapCoor, char> m_Data = new Dictionary<MapCoor, char>();
	private int m_iSizeX = 0;
	private int m_iSizeY = 0;

	public int X
	{
		get
		{
			return m_iSizeX;
		}
	}
	public int Y
	{
		get
		{
			return m_iSizeY;
		}
	}
	public void Add(MapCoor Pos, char Mode)
	{
		m_Data[Pos] = Mode;
		m_iSizeX = Math.Max(m_iSizeX, Pos.X + 1);
		m_iSizeY = Math.Max(m_iSizeY, Pos.Y + 1);
	}
	public char Get(MapCoor Pos)
	{
		return m_Data.ContainsKey(Pos) ? m_Data[Pos] : Null;
	}
}

// 地圖道路類別
public class MapRoad
{
	private List<MapCoor> m_Data = new List<MapCoor>();

	public int Count
	{
		get
		{
			return m_Data.Count;
		}
	}
	public void Add(MapCoor Pos)
	{
		m_Data.Add(Pos);
	}
	public void Add(List<MapCoor> Data)
	{
		m_Data.AddRange(Data);
	}
	public void Add(List<MapCoor> Data, int iCount)
	{
		m_Data.AddRange(Data.GetRange(0, iCount));
	}
	public MapCoor Get(int iPos)
	{
		return iPos >= 0 && iPos < m_Data.Count ? m_Data[iPos] : null;
	}
}

// 地圖障礙物類別
public class MapBlock
{
	public class Block
	{
		public MapCoor Pos = new MapCoor();
		public int Width = 0;
		public int Height = 0;

		public Block() {}
		public Block(MapCoor pos, int width, int height)
		{
			Pos = pos;
			Width = width;
			Height = height;
		}
		public bool Empty()
		{
			return Pos.X < 0 || Pos.Y < 0 || Width <= 0 || Height <= 0;
		}
		public bool Cover(Block Data)
		{
			int x1 = Pos.X;
			int y1 = Pos.Y;
			int w1 = Width;
			int h1 = Height;

			int x2 = Data.Pos.X;
			int y2 = Data.Pos.Y;
			int w2 = Data.Width;
			int h2 = Data.Height;

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
	}

	private List<Block> m_Data = new List<Block>();

	public int Count
	{
		get
		{
			return m_Data.Count;
		}
	}
	public bool Add(Block Data)
	{
		if(Data.Empty())
			return false;

		foreach(Block Itor in m_Data)
		{
			if(Data.Cover(Itor))
				return false;
		}//for

		m_Data.Add(Data);

		return true;
	}
	public Block Get(int iPos)
	{
		return iPos >= 0 && iPos < m_Data.Count ? m_Data[iPos] : null;
	}
}

// 地圖資料類別
public class MapData
{
	public MapUnit Unit = new MapUnit();
	public MapRoad Road = new MapRoad();
	public MapBlock Block = new MapBlock();
}

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	private const int RoadSizeBase = 500; // 地圖道路基礎長度
	private const int RoadSizeAdd = 10; // 地圖道路增加長度
	private const int MapWidth = 80; // 地圖寬度
	private const int MapBorderX = 12; // 地圖X軸邊框長度
	private const int MapBorderY = 10; // 地圖Y軸邊框長度
	private const int PathStart = 10; // 起點路徑長度
	private const int PathMin = 6; // 最小路徑長度
	private const int PathMax = 16; // 最長路徑長度
	private const int BlockScaleMin = 1; // 最小障礙物尺寸
	private const int BlockScaleMax = 3; // 最大障礙物尺寸
	private const int BlockSpaceMin = 3; // 最小障礙物與道路距離
	private const int BlockSpaceMax = 5; // 最大障礙物與道路距離
	private enum ENUM_Dir { Up, Left, Right, }

	private System.Random m_Rand = new System.Random();

	[SerializeField] private int Stage = 0;

	// 取得道路長度
	private int RoadSize(int iStage)
	{
		return RoadSizeBase + iStage / 5 * RoadSizeAdd;
	}
	// 取得最小道路寬度
	private int RoadWidthMin()
	{
		return MapBorderX;
	}
	// 取得最大道路寬度
	private int RoadWidthMax()
	{
		return MapWidth - MapBorderX;
	}
	// 取得路徑長度
	private int PathLength(bool bStart)
	{
		return bStart ? PathStart : m_Rand.Next(PathMin, PathMax);
	}
	// 檢查座標是否正確
	private bool CheckCoor(MapCoor Pos)
	{
		return Pos.X >= RoadWidthMin() && Pos.X < RoadWidthMax() && Pos.Y >= 1;
	}
	// 取得起點座標
	private MapCoor StartPos()
	{
		return new MapCoor(m_Rand.Next(RoadWidthMin(), RoadWidthMax()), MapBorderY);
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
	// 建立方向骰子
	private CDice<ENUM_Dir> DiceDir(ENUM_Dir emDir, bool bStart)
	{
		CDice<ENUM_Dir> Result = new CDice<ENUM_Dir>();
		
		if(bStart)
			Result.Set(ENUM_Dir.Up, 1);
		else
		{
			switch(emDir)
			{
			case ENUM_Dir.Up:
				Result.Set(ENUM_Dir.Up, 1);
				Result.Set(ENUM_Dir.Left, 1);
				Result.Set(ENUM_Dir.Right, 1);
				break;
				
			case ENUM_Dir.Left:
				Result.Set(ENUM_Dir.Up, 1);
				Result.Set(ENUM_Dir.Left, 1);
				break;
				
			case ENUM_Dir.Right:
				Result.Set(ENUM_Dir.Up, 1);
				Result.Set(ENUM_Dir.Right, 1);
				break;
				
			default:
				break;
			}//switch
		}//if

		return Result;
	}
	// 建立地圖道路
	private void CreateRoad(MapData Data, int iStage)
	{
		int iRoadSize = RoadSize(iStage); // 取得道路長度
		bool bStart = true; // 起點旗標
		ENUM_Dir emDir = ENUM_Dir.Up; // 上次方向
		
		while(iRoadSize > 0)
		{
			// 建立骰子
			CDice<ENUM_Dir> Dice = DiceDir(emDir, bStart);
			// 取得起點/上次的終點
			MapCoor Coor = bStart ? StartPos() : Data.Road.Get(Data.Road.Count - 1);
			// 取得道路列表
			List<MapCoor> Temp = NextPath(emDir = Dice.Roll(m_Rand), Coor, bStart);
			// 取得可增加的數量
			int iCount = Math.Min(Temp.Count, iRoadSize);

			Data.Road.Add(Temp, iCount); // 把道路列表新增回地圖道路
			iRoadSize -= iCount; // 減少還需要產生的道路長度

			// 關閉起點旗標
			if(bStart)
				bStart = false;
		}//while
	}
	// 建立地圖障礙物
	public void CreateBlock(MapData Data)
	{

	}
	// 建立地圖單元
	public void CreateUnit(MapData Data)
	{
		// 把道路設定到地圖單元上
		for(int iPos = 0; iPos < Data.Road.Count; ++iPos)
		{
			MapCoor Pos = Data.Road.Get(iPos);

			if(iPos == 0)
				Data.Unit.Add(Pos, MapUnit.Start);
			else if(iPos >= Data.Road.Count - 1)
				Data.Unit.Add(Pos, MapUnit.End);
			else
				Data.Unit.Add(Pos, MapUnit.Road);
		}//for

		// 把障礙物設定到地圖單元上
		for(int iPos = 0; iPos < Data.Block.Count; ++iPos)
		{
			MapBlock.Block Block = Data.Block.Get(iPos);

			for(int iY = 0; iY < Block.Height; ++iY)
			{
				for(int iX = 0; iX < Block.Width; ++iX)
					Data.Unit.Add(new MapCoor(Block.Pos.X + iX, Block.Pos.Y + iY), MapUnit.Block);
			}//for
		}//for

		// 填充剩餘單元
		int iSizeX = MapWidth;
		int iSizeY = Data.Unit.Y + MapBorderY;

		for(int iY = 0; iY < iSizeY; ++iY)
		{
			for(int iX = 0; iX < iSizeX; ++iX)
			{
				MapCoor Pos = new MapCoor(iX, iY);

				if(Data.Unit.Get(Pos) == MapUnit.Null)
					Data.Unit.Add(Pos, MapUnit.Normal);
			}//for
		}//for
	}
	// 建立地圖
	public MapData Create(int iStage)
	{
		MapData Data = new MapData();

		// 建立地圖道路
		CreateRoad(Data, iStage);
		// 建立地圖障礙物
		CreateBlock(Data);
		// 建立地圖單元
		CreateUnit(Data);

		// 測試用的輸出
		List<string> MapText = new List<string>();

		for(int iY = Data.Unit.Y - 1; iY >= 0; --iY)
		{
			string szOutput = "";

			for(int iX = 0; iX < Data.Unit.X; ++iX)
				szOutput += Data.Unit.Get(new MapCoor(iX, iY));

			MapText.Add(szOutput);
		}//for

		Tool.SaveTextFile("d:\\map.txt", MapText);
		Debug.Log("Road : " + RoadSize(iStage));

		return Data;
	}

	void Start()
	{
		Create(Stage);
	}
}