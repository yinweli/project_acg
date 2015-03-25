using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIEnemy : MonoBehaviour 
{
    // HP
    public int iHP = 5;
    // 移動速度
    public float fMoveSpeed = 0.05f;
    // 威脅程度(越高成為玩家優先選擇)
    public int iThreat = 1;
    // 目標
    public GameObject ObjTarget = null;

    // 是否已抓了人.
    public bool bHasTarget = false;

    public AudioClip ClipHurt;
    public AudioClip ClipDead;

    // 記住起點.
    Vector3 PosStart = new Vector3();
    
	// Use this for initialization
	void Start () 
    {
        PosStart = transform.position;
        //測試先指定目標.
        KeyValuePair<GameObject, int> pTemp = LibCSNStandard.Tool.RandomDictionary(SysMain.pthis.Role, new System.Random());
        ObjTarget = pTemp.Key;
	}
    // ------------------------------------------------------------------
    void OnDestroy()
    {
        if (SysMain.pthis.Enemy.ContainsKey(gameObject))
            SysMain.pthis.Enemy.Remove(gameObject);
    }
    // ------------------------------------------------------------------
    void Update()
    {
        // 沒血逃跑.
        if (iHP <= 0)
        {
            // 取向量.
            MoveTo(PosStart - transform.position, fMoveSpeed * 4);
            // 跑出畫面外就刪掉
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.y > Screen.height || screenPosition.y < 0)
                Destroy(gameObject);
            if (screenPosition.x > Screen.width - 10 || screenPosition.x < 10)
                Destroy(gameObject);
            return;
        }       

        // 確認目標.
        FindTarget();

        // 已有抓人逃跑模式.
        //if (bHasTarget)

        // 如果有目標且沒抓人時，追蹤目標
        //if (!ObjTarget && !bHasTarget)
            Chace();
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (!SysMain.pthis.Enemy.ContainsKey(gameObject))
                SysMain.pthis.Enemy.Add(gameObject, iThreat);
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (SysMain.pthis.Enemy.ContainsKey(gameObject))
                SysMain.pthis.Enemy.Remove(gameObject);
        }
    }
    // ------------------------------------------------------------------
    public void AddHP(int iValue)
    {
        iHP += iValue;

        // 沒血逃跑.
        if (iHP <= 0)
        {
            NGUITools.PlaySound(ClipDead, 0.8f);
            // 從可攻擊陣列移除.
            if (SysMain.pthis.Enemy.ContainsKey(gameObject))
                SysMain.pthis.Enemy.Remove(gameObject);       
        }
        else
            NGUITools.PlaySound(ClipHurt, 0.8f);
    }
    // ------------------------------------------------------------------
    void FindTarget()
    {
        // 如果已經抓了人就不用再找目標了.
        if (bHasTarget)
            return;

        // 清空目標.
        //ObjTarget = null;        
    }
    // ------------------------------------------------------------------
    void Chace()
    {
        // 沒有目標就放棄追蹤.
        if (!ObjTarget)
            return;

        // 取向量.
        MoveTo(ObjTarget.transform.position - transform.position, fMoveSpeed);
    }
    // ------------------------------------------------------------------
    void MoveTo(Vector3 vecDirection, float fSpeed)
    {        
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
}
