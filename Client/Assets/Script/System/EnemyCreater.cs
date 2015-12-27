using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*  關卡有關卡編號、怪物間隔、怪物能量這三個屬性
 * 關卡編號
    表示這是第幾個關卡，同時也表示這個關卡的難度。
 * 怪物間隔
    每波怪物產生的間隔。
    每波怪物出現後就重新計算怪物間隔。
    怪物間隔為隨機值，最短為２０秒，最長為４０秒。
 * 怪物能量 = (能量基礎值 + 關卡編號 * 能量增加值) * 額外能量加成。
*/

public class EnemyCreater : MonoBehaviour
{
    public static EnemyCreater pthis = null;
    public Shader pShader;

    public GameObject ObjCamCtrl;
    // 波數能量.
    public int iEnegry = 0;

    int iCount = 0;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    // 開始新的關卡.
    public void StartNew()
    {
        iCount = 0;

        // 每5關為魔王關
		Debug.Log(Rule.AppearBossStage() ? "Boss Stage" : "Normal Stage");

		if(Rule.AppearBossStage())
            StartCoroutine(BossCreater());
        else
        {
            // 計算總波數能量：怪物能量 = (能量基礎值 + 關卡編號 * 能量增加值) * 額外能量加成
            iEnegry = (int)((GameDefine.iBaseEngry + (int)(DataPlayer.pthis.iStage * GameDefine.fUpgradeEnegry)) * (1.0f + Rule.FeatureF(ENUM_ModeFeature.AddEnegry)));

            StartCoroutine(Creater());
        }
    }
    // ------------------------------------------------------------------
    // 停止所有的協同程序.
    public void StopCreate()
    {
        StopAllCoroutines();
    }
    // ------------------------------------------------------------------
    // 敵人佇列產生函式.
    public void ListEnemyCreater(List<int> EnemyList)
    {
        // 先清理List.
        EnemyList.Clear();

		if (SysMain.pthis.bIsGaming == false)
			return;

        // 取得可使用的怪物列表.
        List<int> pEnemy = Rule.MonsterList();
        
        int iTempEnegry = iEnegry;
		bool bRoadblock = false;
		bool bSuccubus = false;
		
		while (iTempEnegry > 0)
		{
			int iEnemy = LibCSNStandard.Tool.RandomPick(pEnemy);
			DBFMonster DBFData = GameDBF.pthis.GetMonster(iEnemy) as DBFMonster;
			
			if (DBFData == null)
			{
				Debug.Log("DBFMonster(" + iEnemy + ") null");
				return;
			}//if

			// 只產生一隻擋路怪
			if((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.NoMove)
			{
				if(bRoadblock == false)
					bRoadblock = true;
				else 
					continue;
			}//if

			// 只產生一隻魅魔
			if((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.Bewitch)
			{
				if(bSuccubus == false)
					bSuccubus = true;
				else 
					continue;
			}//if

			if (iTempEnegry > 0 && iTempEnegry <= 1)
			{
				iTempEnegry -= 1;
				EnemyList.Add(1);
			}

			if (iTempEnegry > 0 && iTempEnegry >= DBFData.Enegry)
			{
				iTempEnegry -= DBFData.Enegry;
				EnemyList.Add(iEnemy);
			}
		}
    }
    // ------------------------------------------------------------------
    // 產生一隻怪物.
    public GameObject CreateOneEnemy(int iMonster, int iHP, Vector2 vPos)
    {
        GameObject pEnemy = UITool.pthis.CreateUIByPos(gameObject, string.Format("Enemy/{0:000}", iMonster), vPos.x, vPos.y);

        if (iMonster == 61)
            pEnemy.transform.localPosition = new Vector2(pEnemy.transform.localPosition.x -18, pEnemy.transform.localPosition.y -50);

        if (pEnemy && pEnemy.GetComponent<AIEnemy>())
        {
            ToolKit.SetLayer(iCount, pEnemy.GetComponentsInChildren<SpriteRenderer>());
            pEnemy.GetComponent<AIEnemy>().iMonster = iMonster;
            pEnemy.GetComponent<AIEnemy>().iHP = iHP;
        }
        iCount++;

        return pEnemy;
    }
    // ------------------------------------------------------------------
    // 復原怪物.
    public void CreateOldEnemy()
    {
        foreach (SaveEnemy itor in DataEnemy.pthis.Data)
            CreateOneEnemy(itor.iMonster, itor.iHP, new Vector2(itor.fPosX + ObjCamCtrl.transform.localPosition.x, itor.fPosY + ObjCamCtrl.transform.localPosition.y));
    }
    // ------------------------------------------------------------------
    IEnumerator BossCreater()
    {
        yield return new WaitForSeconds(Random.Range(GameDefine.iMinWaitSec, GameDefine.iMaxWaitSec));
        while (SysMain.pthis.bIsGaming)
        {
            // 魔王還在不出怪.
            if (SysMain.pthis.Enemy.Count > 0)
                yield return new WaitForSeconds(0.1f);
            else
            {
                // 等待1.8秒後出新魔王.
                yield return new WaitForSeconds(1.8f);

				int iIndex = DataPlayer.pthis.iStage / GameDefine.iBossStage % GameDefine.iBossCount;

				if(iIndex == 0)
					iIndex = GameDefine.iBossCount;

                Vector2 vPos = new Vector2(ObjCamCtrl.transform.localPosition.x + Random.Range(-500.0f, 500.0f), ObjCamCtrl.transform.localPosition.y + Random.Range(380.0f, 450.0f));
                CreateOneEnemy(iIndex + 1000, -1, vPos);               
                yield return new WaitForSeconds(5.0f);
            }
        }
    }
    // ------------------------------------------------------------------
    // 偕同程序
    IEnumerator Creater()
    {
        while (SysMain.pthis.bIsGaming)
        {
            // 計算要出的敵人佇列.
            List<int> ListEnemy = new List<int>();
            ListEnemyCreater(ListEnemy);

            // 計算等待間隔.
			yield return new WaitForSeconds(Random.Range(GameDefine.iMinWaitSec, GameDefine.iMaxWaitSec));            
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                DBFMonster DBFData = GameDBF.pthis.GetMonster(ListEnemy[i]) as DBFMonster;
                // 如果是擋路怪要生在路上.
                if ((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.NoMove)
                    CreateByRoad(ListEnemy[i]);
                // 魅惑怪需要生在路附近.
                else if ((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.Bewitch)
                    CreateByStandOutRoad(ListEnemy[i], 7, 15);
                else
                    CreateByNormal(ListEnemy[i]);
                yield return new WaitForSeconds(0.05f);
            }
        }        
    }
    // ------------------------------------------------------------------
    void CreateByNormal(int iMonster)
    {
        Vector2 vPos = ObjCamCtrl.transform.localPosition;
        switch (Random.Range(1, 4))
        {
            case 1: //上方.
                vPos = new Vector2(vPos.x + Random.Range(-500.0f, 500.0f), vPos.y + Random.Range(380.0f, 450.0f));
                break;
            case 2: //左方.
                vPos = new Vector2(vPos.x + Random.Range(-470.0f, -520.0f), vPos.y + Random.Range(-300.0f, 400.0f));
                break;
            case 3: //右方.
                vPos = new Vector2(vPos.x + Random.Range(470.0f, 520.0f), vPos.y + Random.Range(-300.0f, 400.0f));
                break;
        }
        CreateOneEnemy(iMonster, -1, vPos);
    }
    // ------------------------------------------------------------------
    void CreateByRoad(int iMonster)
    {
        int iRoad = CameraCtrl.pthis.iNextRoad + Random.Range(7, 15);

        if (iRoad > DataMap.pthis.DataRoad.Count || MapCreater.pthis.GetRoadObj(iRoad) == null)
            return;

        GameObject pObjMon = CreateOneEnemy(iMonster, -1, Vector2.zero);
        pObjMon.transform.position = MapCreater.pthis.GetRoadObj(iRoad).transform.position;
    }
    // ------------------------------------------------------------------
    public void CreateByStandOutRoad(int iMonster, int iRoad_Min, int iRoad_Max)
    {
        // 取得路編號.
        int iRoad = CameraCtrl.pthis.iNextRoad + Random.Range(iRoad_Min, iRoad_Max);

        if (iRoad > DataMap.pthis.DataRoad.Count)
            return;
        
        // 取得路資料.
        MapCoor Road = DataMap.pthis.DataRoad[iRoad];

        for (int iCount = 0; iCount < GameDefine.iPickupSearch; ++iCount)
        {
            MapCoor Result = new MapCoor(Road.X + Random.Range(-5, 6), Road.Y + Random.Range(0, 3));
            bool bCheck = true;

            // 確認是否為道路.
            foreach (MapCoor Itor in DataMap.pthis.DataRoad)
                bCheck &= (Result.X == Itor.X && Result.Y == Itor.Y) == false;

            bCheck &= (MapCreater.pthis.GetMapObj(Result.ToVector2()) != null);

            if (bCheck)
            {
                if (iMonster == 0)
                    PickupCreater.pthis.CreateMeet(Result);
                else
                {
                    GameObject pObjMon = CreateOneEnemy(iMonster, -1, Vector2.zero);
                    pObjMon.transform.position = MapCreater.pthis.GetMapObj(Result.ToVector2()).transform.position;
                }                
                return;
            }            
        }
    }
    // ------------------------------------------------------------------
    public bool CheckPos(GameObject pObj)
    {
        if (Vector2.Distance(ObjCamCtrl.transform.position, pObj.transform.position) > 1.95f)
            return true;
        else
            return false;
    }
    // ------------------------------------------------------------------
    public void SetAI(GameObject ObjMoster, ENUM_ModeMonster enumMode)
    {
        switch (enumMode)
        {
            case ENUM_ModeMonster.ActiveDark:
                ObjMoster.AddComponent<EnemyLightStop>();
                break;
            case ENUM_ModeMonster.Tied:
                ObjMoster.AddComponent<EnemyTied>();
                break;
            case ENUM_ModeMonster.NoMove:
                ObjMoster.AddComponent<EnemyLeoCat>();
                break;
            case ENUM_ModeMonster.Bewitch:
                ObjMoster.AddComponent<EnemyBewitch>();
                break;
            case ENUM_ModeMonster.Shield:
                break;
            case ENUM_ModeMonster.Boss:
                //ObjMoster.AddComponent<EnemyBoss>();
                break;
            default:
                ObjMoster.AddComponent<EnemyNormal>();
                break;
        }
    }
}
