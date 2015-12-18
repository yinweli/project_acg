using UnityEngine;
using System.Collections;

public class G_SmallJelly : MonoBehaviour {

	public void CreateRellyJelly()
    {
        GameObject pObj = EnemyCreater.pthis.CreateOneEnemy(2001, -1, transform.position);
        pObj.transform.position = transform.position;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
