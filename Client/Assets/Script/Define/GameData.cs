using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour 
{
    static public GameData pthis = null;

	/* Save */
	public List<MapRoad> RoadList = new List<MapRoad>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表

	/* Not Save */
	public int iStyle = 1; // 風格編號
	public float fRunDouble = 1.0f; // 跑步速度倍率.

    void Awake()
    {
        pthis = this;
    }

    // 讀檔.

    // 存檔.

}
