using UnityEngine;
using System.Collections;

public class G_SmallJelly : MonoBehaviour {

    public SpriteRenderer S_Face;
	public void CreateRellyJelly()
    {
        GameObject pObj = EnemyCreater.pthis.CreateOneEnemy(2001, -1, transform.position);
        pObj.transform.position = transform.position;
        pObj.GetComponent<ChangeColor>().Change_Color = S_Face.color;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
