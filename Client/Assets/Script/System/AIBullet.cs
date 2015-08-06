using UnityEngine;
using System.Collections;

public class AIBullet : MonoBehaviour 
{
	// 是否暴擊
	public bool bCriticalStrik = false;
    // 傷害.
    public int iDamage = 1;
    // 移動速度
    public float fSpeed = 1.0f;
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
        {
            Destroy(gameObject);
            return;
        }

        // 把物件朝目標(玩家方向)移動.
        transform.Translate(0, fSpeed * Time.deltaTime, 0);

        // 跑出畫面外就刪掉 
        if (EnemyCreater.pthis.CheckPos(gameObject))
            Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    public void Chace(GameObject pOgj)
    {
        // 沒有目標就把自己刪掉.
        if (!pOgj)
            return;

        Vector3 diff = pOgj.transform.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
