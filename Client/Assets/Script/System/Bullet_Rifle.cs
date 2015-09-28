using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Rifle : MonoBehaviour
{
    public AIBullet pAI = null;
	public Sprite pSprite = null;

	public bool FirstHit = true;
	public int iCount = 0;
    // ------------------------------------------------------------------
    void Start()
    {
        iCount = Rule.UpgradeWeaponRifle();

		if(iCount > 0)
            pAI.pRander.sprite = pSprite;
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
			Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, FirstHit);

            if (other.gameObject.GetComponent<AIEnemy>())
			{
                other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1, Damage.Item2);
				Statistics.pthis.RecordHit(ENUM_Damage.Rifle, Damage.Item1, FirstHit);
			}//if

            if (iCount <= 0)
                Destroy(gameObject);

			FirstHit = false;
			--iCount;
        }        
    }
    // ------------------------------------------------------------------
}
