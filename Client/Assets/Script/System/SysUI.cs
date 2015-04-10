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
    public GameObject CreatePanel(string Path)
    {
        return NGUITools.AddChild(gameObject, Resources.Load(Path) as GameObject);
    }
    // ------------------------------------------------------------------
    public void ShowDay()
    {
        GameObject pObj = CreatePanel("Prefab/P_Day");
        pObj.GetComponent<P_Day>().SetDay();
    }
    // ------------------------------------------------------------------
}
