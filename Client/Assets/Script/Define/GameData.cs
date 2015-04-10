using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour 
{
    static public GameData pthis = null;
    // 地圖樣式.
    public int iStyle = 1;
    // 跑步速度倍率.
    public float fRunDouble = 1;
    // 耐力消耗值.
    public int iStaminaCost = 5;

    void Awake()
    {
        pthis = this;
    }

    // 讀檔.

    // 存檔.

}
