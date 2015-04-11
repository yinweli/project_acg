using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = false;

    // 角色數量
    public int iRoleCount = 0;
	// 怪物數量
    public int iEnemyCount = 0;
    // 跑步是否為冷卻中
    public bool bCanRun = true;

    // 人物佇列.
    public Dictionary<GameObject, int> Role = new Dictionary<GameObject, int>();
    // 敵人佇列.
    public Dictionary<GameObject, int> Enemy = new Dictionary<GameObject, int>();
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
		GameLoad.Load();
    }

    void OnDestroy()
    {
        //pthis = null;
    }
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        iRoleCount = Role.Count;
        iEnemyCount = Enemy.Count;
	}
    // ------------------------------------------------------------------
    // 準備開始遊戲.
    public void ReadyStart()
    {
        // 建立地圖.
        MapCreater.pthis.Create();
        MapMove.pthis.StartNew();
        // 確認是否為新遊戲.

        // 新遊戲 - 淡出淡入天數後開始遊戲.
        SysUI.pthis.ShowDay();

        NewGame();
    }
    // ------------------------------------------------------------------
    // 開始遊戲.
    public void NewGame()
    {
        bIsGaming = true;
        // 創建人物.
        PlayerCreater.pthis.StartNew();
        // 開始出怪.
        EnemyCreater.pthis.StartNew();
        
    }
    // ------------------------------------------------------------------
    // 取得真實跑速
    public float GetMoveSpeed()
    {
        return GameDefine.fBaseSpeed * GameData.pthis.fRunDouble;
    }
    // ------------------------------------------------------------------
    // 增加耐力.
    public bool AddStamina(int iValue)
    {
        if (iValue < 0 && !bCanRun)
            return false;

        // 當扣除後低於0進入冷卻狀態.
        if (bCanRun && PlayerData.pthis.iStamina + iValue <= 0)
        {
            PlayerData.pthis.iStamina = 0;
            GameData.pthis.fRunDouble = 0;
            bCanRun = false;
            P_UI.pthis.UpdateStamina();
            return true;
        }
        
        Rule.StaminaAdd(iValue);

        if (iValue > 0 && !bCanRun && PlayerData.pthis.iStamina + iValue > 20)
            bCanRun = true;
        if (iValue > 0 && !bCanRun && PlayerData.pthis.iStamina + iValue > 10)
            GameData.pthis.fRunDouble = 1.0f;

        P_UI.pthis.UpdateStamina();
        return true;
    }
}