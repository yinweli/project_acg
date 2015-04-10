using UnityEngine;
using System.Collections;

public class UITool : MonoBehaviour {

    public static UITool pthis;
    void Awake()
    {
        pthis = this;
    }

    public GameObject CreateUI(GameObject Parent, string Path)
    {
		return NGUITools.AddChild(Parent, Resources.Load(Path) as GameObject);
    }

    public GameObject CreateUIByPos(GameObject Parent, string Name, float fPosX, float fPosY)
    {
        GameObject pObj = NGUITools.AddChild(Parent, Resources.Load("Prefab/" + Name) as GameObject);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY);

        return pObj;
    }

    public GameObject CreateMap(GameObject Parent, string Name,int iStyle, float fPosX, float fPosY)
    {
        GameObject pObj = NGUITools.AddChild(Parent, Resources.Load(string.Format("Prefab/Scene{0:00}/", iStyle) + Name) as GameObject);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY);

        return pObj;
    }
}
