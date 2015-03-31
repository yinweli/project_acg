using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour 
{
    // 角色動畫.
    public Animator pAni = null;
    // 手上武器type.
    public WeaponType pWeapon = WeaponType.Weapon_null;
    // 攻擊範圍.
    public float fRange = 1;
    // 射速.
    public float fShotSpeed = 1;

    // 目標
    public GameObject ObjTarget = null;
    // 是否被抓住.
    public bool bBeCaught = false;
    // 無敵狀態剩餘秒數.
    public float fInvincibleSec = 0;
    // 武器冷卻.
    public float fCoolDown = 0;

    public AudioClip audioClip;

	// Use this for initialization
	void Start ()
    {
        SysMain.pthis.Role.Add(gameObject, 1);
	}
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        if (pWeapon == WeaponType.Weapon_null)
            return;

        // 確認目標.
        GetTarget();

        // 射擊.
        if (ObjTarget && fCoolDown <= Time.time)
        {
            // 播放射擊.
            if (pAni)
                pAni.Play("Shot");

            NGUITools.PlaySound(audioClip, 0.8f);
            // 發射子彈.
            CreateBullet();
            // 計算冷卻.
            fCoolDown = Time.time + fShotSpeed;
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

        // 如果被抓了，目標就定在抓人的怪物身上.
        if (bBeCaught)
            return;

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
    }
    // ------------------------------------------------------------------
    // 被抓函式.
    public void BeCaught(GameObject ObjMonster)
    {
        bBeCaught = true;
        ObjTarget = ObjMonster;
        gameObject.AddComponent<PlayerFollow>().ObjTarget = ObjMonster;
    }
    // ------------------------------------------------------------------
    // 自由函式.
    public void BeFree()
    {
        bBeCaught = false;
        Destroy(gameObject.GetComponent<PlayerFollow>());
    }
}
