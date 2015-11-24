using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class AIEnemy : MonoBehaviour 
{
	// 怪物編號
	public int iMonster = 1;
	// HP
	public int iHP = 0;
	// 下次灼燒時間
	public float fBurnTime = 0.0f;
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

	GameObject Burn = null;
    // ------------------------------------------------------------------
	void Start () 
    {
		DBFData = GameDBF.pthis.GetMonster(iMonster) as DBFMonster;

        SysMain.pthis.Enemy.Add(gameObject, DBFData.Threat);

		if(DBFData == null)
		{
			Debug.Log("DBFMonster(" + iMonster + ") null");
			return;
		}//if

        if (iHP <= 0)
            iHP = DBFData.HP;

        if ((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.Boss)
            iHP += Rule.BossHP(iHP);

        PosStart = transform.position;

        EnemyCreater.pthis.SetAI(gameObject, (ENUM_ModeMonster)DBFData.Mode);
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
		if(iHP <= 0)
			return;

		if(other.gameObject.tag == "FreshLight" || other.gameObject.tag == "Light")
		{
			if(SysMain.pthis.AtkEnemy.ContainsKey(gameObject) == false)
				SysMain.pthis.AtkEnemy.Add(gameObject, GetTheat());
		}//if

		if(other.gameObject.tag == "FreshLight")
        {
			if(Rule.GetWeaponLevel(ENUM_Weapon.Light) > 0 && Time.realtimeSinceStartup >= fBurnTime)
			{
				Tuple<int, float> Result = Rule.UpgradeWeaponLight();

				fBurnTime = Result.Item2;
				AddHP(-Result.Item1, false);
				Statistics.pthis.RecordShot(ENUM_Damage.Light);
				Statistics.pthis.RecordHit(ENUM_Damage.Light, Result.Item1, true);

				// 播放燃燒動畫
				if(Burn == null)
					Burn = UITool.pthis.CreateUI(gameObject, "Prefab/Sfx/G_Burn");
			}//if
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
		if(iHP <= 0)
			return;

		if(other.gameObject.tag == "FreshLight" || other.gameObject.tag == "Light")
        {
            if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Remove(gameObject);
        }

		if(other.gameObject.tag == "FreshLight")
		{
			if(Burn != null)
			{
				Destroy(Burn);
				Burn = null;
			}//if
		}
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

        if(IsCrit)
            UITool.pthis.CreateUI(gameObject, "Prefab/Sfx/G_Crit");

        if(iValue < 0 && GetComponent<EnemyJelly>())
            GetComponent<EnemyJelly>().CreateJelly(iValue);

        AniPlay("Hit");

        // 沒血逃跑.
        if (iHP <= 0)
        {
            NGUITools.PlaySound(ClipDead, 0.8f);
            DataGame.pthis.iKill++;

            // 從可攻擊陣列移除.
            if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Remove(gameObject);       
        }
        else
            NGUITools.PlaySound(ClipHurt, 0.8f);
    }
    // ------------------------------------------------------------------
    public void HitSfx(string sSfxName)
    {
        UITool.pthis.CreateUI(gameObject, "Prefab/Sfx/" + sSfxName);
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
            return DBFData.MoveSpeed * Rule.UpgradeWeaponPistol();
        else
            return DBFData.MoveSpeed;
    }
    // ------------------------------------------------------------------
    public void FaceAndMove(Vector3 vecDir, bool bMove, float fValue)
    {
        if (vecDir.x < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
 
        if(bMove)
            ToolKit.MoveTo(gameObject, vecDir, GetSpeed() * fValue);
    }
    // ------------------------------------------------------------------
    public void AniPlay(string Name)
    {
        if (pAni && pAni.HasState(0, Animator.StringToHash(Name)))
            pAni.Play(Name);
    }
}
