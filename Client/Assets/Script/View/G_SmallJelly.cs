using UnityEngine;
using System.Collections;

public class G_SmallJelly : MonoBehaviour {

	public void CreateRellyJelly()
    {
        GameObject pObj = EnemyCreater.pthis.CreateOneEnemy(11, -1, transform.position.x, transform.position.y);
        pObj.transform.position = transform.position;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
