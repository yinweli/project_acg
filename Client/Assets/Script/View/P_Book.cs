using UnityEngine;
using System.Collections;

public class P_Book : MonoBehaviour 
{
    public GameObject ObjList;
    GameObject ObjNowPage;
    // ------------------------------------------------------------------
    void Start()
    {

    }
    // ------------------------------------------------------------------    
    public void OpenUpdate()
    {
        if (ObjNowPage)
            Destroy(ObjNowPage);
    }
    // ------------------------------------------------------------------
    public void OpenAchieve()
    {
        if (ObjNowPage)
            Destroy(ObjNowPage);
    }
    // ------------------------------------------------------------------
    public void OpenRecord()
    {
        if (ObjNowPage)
            Destroy(ObjNowPage);
        ObjNowPage = UITool.pthis.CreateUI(gameObject, "Prefab/G_Record");
    }
}
