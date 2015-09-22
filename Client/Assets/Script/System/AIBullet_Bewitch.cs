﻿using UnityEngine;
using System.Collections;

public class AIBullet_Bewitch : MonoBehaviour
{
    public AIEnemy pMaster = null;
    public EnemyBewitch pAIBewitch = null;
    public GameObject ObjTarget = null;
    public float fSpeed = 0;
    // ------------------------------------------------------------------
	void Update () 
    {
        // 主人不見了!
        if (!pMaster || !pAIBewitch)
            Destroy(gameObject);

        // 追追追.
        ToolKit.MoveTo(gameObject, ObjTarget.transform.position - transform.position, fSpeed);

        // 距離夠近就魅惑.
        if (Vector2.Distance(gameObject.transform.position, ObjTarget.transform.position) < 1.5f)
        {
            ObjTarget.GetComponent<AIPlayer>().BeWitch(pMaster.gameObject, 1);

            GameObject pObj = EnemyCreater.pthis.CreateOneEnemy(71, -1, 0, 0);
            pObj.GetComponent<EnemyShield>().InitShield(ObjTarget, pMaster, pAIBewitch);

            pAIBewitch.ObjBullet = null;
            Destroy(gameObject);
        }
	}
    // ------------------------------------------------------------------
    public void InitBullet(GameObject Obj, AIEnemy pAI, EnemyBewitch pBewitch)
    {
        ObjTarget = Obj;
        pMaster = pAI;
        pAIBewitch = pBewitch;

        fSpeed = pMaster.GetSpeed();
    }
    // ------------------------------------------------------------------
}
