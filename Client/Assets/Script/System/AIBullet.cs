using UnityEngine;
using System.Collections;

public class AIBullet : MonoBehaviour 
{
    // 傷害.
    public int iDamage = 1;
    // 移動速度
    public float fSpeed = 1.0f;
    // 目標
    public GameObject ObjTarget = null;

    void Update()
    {
        Chace();

        if (ObjTarget && Vector2.Distance(transform.position, ObjTarget.transform.position) < 0.01f)
        {
            ObjTarget.GetComponent<AIEnemy>().AddHP(-iDamage);
            Destroy(gameObject);
        }
    }

    void Chace()
    {
        // 沒有目標就把自己刪掉.
        if (!ObjTarget)
        {
            Destroy(gameObject);
            return;
        }
       
        Vector3 diff = ObjTarget.transform.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
       
        // 把物件朝目標(玩家方向)移動.
        transform.Translate(0, fSpeed * Time.deltaTime, 0);
    }
	
}
