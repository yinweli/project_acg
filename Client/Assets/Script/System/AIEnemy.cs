using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour 
{
    // HP
    public int iHP = 5;
    // 移動速度
    public float fSpeed = 0.05f;
    // 威脅程度(越高成為玩家優先選擇)
    public int iThreat = 1;
    // 目標
    public GameObject ObjTarget = null;

    // 是否已抓了人.
    public bool bHasTarget = false;

	// Use this for initialization
	void Start () 
    {
        //測試先指定目標.
        ObjTarget = SysMain.pthis.Role[Random.Range(0, SysMain.pthis.Role.Count)];
	}

    void Update()
    {
        // 沒血消失.
        if (iHP <= 0)
            Destroy(gameObject);

        // 確認目標.
        FindTarget();

        // 已有抓人逃跑模式.
        //if (bHasTarget)

        // 如果有目標且沒抓人時，追蹤目標
        //if (!ObjTarget && !bHasTarget)
            Chace();
    }

    void FindTarget()
    {
        // 如果已經抓了人就不用再找目標了.
        if (bHasTarget)
            return;

        // 清空目標.
        //ObjTarget = null;        
    }

    void Chace()
    {
        // 沒有目標就放棄追蹤.
        if (!ObjTarget)
            return;

        // 取向量.
        Vector3 vecDirection = ObjTarget.transform.position - transform.position;
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
}
