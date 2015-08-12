using UnityEngine;
using System.Collections;

public class EnemyJelly : MonoBehaviour
{
    public int iShotHurt = 0;
	// 產生小軟泥.
    public void CreateJelly(int iHurt)
    {
        iShotHurt += iHurt;

        if (iShotHurt > -GameDefine.iJellyGrow)
            return;


        GameObject ObjJelly = UITool.pthis.CreateUIByPos(gameObject, "G_SmallJelly", gameObject.transform.position.x, gameObject.transform.position.y);
        ObjJelly.transform.parent = gameObject.transform.parent;

        float PosX = gameObject.transform.position.x + Random.Range(-0.2f, 0.2f);
        float PosY = gameObject.transform.position.y + Random.Range(-0.015f, 0.45f);
        ObjJelly.transform.position = new Vector3(PosX, PosY, 0);

        if (PosX < 0)
            ObjJelly.transform.localScale = new Vector3(-1, 1, 1);

        iShotHurt += GameDefine.iJellyGrow;
    }
}
