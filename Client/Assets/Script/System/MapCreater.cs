﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 建立地圖類別
public class MapCreater : MonoBehaviour
{
	public static MapCreater pthis = null;

	private Dictionary<Vector2, GameObject> Data = new Dictionary<Vector2, GameObject>();

	void Awake()
	{
		pthis = this;
	}
	// 地圖物件覆蓋檢查
	private bool CheckCover(MapObjt Objt)
	{
		if(DataMap.pthis.DataObjt.ContainsKey(Objt.Pos.ToVector2()))
			return false;

		for(int iX = -2; iX <= 2; ++iX)
		{
			for(int iY = -1; iY <= 1; ++iY)
			{
				Vector2 Pos = (new MapCoor(Objt.Pos.X + iX, Objt.Pos.Y + iY)).ToVector2();

				if(DataMap.pthis.DataObjt.ContainsKey(Pos) && Objt.Cover(DataMap.pthis.DataObjt[Pos]))
					return false;
			}//for
		}//for

		return true;
	}
	// 建立地圖道路
	private void CreateRoad()
	{
		int iRoadSize = GameDefine.iRoadSizeBase + (int)(DataPlayer.pthis.iStage * GameDefine.fUpgradeRoad); // 取得地圖道路長度
		ENUM_Dir emGrowDir = ENUM_Dir.Null;
		int iGrowLength = 0;
		CDice<ENUM_Dir> DirDiceUp = new CDice<ENUM_Dir>();
		CDice<ENUM_Dir> DirDiceLeft = new CDice<ENUM_Dir>();
		CDice<ENUM_Dir> DirDiceRight = new CDice<ENUM_Dir>();

		DirDiceUp.Set(ENUM_Dir.Up, 2);
		DirDiceUp.Set(ENUM_Dir.Left, 3);
		DirDiceUp.Set(ENUM_Dir.Right, 3);
		DirDiceLeft.Set(ENUM_Dir.Up, 3);
		DirDiceLeft.Set(ENUM_Dir.Left, 2);
		DirDiceRight.Set(ENUM_Dir.Up, 3);
		DirDiceRight.Set(ENUM_Dir.Right, 2);

		while(iRoadSize > 0)
		{
			// 決定成長方向與長度
			if(emGrowDir == ENUM_Dir.Null)
			{
				emGrowDir = ENUM_Dir.Up;
				iGrowLength = GameDefine.iPathStart;
			}
			else
			{
				if(emGrowDir == ENUM_Dir.Up)
					emGrowDir = DirDiceUp.Roll();
				else if(emGrowDir == ENUM_Dir.Left)
					emGrowDir = DirDiceLeft.Roll();
				else if(emGrowDir == ENUM_Dir.Right)
					emGrowDir = DirDiceRight.Roll();
				else
					emGrowDir = ENUM_Dir.Up;

				iGrowLength = Random.Range(GameDefine.iPathMin, GameDefine.iPathMax);
			}//if

			// 新增道路
			while(iGrowLength > 0 && iRoadSize > 0)
			{
				MapCoor Pos = null;

				if(DataMap.pthis.DataRoad.Count <= 0)
					Pos = new MapCoor(Random.Range(GameDefine.iMapRoadXMin, GameDefine.iMapRoadXMax), GameDefine.iMapBorder);
				else
					Pos = DataMap.pthis.DataRoad[DataMap.pthis.DataRoad.Count - 1].Add(emGrowDir, 1);

				if(Pos.X >= GameDefine.iMapRoadXMin && Pos.X <= GameDefine.iMapRoadXMax && Pos.Y >= GameDefine.iMapBorder)
				{
					DataMap.pthis.DataRoad.Add(Pos);
					--iGrowLength; // 減少成長次數
					--iRoadSize; // 減少還需要產生的地圖道路長度
				}
				else
					iGrowLength = 0;
			}//while
		}//while
	}
	// 建立地圖物件
	private void CreateObjt()
	{
		// 建立道路物件
		for(int iPos = 0; iPos < DataMap.pthis.DataRoad.Count; ++iPos)
		{
			MapCoor Pos = DataMap.pthis.DataRoad[iPos];

			// 判斷是否該建立起點物件
			if(iPos == 0)
			{
				MapObjt Temp = new MapObjt();

				Temp.Pos = new MapCoor(Pos.X, Pos.Y - 1);
				Temp.Type = (int)ENUM_Map.MapStart;
				Temp.Width = GameDefine.ObjtStart.X;
				Temp.Height = GameDefine.ObjtStart.Y;

				DataMap.pthis.DataObjt.Add(Temp.Pos.ToVector2(), Temp);
			}//if

			// 判斷是否該建立終點物件
			if(iPos == DataMap.pthis.DataRoad.Count - 1)
			{
				MapObjt Temp = new MapObjt();
				
				Temp.Pos = new MapCoor(Pos.X - 1, Pos.Y + 1);
				Temp.Type = (int)ENUM_Map.MapEnd;
				Temp.Width = GameDefine.ObjtEnd.X;
				Temp.Height = GameDefine.ObjtEnd.Y;
				
				DataMap.pthis.DataObjt.Add(Temp.Pos.ToVector2(), Temp);
			}//if

			// 建立道路物件
			{
				MapObjt Temp = new MapObjt();
				
				Temp.Pos = new MapCoor(Pos.X, Pos.Y);
				Temp.Type = (int)ENUM_Map.MapRoad;
				Temp.Width = GameDefine.ObjtBase.X;
				Temp.Height = GameDefine.ObjtBase.Y;
				
				DataMap.pthis.DataObjt.Add(Temp.Pos.ToVector2(), Temp);
			}
		}//for

		// 建立一般物件
		foreach(MapCoor Itor in DataMap.pthis.DataRoad)
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
						
						if(Pos.X >= 0 && Pos.Y >= 0)
						{
							int iIndex = Random.Range(0, GameDefine.ObjtScale.Count);
							MapObjt Temp = new MapObjt();

							Temp.Pos = Pos;
							Temp.Type = iIndex + (int)ENUM_Map.MapObjt_0;
							Temp.Width = GameDefine.ObjtScale[iIndex].X;
							Temp.Height = GameDefine.ObjtScale[iIndex].Y;

							if(CheckCover(Temp))
								DataMap.pthis.DataObjt.Add(Temp.Pos.ToVector2(), Temp);
						}//if
					}//if
					
