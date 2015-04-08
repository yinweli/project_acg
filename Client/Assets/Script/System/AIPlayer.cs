using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour 
{
    // 角色動畫.
    public Animator pAni = null;
    // 武器動畫.
    public Animator pWAni = null;
    // 手上武器type.
    public WeaponType pWeapon = WeaponType.Weapon_null;

    // 攻擊範圍.
    public float fRange = 1;
    // 射速.
    public float fShotSpeed = 1;

    // 角色.
    public GameObject ObjHuman = null;
    // 目標.
    public GameObject ObjTarget = null;
    // 武器物件.
    public GameObject pSWeapon = null;
    // 是否被抓住.
    public bool bBeCaught = false;
    // 無敵狀態剩餘秒數.
    public float fInvincibleSec = 0;
    // 武器冷卻.
    public float fCoolDown = 0;

    public AudioClip audioClip;

    G_Player pPlayer;

	// Use this for initialization
	void Start ()
    {
        pPlayer = GetComponent<G_Player>(); 
        pPlayer.InitPlayer();
	}
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        if (pWeapon == WeaponType.Weapon_001)
            ChackLight();
        else
            Attack();

        // 移動.
        if (!bBeCaught)
            MoveTo(CameraCtrl.pthis.iNextRoad - pPlayer.iPlayer);
	}
    // ------------------------------------------------------------------
    // 燈光轉向.
    void ChackLight()
    {
        if (ObjTarget.transform.localPosition.x > 0)
            FaceTo(0);
        else
            FaceTo(180);
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
                    FaceTo(0);
                else
                    FaceTo(180);
            }

            NGUITools.PlaySound(audioClip, 0.8f);
            // 發射子彈.
            CreateBullet();
            // 計算冷卻.
			fCoolDown = Time.time + Rule.EquipFireRate(GetComponent<G_Player>().iPlayer);
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
        pObj.GetComponent<AIBullet>().iDamage = Rule.BulletDamage(GetComponent<G_Player>().iPlayer);
    }
    // ------------------------------------------------------------------
    // 被抓函式.
    public void BeCaught(GameObject ObjMonster)
    {
        bBeCaught = true;
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
        Destroy(gameObject.GetComponent<PlayerFollow>());
        if (pAni)
            pAni.Play("Run");
    }

    public void MoveTo(int iRoad)
    {
        float fMyMoveSpeed = SysMain.pthis.GetMoveSpeed();

        if (Vector2.Distance(transform.position, MapCreater.This.GetRoadObj(iRoad).transform.position) > 0.205f)
            fMyMoveSpeed = fMyMoveSpeed * 3; 

        Vector3 vecDirection = MapCreater.This.GetRoadObj(iRoad).transform.position - transform.position;

        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.localPosition += vecDirection.normalized * fMyMoveSpeed * Time.deltaTime;

        if (pPlayer.iPlayer == 0)
        {
            // 檢查距離.
            if (Vector2.Distance(transform.position, MapCreater.This.GetRoadObj(iRoad).transform.position) < 0.005f)
                CameraCtrl.pthis.iLeaderRoad++;
        }
    }

    public void FaceTo(int iRotation)
    {
        ObjHuman.transform.rotation = new Quaternion(0, iRotation, 0, 0);
    }
}
