using UnityEngine;
using System.Collections;

public class SysUI : MonoBehaviour 
{
    static public SysUI pthis = null;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    void Start()
    {
        CreatePanel("Prefab/P_Login");
    }
    // ------------------------------------------------------------------
    public GameObject CreatePanel(string Path)
    {
        return NGUITools.AddChild(gameObject, Resources.Load(Path) as GameObject);
    }
    // ------------------------------------------------------------------
    public void ShowDay()
    {
        GameObject pObj = CreatePanel("Prefab/P_Day");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);
        pObj.GetComponent<P_Day>().SetDay();
    }
    // ------------------------------------------------------------------
}
