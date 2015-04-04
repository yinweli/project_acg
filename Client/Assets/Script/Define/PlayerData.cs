using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Looks
{
	/* [Save] */ public int iHair = 0; // 頭髮
	/* [Save] */ public int iDecorative = 0; // 裝飾
	/* [Save] */ public int iFace = 0; // 臉部
	/* [Save] */ public int iEyes = 0; // 眼睛
	/* [Save] */ public int iEyeback = 0; // 眼白
	/* [Save] */ public int iEyebrow = 0; // 睫毛
	/* [Save] */ public int iBlush = 0; // 腮紅
	/* [Save] */ public int iMonth = 0; // 嘴巴
	/* [Save] */ public int iBody = 0; // 身體
	/* [Save] */ public int iFoot = 0; // 腳
	/* [Save] */ public int iHand = 0; // 手
	/* [    ] */ public int iEquip = 0; // 裝備
}

public class Member
{
	/* [Save] */ public Looks Looks = new Looks(); // 外型資料 [Save]
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
	/* [Save] */ public List<Member> Data = new List<Member>(); // 成員列表
}