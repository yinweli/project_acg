using UnityEngine;
using System.Collections;

public class G_RandRole : MonoBehaviour 
{
    public GameObject[] pObjRole = new GameObject[3];
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 3; i++)
            pObjRole[i].SetActive(false);
	}
    // ------------------------------------------------------------------
    public void StartRand()
    {
        for (int i = 0; i < 3; i++)
        {
            pObjRole[i].SetActive(true);
            CreateRole(pObjRole[i]);
        }
    }

    public void CreateRole(GameObject ObjParent)
    {
        // 建立外觀.
        GameObject ObjHuman = UITool.pthis.CreateRole(ObjParent, Random.Range(0, 20));
        ObjHuman.AddComponent<G_PLook>().ChangeTo2DSprite(ENUM_Weapon.Null);
    }
}
