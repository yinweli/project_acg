using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Bullet_Revolver : MonoBehaviour 
{
    public AIBullet pAI = null;

	public bool FirstHit = true;
	public int iCountMax = 0;
    public int iCount = 0;
	public List<GameObject> History = new List<GameObject>();
    // ------------------------------------------------------------------
    void Start()
    {
		iCountMax = iCount = Rule.UpgradeWeaponRevolver();
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Enemy")
		{
			Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, FirstHit);
			AIEnemy pEnemy = other.gameObject.GetComponent<AIEnemy>();

			if(pEnemy)
			{
				pEnemy.AddHP(-Rule.UpgradeWeaponRevolver(Damage.Item1, iCountMax - iCount), Damage.Item2);

				if(FirstHit && Damage.Item2)
					pEnemy.HitSfx("G_Crit");

				if(FirstHit == false)
					pEnemy.HitSfx("G_Combo");

				History.Add(other.gameObject);
			}//if

			if(iCount <= 0)
				Destroy(gameObject);

			GameObject NewTarget = GetTarget();

			if(NewTarget == null)
				Destroy(gameObject);

			FirstHit = false;
			--iCount;
			pAI.Chace(NewTarget);
		}//if
    }
    // ------------------------------------------------------------------
    // 取得目標.
    GameObject GetTarget()
    {
        // 沒有可作為目標的怪物.
        if (SysMain.pthis.AtkEnemy.Count == 0)
            return null;

        GameObject ObjTarget = null;

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.AtkEnemy)
        {
			if(History.Contains(itor.Key))
				continue;

            if (!ObjTarget)
                ObjTarget = itor.Key;

            float fDisTarget = Vector2.Distance(transform.position, ObjTarget.transform.position);
            float fDisObj = Vector2.Distance(transform.position, itor.Key.transform.position);

            // 比較距離.
            if (fDisTarget > fDisObj)
                ObjTarget = itor.Key;
        }

        return ObjTarget;
    }
    // ------------------------------------------------------------------

}
