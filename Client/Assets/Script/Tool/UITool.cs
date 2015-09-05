using UnityEngine;
using System.Collections;

public class UITool : MonoBehaviour {

    public static UITool pthis;
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    public GameObject CreateUI(GameObject Parent, string Path)
    {
		if(Resources.Load(Path) == null)
			Debug.Log(Path + " is NULL");
		return NGUITools.AddChild(Parent, Resources.Load(Path) as GameObject);
    }
    // ------------------------------------------------------------------
    public GameObject CreateUIByPos(GameObject Parent, string Name, float fPosX, float fPosY)
    {
        GameObject pObj = NGUITools.AddChild(Parent, Resources.Load("Prefab/" + Name) as GameObject);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY);

        return pObj;
    }
    // ------------------------------------------------------------------
    public GameObject CreateMap(GameObject Parent, string Name,int iStyle, float fPosX, float fPosY)
    {
        GameObject pObj = NGUITools.AddChild(Parent, Resources.Load(string.Format("Prefab/Scene{0:00}/", iStyle) + Name) as GameObject);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY);
        pObj.transform.localScale = new Vector3(100, 100, 1);

        return pObj;
    }
	// ------------------------------------------------------------------
	public GameObject CreatePickup(GameObject Parent, ENUM_Pickup emType, float fPosX, float fPosY)
	{
		GameObject pObj = null;

		if(emType != ENUM_Pickup.Member)
		{
			pObj = NGUITools.AddChild(Parent, Resources.Load("Prefab/Item/G_" + emType) as GameObject);
			pObj.transform.localPosition = new Vector3(fPosX, fPosY);
		}//if

		return pObj;
	}
    // ------------------------------------------------------------------
    // 建立角色.
    public GameObject CreateRole(GameObject Parent, int iLooks)
    {
		return NGUITools.AddChild(Parent, Resources.Load("Prefab/Chr/" + string.Format("Chr_{0:000}", iLooks)) as GameObject);
    }
    // ------------------------------------------------------------------
}
