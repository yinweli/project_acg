using UnityEngine;
using System.Collections;

public class G_ListRole : MonoBehaviour 
{
    public int iPlayerID = 0;
    public G_Info pInfo = null;

    void OnPress(bool bIsDown)
    {
        if (bIsDown && pInfo)
        {
            pInfo.gameObject.SetActive(true);
            pInfo.SetInfo(iPlayerID);            
        }
        else if(!bIsDown && pInfo)
            pInfo.gameObject.SetActive(false);
    }

    public void ShowFeature(int iFeature)
    {
        GameObject pObj = SysUI.pthis.CreateUI(gameObject, "Prefab/S_Feature");
        pObj.GetComponent<Lb_Feature>().SetFeature(iFeature);        
    }

    public void ShowEquip(GameObject pHand, int iEquip, Shader pShader)
    {
        GameObject pObj = SysUI.pthis.CreateUI(gameObject, "Prefab/S_Weapon");
        pObj.transform.localPosition = new Vector3(0, -73, 0);

        GameObject ObjWeapon = UITool.pthis.CreateUI(pHand, "Prefab/" + (ENUM_Weapon)iEquip);
        UI2DSprite[] p2DS = ObjWeapon.GetComponentsInChildren<UI2DSprite>();

        for (int i = 0; i < p2DS.Length; i++)
            p2DS[i].shader = pShader;
    }
}
