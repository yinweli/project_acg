using UnityEngine;
using System.Collections;

public class EnemyShield : MonoBehaviour
{
    public AIEnemy pAI = null;

    public AIEnemy pMaster = null;
    public EnemyBewitch pAIBewitch = null;
    public GameObject ObjTarget = null;
    // ------------------------------------------------------------------
    void Start()
    {
        pAI = GetComponent<AIEnemy>();
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!ObjTarget || !pMaster)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.position = ObjTarget.transform.position;

        if (pAI.iHP <= 0)
        {
            pMaster.iHP -= pAI.DBFData.HP;
            pAIBewitch.ObjTarget = null;
            pMaster.bHasTarget = false;

            ObjTarget.GetComponent<AIPlayer>().BeFree();

            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
    public void InitShield(GameObject Obj, AIEnemy pAI, EnemyBewitch pBewitch)
    {
        ObjTarget = Obj;
        pMaster = pAI;
        pAIBewitch = pBewitch;
    }
}
