using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Looks
{

}

public class Member
{
	/* [Save] */ public PlayerLooks Looks = new PlayerLooks(); // 外型資料 [Save]
	/* [Save] */ public int iEquip = 0; // 裝備編號
	/* [Save] */ public List<int> Feature = new List<int>(); // 特性列表
	/* [Save] */ public List<int> Behavior = new List<int>(); // 行為列表
	/* [    ] */ public int iInvincibleTime = 0; // 無敵時間
	/* [    ] */ public float fCriticalStrike = 0.0f; // 致命
	/* [    ] */ public int iAddDamage = 0; // 增傷
}

public class PlayerData
{
	/* [Save] */ public int iStage = 0; // 關卡編號
	/* [    ] */ public int iPressure = 0; // 壓力
	/* [    ] */ public int iStamina = 0; // 耐力
	/* [    ] */ public int iStaminaLimit = 0; // 耐力上限
	/* [    ] */ public int iStaminaRecovery = 0; // 耐力回復
	/* [Save] */ public int iBattery = 0; // 電池
	/* [Save] */ public int iLightAmmo = 0; // 輕型彈藥
	/* [Save] */ public int iHeavyAmmo = 0; // 重型彈藥
	/* [Save] */ public List<PlayerMember> Member = new List<PlayerMember>(); // 成員列表
}