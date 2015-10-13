using UnityEngine;
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
        ToolKit.MoveTo(gameObject, ObjTarget.transform.position - transform.position, fSpeed * 1.8f);

        // 距離夠近就魅惑.
        if (Vector2.Distance(gameObject.transform.position, ObjTarget.transform.position) < 0.015f)
        {
            GameObject pObj = EnemyCreater.pthis.CreateOneEnemy(71, -1, 0, 0);
            pObj.GetComponent<EnemyShield>().InitShield(ObjTarget, pMaster, pAIBewitch);

            ObjTarget.GetComponent<AIPlayer>().BeWitch(pMaster.gameObject, pObj, 1);
            
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
