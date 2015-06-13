using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIEnemy : MonoBehaviour 
{
	// 怪物編號
	public int iMonster = 1;
	// HP
	public int iHP = 0;
    // 怪物資料.
    public DBFMonster DBFData;

    // 是否已抓了人.
    public bool bHasTarget = false;

    public Animator pAni = null;

    public AudioClip ClipHurt;
    public AudioClip ClipDead;

    // 記住起點.
    public Vector3 PosStart = new Vector3();

    public Vector3 vecRunDir = Vector3.zero;
    
	// Use this for initialization
	void Start () 
    {
		DBFData = GameDBF.This.GetMonster(iMonster) as DBFMonster;

        SysMain.pthis.Enemy.Add(gameObject, DBFData.Threat);

		if(DBFData == null)
		{
			Debug.Log("DBFMonster(" + iMonster + ") null");
			return;
		}//if

        iHP = DBFData.HP;
        PosStart = transform.position;

        if (ENUM_ModeMonster.ActiveDark == (ENUM_ModeMonster)DBFData.Mode)
            gameObject.AddComponent<EnemyLightStop>();
        else if (ENUM_ModeMonster.Tied == (ENUM_ModeMonster)DBFData.Mode)
            gameObject.AddComponent<EnemyTied>();
        else if (ENUM_ModeMonster.Boss == (ENUM_ModeMonster)DBFData.Mode)
            gameObject.AddComponent<EnemyBoss>();
        else
            gameObject.AddComponent<EnemyNormal>();
	}
    // ------------------------------------------------------------------
    void OnDestroy()
    {
        if (SysMain.pthis.Enemy.ContainsKey(gameObject))
            SysMain.pthis.Enemy.Remove(gameObject);
        if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
            SysMain.pthis.AtkEnemy.Remove(gameObject);
    }
    // ------------------------------------------------------------------
    void OnTriggerStay2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (!SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Add(gameObject, GetTheat());
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Remove(gameObject);
        }
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        if (iHP > 0)
            AddHP(-GameDefine.iDamageClick, false);
    }
    // ------------------------------------------------------------------
    public void AddHP(int iValue, bool IsCrit)
    {
        if (iHP <= 0 && iValue < 0)
            return;

        iHP += iValue;

		// 播放受擊特效.
		if(iValue < 0)
			UITool.pthis.CreateUI(gameObject,"Prefab/S_Hit");

        if (IsCrit)
            UITool.pthis.CreateUI(gameObject, "Prefab/G_Crit");

        // 沒血逃跑.
        if (iHP <= 0)
        {
            NGUITools.PlaySound(ClipDead, 0.8f);
            GameData.pthis.iKill++;
            // 從可攻擊陣列移除.
            if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Remove(gameObject);       
        }
        else
            NGUITools.PlaySound(ClipHurt, 0.8f);
    }
    // ------------------------------------------------------------------
    public int GetTheat()
    {
        if (bHasTarget)
            return DBFData.Threat + 5;
        else
            return DBFData.Threat;
    }
    // ------------------------------------------------------------------
    public float GetSpeed()
    {
        if (GetComponent<Freeze>())
            return DBFData.MoveSpeed * 0.5f;
        else
            return DBFData.MoveSpeed;
    }
    // ------------------------------------------------------------------
    public void FaceTo(Vector3 vecDir)
    {
        if (vecDir.x < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);        
    }
    // ------------------------------------------------------------------
    public void AniPlay(string Name)
    {
        if (pAni)
            pAni.Play(Name);
    }
}