					iProb -= GameDefine.iObjtDec;
					iInterval++;
				}//while
			}//for
		}//for

		// 填滿地圖
		foreach(MapCoor Itor in DataMap.pthis.DataRoad)
		{
			for(int iX = -GameDefine.iMapBorder; iX <= GameDefine.iMapBorder; ++iX)
			{
				for(int iY = -GameDefine.iMapBorder; iY <= GameDefine.iMapBorder; ++iY)
				{
					MapCoor Pos = new MapCoor(Itor.X + iX, Itor.Y + iY);

					if(Pos.X < 0 || Pos.Y < 0)
						continue;

					if(DataMap.pthis.DataObjt.ContainsKey(Pos.ToVector2()))
					   	continue;

					MapObjt Temp = new MapObjt();
					
					Temp.Pos = Pos;
					Temp.Type = (int)ENUM_Map.MapBase;
					Temp.Width = GameDefine.ObjtBase.X;
					Temp.Height = GameDefine.ObjtBase.Y;

					if(CheckCover(Temp))
						DataMap.pthis.DataObjt.Add(Temp.Pos.ToVector2(), Temp);
				}//for
			}//for
		}//for
	}
	// 建立地圖
	public void Create()
	{
		Debug.Log("Create Map");

		Clear();
		CreateRoad();
		CreateObjt();		
	}
	// 更新地圖
	public void Refresh(int iRoad)
	{
		MapCoor RoadPos = DataMap.pthis.DataRoad.Count > iRoad ? DataMap.pthis.DataRoad[iRoad] : new MapCoor();
		//HashSet<Vector2> InvalidPos = new HashSet<Vector2>(Data.Keys);

		// 建立新物件
		for(int iX = -GameDefine.iMapBorder; iX <= GameDefine.iMapBorder; ++iX)
		{
			for(int iY = -GameDefine.iMapBorder; iY <= GameDefine.iMapBorder; ++iY)
			{
				MapCoor Pos = new MapCoor(RoadPos.X + iX, RoadPos.Y + iY);

				if(Pos.X < 0  || Pos.Y < 0)
					continue;

				Vector2 PosVec = Pos.ToVector2();

				//InvalidPos.Remove(PosVec);

				if(Data.ContainsKey(PosVec))
					continue;

				if(DataMap.pthis.DataObjt.ContainsKey(PosVec) == false)
					continue;

				MapObjt pObjt = DataMap.pthis.DataObjt[PosVec];

				float fPosX = PosVec.x + (pObjt.Width * GameDefine.iBlockSize) / 2;
				float fPosY = PosVec.y + (pObjt.Height * GameDefine.iBlockSize) / 2;

				Data.Add(PosVec, UITool.pthis.CreateMap(gameObject, ((ENUM_Map)pObjt.Type).ToString(), DataPlayer.pthis.iStyle, fPosX, fPosY));
			}//for
		}//for
		/*
		// 刪除不需要物件
		foreach(Vector2 Itor in InvalidPos)
		{
			if(Data.ContainsKey(Itor))
			{
				Destroy(Data[Itor]);
				Data.Remove(Itor);
			}//if
		}//for*/
	}
	// 顯示地圖
	public void Show(int iRoad)
	{
		Refresh(iRoad);
		transform.localPosition = new Vector3(-GetRoadObj(iRoad).transform.localPosition.x, -GetRoadObj(iRoad).transform.localPosition.y, 0);
	}
	// 清除地圖
	public void Clear()
	{
		foreach(KeyValuePair<Vector2, GameObject> Itor in Data)
			Destroy(Itor.Value);
		
		Data.Clear();
		DataMap.pthis.DataRoad.Clear();
		DataMap.pthis.DataObjt.Clear();
	}
	// 取得道路物件
    public GameObject GetRoadObj(int iRoad)
	{
		if(iRoad < 0)
			iRoad = 0;

		if(DataMap.pthis.DataRoad.Count <= iRoad)
			return null;

		Vector2 Pos = DataMap.pthis.DataRoad[iRoad].ToVector2();

		if(Data.ContainsKey(Pos) == false)
			return null;

		return Data[Pos];
	}
	// 取得物件數量
	public int ObjectCount()
	{
		return Data.Count;
	}
}