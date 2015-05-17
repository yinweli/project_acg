using UnityEngine;
using System.Collections;

public class Freeze : MonoBehaviour 
{
    public GameObject Obj;
    public float fTime = 2;
    float fTimeCount = 0;
    // ------------------------------------------------------------------
	public void FreezeNow () 
    {
        Obj = UITool.pthis.CreateUI(gameObject, "Prefab/G_Freeze");
        fTimeCount = Time.time + 1;
	}
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        if (fTime > 0 && fTimeCount <= Time.time)
        {
            fTime--;
            fTimeCount = Time.time + 1;
        }
        else if (fTime <= 0)
        {
            Destroy(Obj);
            Destroy(this);
        }
	}
}
