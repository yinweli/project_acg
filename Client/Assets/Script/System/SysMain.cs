using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = true;

    public PlayerData Data = new PlayerData();

    public int iRoleCount = 0;
    public int iEnemyCount = 0;
    // 跑步速度倍率.
    public float fRunDouble = 1;
    // 耐力消耗值.
    public int iStaminaCost= 5;
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
    public float GetMoveSpeed()
    {
        return GameDefine.fBaseSpeed * fRunDouble;
    }
    // ------------------------------------------------------------------
    // 增加耐力.
    public bool AddStamina(int iValue)
    {
        if (iValue < 0 && !bCanRun)
            return false;

        // 當扣除後低於0進入冷卻狀態.
        if (bCanRun && SysMain.pthis.Data.iStamina + iValue <= 0)
        {
            SysMain.pthis.Data.iStamina = 0;
            SysMain.pthis.fRunDouble = 0;
            bCanRun = false;
            P_UI.pthis.UpdateStamina();
            return true;
        }
        
        Rule.StaminaAdd(iValue);

        if (iValue > 0 && !bCanRun && SysMain.pthis.Data.iStamina + iValue > 20)
            bCanRun = true;
        if (iValue > 0 && !bCanRun && SysMain.pthis.Data.iStamina + iValue > 10)
            SysMain.pthis.fRunDouble = 1.0f;

        P_UI.pthis.UpdateStamina();
        return true;
    }
}