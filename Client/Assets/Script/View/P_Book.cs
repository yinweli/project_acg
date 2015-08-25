using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class P_Book : MonoBehaviour 
{
    public UIGrid pGrid;
    public UISprite pSelect;
    public Btn_BookBtn[] pBookBtn = new Btn_BookBtn[(int)ENUM_BookBtn.Count];
    public GameObject[] BG = new GameObject[2];

    public List<GameObject> NowObj = new List<GameObject>();
    // ------------------------------------------------------------------
    void Start()
    {
        ResetBtn();
        SetSelect(pBookBtn[0].gameObject, pBookBtn[0].pMySprite);
        CreatePage(pBookBtn[0].pType);

        pBookBtn[0].pBtn.isEnabled = false;
    }
    // ------------------------------------------------------------------
    public void CreatePage(ENUM_BookBtn pType)
    {
        if (pType == ENUM_BookBtn.Update)
            OpenUpgrade();
        else if (pType == ENUM_BookBtn.Achievement)
            OpenAchieve();
        else if (pType == ENUM_BookBtn.Record)
            OpenRecord();
    }
    // ------------------------------------------------------------------    
    public void OpenUpgrade()
    {
        ClearNowPage();
        SetBg(0);

        for (int i = 1; i < (int)ENUM_Weapon.Count; i++)
        {
            GameObject pObj = UITool.pthis.CreateUI(pGrid.gameObject, "Prefab/G_Upgrade");
            G_Upgrade pScript = pObj.GetComponent<G_Upgrade>();
            if (pScript)
                pScript.pWeapon = (ENUM_Weapon)i;

            NowObj.Add(pObj);
        }

        pGrid.Reposition();
    }
    // ------------------------------------------------------------------
    public void OpenAchieve()
    {
        ClearNowPage();

        SetBg(0);
    }
    // ------------------------------------------------------------------
    public void OpenRecord()
    {
        ClearNowPage();

        SetBg(1);
        NowObj.Add(UITool.pthis.CreateUI(gameObject, "Prefab/G_Record"));
    }
    // ------------------------------------------------------------------
    public void ClearNowPage()
    {
        foreach (GameObject itor in NowObj)
            Destroy(itor);

        NowObj.Clear();
    }
    // ------------------------------------------------------------------
    public void SetSelect(GameObject pObjBtn, UISprite pS_Btn)
    {
        pSelect.gameObject.transform.localPosition = pObjBtn.transform.localPosition;
        pSelect.width = pS_Btn.width + 4;
        pSelect.height = pS_Btn.height + 1;
    }
    // ------------------------------------------------------------------
    public void ResetBtn()
    {
        foreach (Btn_BookBtn pBtn in pBookBtn)
            pBtn.pBtn.isEnabled = true;
    }
    // ------------------------------------------------------------------
    public void SetBg(int index)
    {
        foreach (GameObject pBg in BG)
            pBg.SetActive(false);

        BG[index].SetActive(true);
    }
    // ------------------------------------------------------------------
}
