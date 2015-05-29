﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour 
{
    // 角色編號.
    public int iPlayer;
	// 角色動畫.
	public Animator pAni = null;
	// 武器動畫.
	public Animator pWAni = null;
	// 手上武器type.
	public ENUM_Weapon pWeapon = ENUM_Weapon.Weapon_null;
	
	// 角色.
	public GameObject ObjHuman = null;
	// 目標.
	public GameObject ObjTarget = null;
	// 武器物件.
	public GameObject pSWeapon = null;
    // 盾盾.
    public GameObject ObjShield = null;
	// 是否被抓住.
	public bool bBeCaught = false;
	// 武器冷卻.
	public float fCoolDown = 0;

	public AudioClip audioClip;

    public Vector3 VecDeadPos = Vector3.zero;

    GameObject ObjCatch = null;    
    // ------------------------------------------------------------------
    public void Init(bool bIsIn,int iItemID, Member pMember)
	{
        // 未加入角色要被加上蜘蛛網.
        if (!bIsIn)
        {
            ObjCatch = UITool.pthis.CreateUI(gameObject, "Prefab/Item/G_Catch");
            ObjCatch.transform.localPosition = new Vector3(0, 0, -0.01f);
            ObjCatch.GetComponent<Btn_SaveRole>().iItemID = iItemID;
        }

        Debug.Log("PlayerID" + iPlayer);
        // 有盾套盾.
        if (PlayerData.pthis.Members[iPlayer].iShield > 0)
            ObjShield = UITool.pthis.CreateUI(gameObject, "Prefab/G_Shield");

        pWeapon = (ENUM_Weapon)pMember.iEquip;
        // 建立外觀.
        ObjHuman = UITool.pthis.CreateRole(gameObject, pMember.iSex, pMember.iLook);
        // 設定武器.
        if (ObjHuman && ObjHuman.GetComponent<G_PLook>())
            ObjHuman.GetComponent<G_PLook>().SetLook(this, iPlayer, pWeapon);
        // 設定武器音效
        if (pWeapon != ENUM_Weapon.Weapon_null && pWeapon != ENUM_Weapon.Weapon_001)
            audioClip = Resources.Load("Sound/FX/" + pWeapon) as AudioClip;
	}
	// ------------------------------------------------------------------
	void Update () 
	{
        if (!SysMain.pthis.bIsGaming || ObjCatch)
            return;

        if (pWeapon == ENUM_Weapon.Weapon_001)
            FaceTo(1, ObjTarget);
        else
            Attack();

        // 移動.
        if (!bBeCaught)
            MoveTo(CameraCtrl.pthis.iNextRoad - iPlayer);
	}
	// ------------------------------------------------------------------
	// 射擊函式.
	void Attack()
	{
		if (pWeapon == ENUM_Weapon.Weapon_null || pWeapon == ENUM_Weapon.Weapon_001)
			return;
		
		// 確認目標.
        if (!bBeCaught)
    		GetTarget();
		
		// 射擊.
		if (ObjTarget && fCoolDown <= Time.time && P_UI.pthis.UseBullet(pWeapon))
		{
			// 播放射擊.
            if (!bBeCaught && pAni)
            {
                pAni.Play("Shot");
                // 轉面向.
                FaceTo(1, ObjTarget);
            }
			if (pWAni)
				pWAni.Play("Fire");					
			
			NGUITools.PlaySound(audioClip, 0.8f);
			// 發射子彈.
			CreateBullet();
			// 計算冷卻.
			fCoolDown = Time.time + Rule.EquipFireRate(iPlayer);
		}		
	}
	// ------------------------------------------------------------------
	// 取得目標.
	void GetTarget()
	{
		// 沒有可作為目標的怪物.
		if (SysMain.pthis.AtkEnemy.Count == 0)
			ObjTarget = null;
		
		foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.AtkEnemy)
		{
			// 確認現在是否有目標.
			if(ObjTarget == null)
			{
				ObjTarget = itor.Key;
				return;
			}
		
			AIEnemy pTargetAI = ObjTarget.GetComponent<AIEnemy>();
			AIEnemy pObjAi = itor.Key.GetComponent<AIEnemy>();

			// 比較威脅值.
            if (pTargetAI.GetTheat() < pObjAi.GetTheat())
			{
				ObjTarget = itor.Key;
				return;
			}
			
			float fDisTarget = Vector2.Distance(transform.position, ObjTarget.transform.position);
			float fDisObj = Vector2.Distance(transform.position, itor.Key.transform.position);
			
			// 比較距離.
			if(fDisTarget > fDisObj)
				ObjTarget = itor.Key;
		}        
	}
	// ------------------------------------------------------------------
	// 建立子彈函式.
	public void CreateBullet()
	{
		GameObject pObj = NGUITools.AddChild(gameObject, Resources.Load("Prefab/S_Bullet") as GameObject);
		Tuple<int, bool> Damage = Rule.BulletDamage(iPlayer);

		pObj.transform.parent = transform.parent;
		pObj.transform.localPosition = new Vector3(transform.localPosition.x + 5.0f, transform.localPosition.y);
		pObj.GetComponent<AIBullet>().Chace(ObjTarget);
		pObj.GetComponent<AIBullet>().iDamage = Damage.Item1;
		pObj.GetComponent<AIBullet>().bCriticalStrik = Damage.Item2;

        if (Rule.IsFeature(ENUM_ModeFeature.Frozen, iPlayer))
            pObj.GetComponent<AIBullet>().pType = ENUM_ModeFeature.Frozen;
	}
	// ------------------------------------------------------------------
	// 被抓函式.
	public void BeCaught(GameObject ObjMonster)
	{
		bBeCaught = true;
        // 從可抓佇列中移除.
        ToolKit.CatchRole.Remove(gameObject);
		// 拿手電筒的不需要改目標.
		if(pWeapon != ENUM_Weapon.Weapon_001)
			ObjTarget = ObjMonster;
		gameObject.AddComponent<PlayerFollow>().ObjTarget = ObjMonster;
		if (pAni)
			pAni.Play("Break");

        if (ObjShield && ObjShield.GetComponent<Shield>() && ObjShield.GetComponent<Shield>().iCount > 0)
        {
            ObjMonster.GetComponent<AIEnemy>().AddHP(-GameDefine.iDamageShield, false);
            ObjShield.GetComponent<Shield>().CostShield();
        }
	}
	// ------------------------------------------------------------------
	// 自由函式.
	public void BeFree()
	{
		bBeCaught = false;
        // 重新加入可抓佇列中.
        ToolKit.CatchRole.Add(gameObject, Rule.MemberThreat(iPlayer));
		Destroy(gameObject.GetComponent<PlayerFollow>());

        StartCoroutine(ResetDeadPos());
        if (pAni)
            pAni.Play("Run");
	}
    // ------------------------------------------------------------------
    // 被殺函式.
    public void BeKill()
    {
        SysMain.pthis.DeadRole.Add(iPlayer);
        SysMain.pthis.Role.Remove(gameObject);
        GameData.pthis.iDead++;

        Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    // 移動函式.
	public void MoveTo(int iRoad)
	{
		if (!MapCreater.pthis.GetRoadObj(iRoad))
			return;
		
		float fMyMoveSpeed = SysMain.pthis.GetMoveSpeed();
		
		if (Vector2.Distance(transform.position, MapCreater.pthis.GetRoadObj(iRoad).transform.position) > 0.206f)
            fMyMoveSpeed = GameDefine.fBaseSpeed * 3; 
		
		Vector3 vecDirection = MapCreater.pthis.GetRoadObj(iRoad).transform.position - transform.position;
		
		// 把z歸零, 因為沒有要動z值.
		vecDirection.z = 0;
		// 把物件位置朝目標向量(玩家方向)移動.
		transform.localPosition += vecDirection.normalized * fMyMoveSpeed * Time.deltaTime;
		
		if (iPlayer == 0)
		{
			// 檢查距離.
			if (Vector2.Distance(transform.position, MapCreater.pthis.GetRoadObj(iRoad).transform.position) < 0.005f)
				CameraCtrl.pthis.iLeaderRoad++;
		}
	}
    // ------------------------------------------------------------------
    public Vector3 GetDeadPos()
    {
        // 如果已有預定死亡位置.
        if (VecDeadPos == Vector3.zero)
            switch (Random.Range(1, 4))
            {
                case 1: //上方.
                    VecDeadPos.x = transform.localPosition.x + Random.Range(-500.0f, 500.0f);
                    VecDeadPos.y = transform.localPosition.y + Random.Range(380.0f, 450.0f);
                    break;
                case 2: //左方.
                    VecDeadPos.x = transform.localPosition.x + Random.Range(-470.0f, -520.0f);
                    VecDeadPos.y = transform.localPosition.y + +Random.Range(-300.0f, 400.0f);
                    break;
                case 3: //右方.
                    VecDeadPos.x = transform.localPosition.x + Random.Range(470.0f, 520.0f);
                    VecDeadPos.y = transform.localPosition.y + +Random.Range(-300.0f, 400.0f);
                    break;
            }
        
        return VecDeadPos;
    }
    // ------------------------------------------------------------------
	public void FaceTo(int iFaceTo, GameObject FaceObj)
	{
        if (transform.position.x < ObjTarget.transform.position.x)
            ObjHuman.transform.localScale = new Vector3(1 * iFaceTo, 1, 1);
        else
            ObjHuman.transform.localScale = new Vector3(-1 * iFaceTo, 1, 1);
	}
    // ------------------------------------------------------------------
    IEnumerator ResetDeadPos()
    {
        bool bCanReset = true;
        float fReset = Time.time + 3;

        while (bCanReset)
        {
            yield return new WaitForEndOfFrame();
            // 等待期間被抓.
            if (bBeCaught)
                bCanReset = false;

            // 檢查是否可重置.
            if (fReset <= Time.time)
            {
                bCanReset = false;
                VecDeadPos = Vector3.zero;
            }
        }        
    }        
}