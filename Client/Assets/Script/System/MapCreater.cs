using UnityEngine;
using LibCSNStandard;
using System;
using System.Collections;
using System.Collections.Generic;

// 地圖方向類別
public class MapDir
{
	public enum ENUM_Dir { Up, Left, Right, }
	
	private CDice<ENUM_Dir> Data = new CDice<ENUM_Dir>();
	private int DirCount = Enum.GetValues(typeof(ENUM_Dir)).Length;
	
	public MapDir(int count)
	{
		Data.Set(ENUM_Dir.Up, count * DirCount);
		Data.Set(ENUM_Dir.Left, count * DirCount);
		Data.Set(ENUM_Dir.Right, count * DirCount);
	}
	// 取得下個方向
	public ENUM_Dir Next()
	{
		ENUM_Dir emDir = Data.Roll();
		
		foreach(KeyValuePair<ENUM_Dir, int> Itor in Data)
		{
			if(Itor.Key == emDir)
				Data.Set(Itor.Key, Itor.Value - Math.Max(0, DirCount - 1));
			else
				Data.Set(Itor.Key, Itor.Value + 1);
		}//for
		
		return emDir;
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

	public MapCoor Pos = new MapCoor();
	public MapCoor Next = new MapCoor();
	public char Mode = ModeNormal;

	public MapBlock(MapCoor pos, char mode)
	{
		Pos = pos;
		Mode = mode;
	}
}

// 地圖資料類別
public class MapData : IEnumerable
{
	private Dictionary<MapCoor, MapBlock> Data = new Dictionary<MapCoor, MapBlock>();
	private int SizeX = 0;
	private int SizeY = 0;

	public IEnumerator GetEnumerator()
	{
		return Data.GetEnumerator();
	}
	// 新增地圖格
	public MapBlock Add(int iX, int iY, char Mode)
	{
		MapCoor Pos = new MapCoor(iX, iY);
		MapBlock Result = null;
		
		if(Data.ContainsKey(Pos))
			Result = Data[Pos];
		else
		{
			SizeX = Math.Max(SizeX, iX + 1);
			SizeY = Math.Max(SizeY, iY + 1);
			Result = Data[Pos] = new MapBlock(Pos, Mode);
		}
		
		return Result;
	}
	// 取得地圖格
	public MapBlock Get(int iX, int iY)
	{
		MapCoor Pos = new MapCoor(iX, iY);

		return Data.ContainsKey(Pos) ? Data[Pos] : null;
	}
	// 取得地圖X軸長度
	public int GetSizeX()
	{
		return SizeX;
	}
	// 取得地圖Y軸長度
	public int GetSizeY()
	{
		return SizeY;
	}
}

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	public const int Width = 80;
	public const int Border = 10;
	public const int Interval = 7;
	public const int RoadBase = 200;
	public const int RoadAdd = 2;

	// 建立地圖
	public static MapData Create(int iStage)
	{
		int iRoad = RoadBase + iStage / 5 * RoadAdd; // 道路長度
		MapDir Dir = new MapDir(iRoad);
		MapData Result = new MapData();

		MapBlock Last = Result.Add(UnityEngine.Random.Range(Border, Width - Border), Border, MapBlock.ModeStart); // 新增起點
		MapBlock Next = null;

		// 產生道路
		while(iRoad > 0)
		{

		}//while

		return Result;
	}
}