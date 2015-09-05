using UnityEngine;
using System.Collections;

public class G_ListRole : MonoBehaviour 
{
    public int iPlayerID = 0;
    public UILabel pLbLv = null;
    public G_Info pInfo = null;

    public GameObject ObjHuman = null;
    public GameObject ObjHand = null;

    public GameObject ObjInfo = null;
    public GameObject ObjFire = null;
    public GameObject ObjFrame = null;
    // ------------------------------------------------------------------
    void Start()
    {
        if (iPlayerID == -1)
        {
            ObjFrame.SetActive(true);
            pLbLv.text = "Lv --";
            GetComponent<BoxCollider2D>().enabled = false;
            return;
        }

        // 建立外觀.
        ObjHuman = UITool.pthis.CreateRole(gameObject, DataPlayer.pthis.MemberParty[iPlayerID].iLooks);
        ObjHand = ObjHuman.AddComponent<G_PLook>().ChangeTo2DSprite((ENUM_Weapon)DataPlayer.pthis.MemberParty[iPlayerID].iEquip);

        pLbLv.gameObject.SetActive(false);
        if (GetComponent<Animation>())
            GetComponent<Animation>().Stop();
    }
    // ------------------------------------------------------------------
    void Update()
    {
        // 檢查隊伍人數 如果只剩1人關閉開除按鈕.
        if (DataPlayer.pthis.MemberParty.Count <= 1)
            ObjFire.GetComponent<UIButton>().isEnabled = false;
    }
    // ------------------------------------------------------------------
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
    // ------------------------------------------------------------------
    public void ShowLevelUp(int iLv)
    {
        pLbLv.gameObject.SetActive(true);
        if (GetComponent<Animation>())
            GetComponent<Animation>().Play();
        ChangeLevel(iLv);
    }
    // ------------------------------------------------------------------
    public void ChangeLevel(int iLv)
    {
        pLbLv.text = "Lv " + iLv;
    }
    // ------------------------------------------------------------------
    public void ShowFeature(int iFeature)
    {
        GameObject pObj = SysUI.pthis.CreateUI(gameObject, "Prefab/S_Feature");
        pObj.GetComponent<Lb_Feature>().SetFeature(iFeature);
    }
    // ------------------------------------------------------------------
    public void ShowEquip(int iEquip)
    {
        GameObject pObj = SysUI.pthis.CreateUI(gameObject, "Prefab/S_Weapon");
        pObj.transform.localPosition = new Vector3(0, -73, 0);

        GameObject ObjWeapon = UITool.pthis.CreateUI(ObjHand, "Prefab/Weapon/" + (ENUM_Weapon)iEquip);
        SpriteRenderer[] p2DS = ObjWeapon.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer pRender in p2DS)
            ToolKit.ChangeTo2DSprite(pRender);
    }
    // ------------------------------------------------------------------
    public void Layoff()
    {   
        DataPlayer.pthis.MemberParty.RemoveAt(iPlayerID);
        DataPlayer.pthis.Save();

        if (P_Victory.pthis != null && P_Victory.pthis.pFeature != null)
            P_Victory.pthis.pFeature.DelChr(iPlayerID);
    }
}
