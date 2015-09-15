using UnityEngine;
using System.Collections;

public class Btn_CrystalShop : MonoBehaviour 
{
    public GameObject pTarget = null;
    public UILabel pLbTime = null;

    public GameObject pP_CrystalShop = null;

    public int iTimeCount = GameDefine.iCrystalTime;
    // ------------------------------------------------------------------
    void Start()
    {
        if (DataGame.pthis.iWeaponType[0] == (int)ENUM_Weapon.Null && DataGame.pthis.iWeaponType[1] == (int)ENUM_Weapon.Null)
        {
            pLbTime.gameObject.SetActive(false);
            gameObject.SetActive(false);
            return;
        }

        iTimeCount = GameDefine.iCrystalTime;
        StartCoroutine(IE_CrystalCount());
    }
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        pLbTime.text = string.Format("{0:00}:{1:00}", iTimeCount / 60, iTimeCount % 60);
        if (pP_CrystalShop && iTimeCount <= 0)
            Destroy(pP_CrystalShop);

        if (pTarget && iTimeCount <= 0)
            Destroy(pTarget);
        

        if (!pP_CrystalShop)
            GetComponent<BoxCollider2D>().enabled = true;
	}
    // ------------------------------------------------------------------
    void OnClick()
    {
        pP_CrystalShop = SysUI.pthis.CreatePanel("Prefab/P_CrystalShop");
        pP_CrystalShop.GetComponent<P_CrystalShop>().pMan = this;

        GetComponent<BoxCollider2D>().enabled = false;
    }
    // ------------------------------------------------------------------
    IEnumerator IE_CrystalCount()
    {
        while (iTimeCount > 0)
        {
            iTimeCount--;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
