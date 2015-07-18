using UnityEngine;
using System.Collections;

public class G_ListRole : MonoBehaviour 
{
    public int iPlayerID = 0;
    public UILabel pLbLv = null;
    public G_Info pInfo = null;

    void Start()
    {
        pLbLv.gameObject.SetActive(false);
        if (GetComponent<Animation>())
            GetComponent<Animation>().Stop();
    }

    void OnPress(bool bIsDown)
    {
        if (bIsDown && pInfo)
        {
            pInfo.gameObject.SetActive(true);
            pInfo.SetInfo(iPlayerID);            
        }
        else if (!bIsDown && pInfo)
        {
            pInfo.Reset();
            pInfo.gameObject.SetActive(false);
        }
    }

    public void ShowLevelUp(int iLv)
    {
        pLbLv.gameObject.SetActive(true);
        if (GetComponent<Animation>())
            GetComponent<Animation>().Play();
        ChangeLevel(iLv);
    }

    public void ChangeLevel(int iLv)
    {
        pLbLv.text = "Lv " + iLv;
    }

    public void ShowFeature(int iFeature)
    {
        GameObject pObj = SysUI.pthis.CreateUI(gameObject, "Prefab/S_Feature");
        pObj.GetComponent<Lb_Feature>().SetFeature(iFeature);
        pObj.transform.localPosition = new Vector3(0, 30, 0);
    }

    public void ShowEquip(GameObject pHand, int iEquip)
    {
        GameObject pObj = SysUI.pthis.CreateUI(gameObject, "Prefab/S_Weapon");
        pObj.transform.localPosition = new Vector3(0, -73, 0);

        GameObject ObjWeapon = UITool.pthis.CreateUI(pHand, "Prefab/" + (ENUM_Weapon)iEquip);
        SpriteRenderer[] p2DS = ObjWeapon.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer pRender in p2DS)
            ToolKit.ChangeTo2DSprite(pRender);
    }
}
