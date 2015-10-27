using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = false;

    // 跑步是否為冷卻中
    public bool bCanRun = true;

    public Color[] ColorLv = new Color[7]; 

    // 人物佇列.
    public Dictionary<GameObject, int> Role = new Dictionary<GameObject, int>();
    // 死亡人物佇列.
    public List<int> DeadRole = new List<int>();
    // 敵人佇列.
    public Dictionary<GameObject, int> Enemy = new Dictionary<GameObject, int>();
    // 可打敵人佇列.
    public Dictionary<GameObject, int> AtkEnemy = new Dictionary<GameObject, int>();    

    bool bIsOld = true;

    public float fSaveTime = 0;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    public void ReadyStart()
	{
		// 讀取資料
		DataAchievement.pthis.Load();
		DataCollection.pthis.Load();
		DataRecord.pthis.Load();
		DataReward.pthis.Load();

        // 讀取遊戲
		bIsOld &= DataEnemy.pthis.Load();
		bIsOld &= DataGame.pthis.Load();
		bIsOld &= DataMap.pthis.Load();
		bIsOld &= DataPickup.pthis.Load();
        bIsOld &= DataPlayer.pthis.Load();

        if (!bIsOld)
            CreateNew();
    }
    // ------------------------------------------------------------------
	void Update () 
    {
        // 記錄遊戲時間.
        if (bIsGaming && fSaveTime <= Time.time)
        {
            DataGame.pthis.iStageTime += GameDefine.iSaveSec;
            fSaveTime = Time.time + GameDefine.iSaveSec;

            SaveGame();
        }

        // 沒有玩家資料就算失敗了.
        if (bIsGaming && DataPlayer.pthis.MemberParty.Count <= DeadRole.Count)
            Failed();

        // 檢查是否所有成員都被魅惑.
        foreach(KeyValuePair<GameObject, int> itor in Role)
        {
            if (!itor.Key.GetComponent<PlayerCharm>())
                return;            
        }
        if (bIsGaming)
            Failed();
	}
    // ------------------------------------------------------------------
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
	// ------------------------------------------------------------------
	// 儲存遊戲.
	public void SaveGame()
	{
		DataCollection.pthis.Save();
		DataRecord.pthis.Save();
		DataReward.pthis.Save();
		DataEnemy.pthis.Save();
		DataGame.pthis.Save();
		DataPickup.pthis.Save();
		DataPlayer.pthis.Save();
	}
    // ------------------------------------------------------------------
    // 確認存檔內容開始遊戲.
    public void CheckStart()
    {
        if (bIsOld)
            OldStage();
        else
            NewStage();
    }
    // ------------------------------------------------------------------
    // 進入全新遊戲.
    public void CreateNew()
    {
        NewRoleData();
        // 建立新地圖資料.
        MapCreater.pthis.Create();
    }
    // ------------------------------------------------------------------
    // 建立舊關卡.
    public void OldStage()
    {
        // 選音樂.
        AudioCtrl.pthis.RedomMusic();

        bCanRun = true;
        // 重新計算數值.
        DataPlayer.pthis.iStaminaLimit = Rule.StaminaLimit();
        Rule.StaminaRecovery();
        Rule.CriticalStrikeReset();
        Rule.AddDamageReset();
		DataGame.pthis.fRunDouble = 1.0f;
		
		// 建立地圖物件.
		MapCreater.pthis.Show(DataGame.pthis.iRoad);
		// 建立撿取物件.
		PickupCreater.pthis.Show(DataGame.pthis.iRoad);

        // UI初始化.
        P_UI.pthis.StartNew();

        // 鏡頭位置調整.
        CameraCtrl.pthis.StartNew();

        // 新遊戲 - 淡出淡入天數後開始遊戲.
        SysUI.pthis.ShowDay();

		Statistics.pthis.ResetResource();
		Statistics.pthis.ResetDamage();

        // 到數開始.
        StartCoroutine(CountStart(true));

        bIsOld = false;
    }
    // ------------------------------------------------------------------
    // 建立新關卡.
    public void NewStage()
    {
        System.GC.Collect();

        // 選音樂.
        AudioCtrl.pthis.RedomMusic();
        // 清空遊戲資料.
        DataGame.pthis.Clear();
		DataPickup.pthis.Clear();
        // 清空物件.
        ClearObj();

        // 重置跑步旗標.
        bCanRun = true;
        // 重新計算數值.
        DataPlayer.pthis.iStaminaLimit = Rule.StaminaLimit();
        Rule.StaminaReset();
        Rule.StaminaRecovery();
        Rule.CriticalStrikeReset();
        Rule.AddDamageReset();
		Rule.BombReset();
		Rule.ShieldReset();
        Rule.RandomCollect();

 		DataGame.pthis.fRunDouble = 1.0f;

        // 選擇關卡風格編號.
		DataPlayer.pthis.iStyle = Tool.RandomPick(GameDefine.StageStyle);
        // 建立地圖資料.
        MapCreater.pthis.Create();
        // 建立地圖物件.
        MapCreater.pthis.Show(0);
		// 建立撿取資料.
		PickupCreater.pthis.Create();
		// 建立撿取物件.
		PickupCreater.pthis.Show(0);

        DataMap.pthis.Save();

        // UI初始化.
        P_UI.pthis.StartNew();

        // 鏡頭位置調整.
        CameraCtrl.pthis.StartNew();

        // 新遊戲 - 淡出淡入天數後開始遊戲.
        SysUI.pthis.ShowDay();

		Statistics.pthis.ResetResource();
		Statistics.pthis.ResetDamage();

        // 到數開始.
        StartCoroutine(CountStart(false));
    }
    // ------------------------------------------------------------------
    // 新遊戲資料.
    public void NewRoleData()
    {
        DataPlayer.pthis.iStage = 1;
        DataPlayer.pthis.iStyle = Tool.RandomPick(GameDefine.StageStyle);
        DataPlayer.pthis.iCurrency = 0;
		DataPlayer.pthis.iBattery = 0;
		DataPlayer.pthis.iLightAmmo = 0;
		DataPlayer.pthis.iHeavyAmmo = 0;
		DataPlayer.pthis.iBomb = 0;
		DataPlayer.pthis.iStamina = 0;
		DataPlayer.pthis.iDamageLv = 0;
        DataPlayer.pthis.iEnemyKill = 0;
        DataPlayer.pthis.iPlayTime = 0;
		DataPlayer.pthis.iAdsWatch = 0;
        DataPlayer.pthis.MemberParty = new List<Member>();

        // 給與初始資源
		Rule.CurrencyAdd(DataReward.pthis.iInitCurrency);
		Rule.BatteryAdd(DataReward.pthis.iInitBattery);
		Rule.LightAmmoAdd(DataReward.pthis.iInitLightAmmo);
		Rule.HeavyAmmoAdd(DataReward.pthis.iInitHeavyAmmo);
		Rule.BombAdd(DataReward.pthis.iInitBomb);

        // 給與初始隊員
		Rule.MemberPartyAdd(1);
		Rule.MemberPartyAdd(3);

		// 給予初始角色庫
		foreach(int Itor in DataReward.pthis.MemberInits)
			Rule.MemberDepotAdd(Itor);
    }
    // ------------------------------------------------------------------
    IEnumerator CountStart(bool bShowCount)
    {
        int iCount = 3;
        while (iCount > 0)
        {
            if (bShowCount)
            { }
            yield return new WaitForSeconds(1);
            iCount--;
        }
        bIsGaming = true;
        P_UI.pthis.StartRecoverSta();
        // 創建人物.
        if (bShowCount)
            PlayerCreater.pthis.CreateByRoad(DataGame.pthis.iRoad);
        else
            PlayerCreater.pthis.StartNew();
        // 開始出怪.
        if (bShowCount)  // 復原舊怪物.            
            EnemyCreater.pthis.CreateOldEnemy();
        EnemyCreater.pthis.StartNew();
    }
    // ------------------------------------------------------------------
    // 取得真實跑速
    public float GetMoveSpeed()
    {
        return GameDefine.fBaseSpeed * DataGame.pthis.fRunDouble;
    }
    // ------------------------------------------------------------------
    // 增加耐力.
    public bool AddStamina(int iValue)
    {
        if (iValue < 0 && !bCanRun)
            return false;

        // 當扣除後低於0進入冷卻狀態.
        if (bCanRun && DataPlayer.pthis.iStamina + iValue <= 0)
        {
            DataPlayer.pthis.iStamina = 0;
            DataGame.pthis.fRunDouble = 0;
            bCanRun = false;
            P_UI.pthis.UpdateStamina();
            return true;
        }
        
        Rule.StaminaAdd(iValue);

        if (iValue > 0 && DataPlayer.pthis.iStamina == DataPlayer.pthis.iStaminaLimit)
            AllRoleTalk("Run");

        // 冷卻後須等待耐力回復至10%才能移動.
		if (iValue > 0 && !bCanRun && DataPlayer.pthis.iStamina + iValue > GameDefine.iStaminaMoveable)
        {
            bCanRun = true;
            DataGame.pthis.fRunDouble = 1.0f;
        }

        P_UI.pthis.UpdateStamina();
        return true;
    }
    // ------------------------------------------------------------------
    public void Victory()
    {
        bIsGaming = false;

		List<Member> NewMember = new List<Member>();

		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
		{
			if(DeadRole.Contains(iPos) == false)
				NewMember.Add(DataPlayer.pthis.MemberParty[iPos]);
		}//for

		DataPlayer.pthis.MemberParty = NewMember;
        DataPlayer.pthis.iPlayTime += DataGame.pthis.iStageTime;
        DataPlayer.pthis.iEnemyKill += DataGame.pthis.iKill;
        DataPlayer.pthis.iPlayerLost += DataGame.pthis.iDead;

        // 檢查成就.
        SysAchieve.pthis.UpdateReplace(ENUM_Achievement.Single_Stage, DataPlayer.pthis.iStage);
        SysAchieve.pthis.UpdateReplace(ENUM_Achievement.Total_Stage, DataPlayer.pthis.iStage);
        SysAchieve.pthis.UpdateTotal(ENUM_Achievement.Total_Kill, DataGame.pthis.iKill);

        SaveGame();

        EnemyCreater.pthis.StopCreate();

        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_Victory");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);
    }
    // ------------------------------------------------------------------
    public void Failed()
    {
        bIsGaming = false;

        DataPlayer.pthis.iPlayTime += DataGame.pthis.iStageTime;
        DataPlayer.pthis.iEnemyKill += DataGame.pthis.iKill;
        DataPlayer.pthis.iPlayerLost += DataGame.pthis.iDead;

        // 檢查成就.
        SysAchieve.pthis.UpdateReplace(ENUM_Achievement.Single_Stage, DataPlayer.pthis.iStage);
        SysAchieve.pthis.UpdateReplace(ENUM_Achievement.Total_Stage, DataPlayer.pthis.iStage);
        SysAchieve.pthis.UpdateTotal(ENUM_Achievement.Total_Kill, DataGame.pthis.iKill);

        EnemyCreater.pthis.StopCreate();

        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_Failed");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);

        ClearObj();
    }
    // ------------------------------------------------------------------
    void ClearObj()
    {
        // 刪除人物.
        foreach (KeyValuePair<GameObject, int> itor in Role)
            Destroy(itor.Key);
        // 清空人物佇列.
        Role.Clear();
        ToolKit.ClearCatchRole();
        DeadRole.Clear();
        // 刪除待救人物.
        PlayerCreater.pthis.ClearList();

        // 刪除 敵人.
        foreach (KeyValuePair<GameObject,int> itor in Enemy)
            Destroy(itor.Key);
        // 清空 敵人佇列.
        Enemy.Clear();
        AtkEnemy.Clear();
    }
    // ------------------------------------------------------------------
    public void AllRoleTalk(string pTalk)
    {
        int iCount = 0;

        foreach(KeyValuePair<GameObject, int> itor in Role)
        {
            int iRandom = Random.Range(0, 100);
            if (iCount == 0 && itor.Key.GetComponent<AIPlayer>())
                itor.Key.GetComponent<AIPlayer>().RoleTalk(false, pTalk, iCount);
            else if(iRandom < 45)
                itor.Key.GetComponent<AIPlayer>().RoleTalk(true, pTalk, iCount);
         
            iCount++;
        }
    }
}