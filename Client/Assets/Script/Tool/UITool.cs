using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UITool : MonoBehaviour
{
    public static UITool pthis;

    public Material M_Sprite;

    GameObject[] SceneObj = new GameObject[(int)ENUM_Map.MapBase];
    Dictionary<string, GameObject> ObjList = new Dictionary<string, GameObject>();

    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    void Start()
    {
        PreLoadUsefulObj();
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
        GameObject pObj = NGUITools.AddChild(Parent, ObjList[Name]);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY);

        return pObj;
    }
    // ------------------------------------------------------------------
    //預先讀取常用的物件.
    public void PreLoadUsefulObj()
    {
        // 讀取子彈.
        for (int i = (int)ENUM_Weapon.Light; i < (int)ENUM_Weapon.Count; i++)
            ObjList.Add(((ENUM_Weapon)i).ToString(), Resources.Load("Prefab/Bullet/" + (ENUM_Weapon)i) as GameObject);

        //讀取物資.
        Object[] pTempObj = Resources.LoadAll("Prefab/Item/");
        for (int i = 0; i < pTempObj.Length; i++)
            ObjList.Add(pTempObj[i].name, pTempObj[i] as GameObject);

        // 讀取怪物.
        pTempObj = Resources.LoadAll("Prefab/Enemy/");
        for (int i = 0; i < pTempObj.Length; i++)
            ObjList.Add(pTempObj[i].name, pTempObj[i] as GameObject);


        // 讀取魅惑愛心.
        ObjList.Add("G_Love", Resources.Load("Prefab/G_Love") as GameObject);
        // 讀取小軟泥.
        ObjList.Add("G_SmallJelly", Resources.Load("Prefab/G_SmallJelly") as GameObject);
        // 讀取基本角色.
        ObjList.Add("G_Player", Resources.Load("Prefab/G_Player") as GameObject);
    }
    // ------------------------------------------------------------------
    // 在關卡開始時預先讀取地圖物件.
    public void PreLoadMapObj(int iStyle)
    {
        for (int i = 1; i <= (int)ENUM_Map.MapBase; i++)
            SceneObj[i - 1] = Resources.Load(string.Format("Prefab/Scene{0:00}/", iStyle) + (ENUM_Map)i) as GameObject;
    }
    // ------------------------------------------------------------------
    public GameObject CreateMap(GameObject Parent, int iType, int iStyle, float fPosX, float fPosY)
    {
        if (SceneObj[iType - 1] == null)
			SceneObj[iType - 1] = Resources.Load(string.Format("Prefab/Scene{0:00}/", iStyle) + (ENUM_Map)iType) as GameObject;
        GameObject pObj = NGUITools.AddChild(Parent, SceneObj[iType - 1]);
        pObj.transform.localPosition = new Vector3(fPosX, fPosY);        

        pObj.transform.localScale = new Vector3(100, 100, 1);

        return pObj;
    }
    // ------------------------------------------------------------------
    // 創建子彈.
    public GameObject CreateBullet(GameObject Parent, ENUM_Weapon pWeapon)
    {
        if (pWeapon == ENUM_Weapon.Null)
            return null;

        return NGUITools.AddChild(Parent, ObjList[pWeapon.ToString()]);
    }
	// ------------------------------------------------------------------
    // 創建拾取物品.
	public GameObject CreatePickup(GameObject Parent, ENUM_Pickup emType, float fPosX, float fPosY)
	{
		GameObject pObj = null;

		if(emType != ENUM_Pickup.Member && ObjList.ContainsKey("G_" + emType))
		{
                pObj = NGUITools.AddChild(Parent, ObjList["G_" + emType]);
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
