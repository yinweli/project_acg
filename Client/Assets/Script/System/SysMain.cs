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

    // 人物佇列.
    public Dictionary<GameObject, int> Role = new Dictionary<GameObject, int>();
    // 可抓人物佇列.
    public Dictionary<GameObject, int> CatchRole = new Dictionary<GameObject, int>();
    // 死亡人物佇列.
    public List<int> DeadRole = new List<int>();
    // 敵人佇列.
    public List<GameObject> Enemy = new List<GameObject>();
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
    void Start()
    {
        // 讀取遊戲
        bIsOld &= PlayerData.pthis.Load();
        bIsOld &= GameData.pthis.Load();

        if (!bIsOld)
            CreateNew();

        // 建立遊戲開頭畫面.
        SysUI.pthis.CreatePanel("Prefab/P_Login");        
    }
    // ------------------------------------------------------------------
	void Update () 
    {
        // 記錄遊戲時間.
        if (bIsGaming && fSaveTime <= Time.time)
        {
            GameData.pthis.iStageTime += GameDefine.iSaveSec;
            fSaveTime = Time.time + GameDefine.iSaveSec;

            SaveGame();
        }

        // 沒有玩家資料就算失敗了.
        if (bIsGaming && PlayerData.pthis.Members.Count <= DeadRole.Count)
            Failed();
	}
	// ------------------------------------------------------------------
	// 儲存遊戲.
	public void SaveGame()
	{
		PlayerData.pthis.Save();
		GameData.pthis.Save();
		PlayerPrefs.Save();
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

        Debug.Log("Old Game");

        bCanRun = true;
        // 重新計算數值.
        PlayerData.pthis.iStaminaLimit = Rule.StaminaLimit();
        Rule.StaminaRecovery();
        Rule.CriticalStrikeReset();
        Rule.AddDamageReset();
		
		// 建立地圖物件.
		MapCreater.pthis.ShowMap(GameData.pthis.iRoad);
		// 建立撿取物件.
		MapCreater.pthis.ShowPickup(GameData.pthis.iRoad);

        // UI初始化.
        P_UI.pthis.StartNew();

        // 鏡頭位置調整.
        CameraCtrl.pthis.StartNew();

        // 到數開始.
        StartCoroutine(CountStart(true));

        bIsOld = false;
    }
    // ------------------------------------------------------------------
    // 建立新關卡.
    public void NewStage()
    {
        // 選音樂.
        AudioCtrl.pthis.RedomMusic();
        Debug.Log("New Game");
        // 清空遊戲資料.
        GameData.pthis.ClearData();
        // 清空物件.
        ClearObj();

        // 重置跑步旗標.
        bCanRun = true;
        // 重新計算數值.
        PlayerData.pthis.iStaminaLimit = Rule.StaminaLimit();
        Rule.StaminaReset();
        Rule.StaminaRecovery();
        Rule.CriticalStrikeReset();
        Rule.AddDamageReset();

        // 選擇關卡風格編號.
		PlayerData.pthis.iStyle = Tool.RandomPick(GameDefine.StageStyle);
        // 建立新地圖資料.
        MapCreater.pthis.Create();
        // 建立地圖物件.
        MapCreater.pthis.ShowMap(0);
		// 建立撿取物件.
		MapCreater.pthis.CreatePickup();
		// 建立撿取物件.
		MapCreater.pthis.ShowPickup(0);

        // UI初始化.
        P_UI.pthis.StartNew();

        // 鏡頭位置調整.
        CameraCtrl.pthis.StartNew();

        // 新遊戲 - 淡出淡入天數後開始遊戲.
        SysUI.pthis.ShowDay();

        // 到數開始.
        StartCoroutine(CountStart(false));
    }
    // ------------------------------------------------------------------
    // 新遊戲資料.
    public void NewRoleData()
    {
        PlayerData.pthis.iStage = 1;
        PlayerData.pthis.iStyle = Tool.RandomPick(GameDefine.StageStyle);
        PlayerData.pthis.iCurrency = 0;
        PlayerData.pthis.iEnemyKill = 0;
        PlayerData.pthis.iPlayTime = 0;
        PlayerData.pthis.Resource = new List<int>();
        PlayerData.pthis.Members = new List<Member>();

        // 給與初始資源
        Rule.ResourceAdd(ENUM_Resource.Battery, GameDefine.iInitBattery);
        Rule.ResourceAdd(ENUM_Resource.LightAmmo, GameDefine.iInitLightAmmo);
        Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, GameDefine.iInitHeavyAmmo);

        // 以下是測試資料, 以後要改
        Rule.MemberAdd(1);
        Rule.MemberAdd(5);
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
            PlayerCreater.pthis.CreateByRoad(GameData.pthis.iRoad);
        else
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

        // 冷卻後須等待耐力回復至10%才能移動.
        if (iValue > 0 && !bCanRun && PlayerData.pthis.iStamina + iValue > PlayerData.pthis.iStaminaLimit / 10)
        {
            bCanRun = true;
            GameData.pthis.fRunDouble = 1.0f;
        }

        P_UI.pthis.UpdateStamina();
        return true;
    }
    // ------------------------------------------------------------------
    public void Victory()
    {
        bIsGaming = false;

		List<Member> NewMember = new List<Member>();

		for(int iPos = 0; iPos < PlayerData.pthis.Members.Count; ++iPos)
		{
			if(DeadRole.Contains(iPos) == false)
				NewMember.Add(PlayerData.pthis.Members[iPos]);
		}//for

        PlayerData.pthis.iPlayTime += GameData.pthis.iStageTime;
        PlayerData.pthis.iEnemyKill += GameData.pthis.iKill;

		PlayerData.pthis.Members = NewMember;
        SaveGame();

        EnemyCreater.pthis.StopCreate();

        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_Victory");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);
    }
    // ------------------------------------------------------------------
    public void Failed()
    {
        bIsGaming = false;

        PlayerData.pthis.iPlayTime += GameData.pthis.iStageTime;
        PlayerData.pthis.iEnemyKill += GameData.pthis.iKill;

        EnemyCreater.pthis.StopCreate();

        // 比較紀錄.        

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
        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
            Destroy(itor.Key);
        // 清空人物佇列.
        Role.Clear();
        CatchRole.Clear();
        DeadRole.Clear();
        // 刪除待救人物.
        PlayerCreater.pthis.ClearList();

        // 刪除 敵人.
        foreach (GameObject itor in Enemy)
            Destroy(itor);
        // 清空 敵人佇列.
        Enemy.Clear();
        AtkEnemy.Clear();
    }
}