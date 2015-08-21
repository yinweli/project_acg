using UnityEngine;
using System.Collections;

public class SysUI : MonoBehaviour 
{
    static public SysUI pthis = null;
    public P_Victory pVictory = null;
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
    public GameObject CreateUI(GameObject Parent, string Path)
    {
        return NGUITools.AddChild(Parent, Resources.Load(Path) as GameObject);
    }
    // ------------------------------------------------------------------
    public void ShowDay()
    {
        GameObject pObj = CreatePanel("Prefab/P_Day");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);
    }
    // ------------------------------------------------------------------
}
