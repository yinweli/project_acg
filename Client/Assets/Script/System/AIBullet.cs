﻿using UnityEngine;
using System.Collections;

public class AIBullet : MonoBehaviour 
{
    // 傷害.
    public int iDamage = 1;
    // 移動速度
    public float fSpeed = 1.0f;
    // ------------------------------------------------------------------
    void Update()
    {
        // 把物件朝目標(玩家方向)移動.
        transform.Translate(0, fSpeed * Time.deltaTime, 0);

        // 跑出畫面外就刪掉
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
            Destroy(gameObject);
        if (screenPosition.x > Screen.width || screenPosition.x < 0)
            Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<AIEnemy>())
                other.gameObject.GetComponent<AIEnemy>().AddHP(-iDamage);
            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
    public void Chace(GameObject pOgj)
    {
        // 沒有目標就把自己刪掉.
        if (!pOgj)
            return;

        Vector3 diff = pOgj.transform.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
