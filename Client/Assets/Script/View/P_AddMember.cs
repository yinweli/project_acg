using UnityEngine;
using System.Collections;

public class P_AddMember : MonoBehaviour
{
    static public P_AddMember pthis = null;

    public G_RandRole pRandRole = null;
    public G_HireList pHireList = null;

    public Btn_Hire pBtn_Hire = null;

    public UILabel Lb_Currency = null;

    public Animator pAni_RandRole;    

    public int iNowSelect = -1;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    void Start()
    {
        UpdateCurrency();
    }
    // ------------------------------------------------------------------
    public void GetNewRole()
    {
        UpdateCurrency();
        pRandRole.gameObject.SetActive(true);
        pRandRole.StartRand();
        DataPlayer.pthis.Save();
        
        pAni_RandRole.speed = 1;
        pAni_RandRole.Play("RandRole", -1, 0);
    }
    // ------------------------------------------------------------------
    public GameObject CreateRole(GameObject ObjParent, Member pMember)
    {
        // 建立外觀.
        GameObject ObjHuman = UITool.pthis.CreateRole(ObjParent, pMember.iLooks);
        ToolKit.AddWeaponTo2DSprite(ObjHuman, (ENUM_Weapon)pMember.iEquip, 15);
        return ObjHuman;
    }
    // ------------------------------------------------------------------
    public void UpdateCurrency()
    {
        Lb_Currency.text = DataPlayer.pthis.iCurrency.ToString();
    }
    // ------------------------------------------------------------------
    public void HireSelect()
    {
        if (iNowSelect >= DataPlayer.pthis.MemberDepot.Count)
            return;

        // 加到隊伍角色.
        DataPlayer.pthis.MemberParty.Add(DataPlayer.pthis.MemberDepot[iNowSelect]);
        // 刪除所選角色.
        DataPlayer.pthis.MemberDepot.RemoveAt(iNowSelect);
        pHireList.ClearList();

        pBtn_Hire.CheckStatu();

        DataPlayer.pthis.Save();

        if (P_Victory.pthis != null && P_Victory.pthis.pFeature != null)
            P_Victory.pthis.pFeature.AddChr(DataPlayer.pthis.MemberParty.Count - 1);
    }
    // ------------------------------------------------------------------
    public void CheckShowEnd(int iIndex)
    {
        if (pRandRole.iRightIndex != iIndex)
            return;

        StartCoroutine(WaitforShow());
    }
    // ------------------------------------------------------------------
    public void MoveSelect(int iSelect)
    {
        if (!pHireList.ObjHire.ContainsKey(iSelect))
            return;

        iNowSelect = iSelect;

        pBtn_Hire.MovePos(pHireList.ObjHire[iSelect].transform.position);        
    }
    // ------------------------------------------------------------------
    IEnumerator WaitforShow()
    {
        pAni_RandRole.speed = 0;
        yield return new WaitForSeconds(1.5f);

        NGUITools.PlaySound(Resources.Load("Sound/FX/RRoleEnd") as AudioClip, 1, 1);
        pRandRole.gameObject.SetActive(false);
        pHireList.Refresh();
        pHireList.PickNew();
    }
}
