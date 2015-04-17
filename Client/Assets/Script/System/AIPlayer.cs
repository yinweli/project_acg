using UnityEngine;
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
	public WeaponType pWeapon = WeaponType.Weapon_null;
	
	// 角色.
	public GameObject ObjHuman = null;
	// 目標.
	public GameObject ObjTarget = null;
	// 武器物件.
	public GameObject pSWeapon = null;
	// 是否被抓住.
	public bool bBeCaught = false;
	// 武器冷卻.
	public float fCoolDown = 0;
	
	public AudioClip audioClip;
    // ------------------------------------------------------------------
	void Start ()
	{
        pWeapon = (WeaponType)PlayerData.pthis.Members[iPlayer].iEquip;
        // 建立外觀.
        ObjHuman = UITool.pthis.CreateRole(gameObject, PlayerData.pthis.Members[iPlayer].iSex, PlayerData.pthis.Members[iPlayer].iLook);
        // 設定武器.
        if (ObjHuman && ObjHuman.GetComponent<G_PLook>())
            ObjHuman.GetComponent<G_PLook>().SetLook(this, iPlayer, pWeapon);
        // 設定武器音效
        if (pWeapon != WeaponType.Weapon_null && pWeapon != WeaponType.Weapon_001)
            audioClip = Resources.Load("Sound/FX/" + pWeapon) as AudioClip;
	}
	// ------------------------------------------------------------------
	void Update () 
	{
		if (!SysMain.pthis.bIsGaming)
			return;
		
		if (pWeapon == WeaponType.Weapon_001)
			ChackLight();
		else
			Attack();
		
		// 移動.
		if (!bBeCaught)
			MoveTo(CameraCtrl.pthis.iNextRoad - iPlayer);
	}
	// ------------------------------------------------------------------
	// 燈光轉向.
	void ChackLight()
	{
		if (ObjTarget.transform.localPosition.x > 0)
			FaceTo(1);
		else
			FaceTo(-1);
	}
	// ------------------------------------------------------------------
	// 射擊函式.
	void Attack()
	{
		if (pWeapon == WeaponType.Weapon_null || pWeapon == WeaponType.Weapon_001)
			return;
		
		// 確認目標.
		GetTarget();
		
		// 射擊.
		if (ObjTarget && fCoolDown <= Time.time && P_UI.pthis.UseBullet(pWeapon))
		{
			// 播放射擊.
			if (!bBeCaught && pAni)
				pAni.Play("Shot");
			if (pWAni)
				pWAni.Play("Fire");
			
			// 轉面向.
			if (!bBeCaught)
			{
				if (transform.position.x < ObjTarget.transform.position.x)
					FaceTo(1);
				else
					FaceTo(-1);
			}
			
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
		if (SysMain.pthis.Enemy.Count == 0)
		{            
			ObjTarget = null;
			// 播放角色走路.
		}
		
		foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Enemy)
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
			if (pTargetAI.iThreat < pObjAi.iThreat)
			{
				ObjTarget = itor.Key;
				return;
			}
			
			float fDisTarget = Vector2.Distance(transform.position, ObjTarget.transform.position);
			float fDisObj = Vector2.Distance(transform.position, itor.Key.transform.position);
			
			// 比較距離.
			if(fDisTarget > fDisObj)
			{
				ObjTarget = itor.Key;
				return;
			}
		}        
	}
	// ------------------------------------------------------------------
	// 建立子彈函式.
	public void CreateBullet()
	{
		GameObject pObj = NGUITools.AddChild(gameObject, Resources.Load("Prefab/S_Bullet") as GameObject);
		pObj.transform.parent = transform.parent;
		pObj.transform.localPosition = new Vector3(transform.localPosition.x + 5.0f, transform.localPosition.y);
		pObj.GetComponent<AIBullet>().Chace(ObjTarget);
		pObj.GetComponent<AIBullet>().iDamage = Rule.BulletDamage(iPlayer);
	}
	// ------------------------------------------------------------------
	// 被抓函式.
	public void BeCaught(GameObject ObjMonster)
	{
		bBeCaught = true;
        // 從可抓佇列中移除.
        SysMain.pthis.CatchRole.Remove(gameObject);
		// 拿手電筒的不需要改目標.
		if(pWeapon != WeaponType.Weapon_001)
			ObjTarget = ObjMonster;
		gameObject.AddComponent<PlayerFollow>().ObjTarget = ObjMonster;
		if (pAni)
			pAni.Play("Break");
	}
	// ------------------------------------------------------------------
	// 自由函式.
	public void BeFree()
	{
		bBeCaught = false;
        // 重新加入可抓佇列中.
        SysMain.pthis.CatchRole.Add(gameObject, iPlayer);
		Destroy(gameObject.GetComponent<PlayerFollow>());
		if (pAni)
			pAni.Play("Run");
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
	public void FaceTo(int iFace)
	{
        //ObjHuman.transform.rotation = new Quaternion(0, iRotation, 0, 0);
        ObjHuman.transform.localScale = new Vector3(iFace, 1, 1);
	}
}