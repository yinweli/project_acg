using UnityEngine;
using System.Collections;

public class Btn_AddRole : MonoBehaviour
{
    public UILabel Lb_Money = null;
    public UIButton pBtn;
    // ------------------------------------------------------------------
    void Start()
    {
        Lb_Money.text = GameDefine.iPriceHire.ToString();

        if (DataPlayer.pthis.iCurrency < GameDefine.iPriceHire || DataPlayer.pthis.MemberDepot.Count >= GameDefine.iMaxMemberDepot)
            pBtn.isEnabled = false;
        else
            pBtn.isEnabled = true;
    }
    // ------------------------------------------------------------------
	void OnClick()
    {
        if (DataPlayer.pthis.iCurrency < GameDefine.iPriceHire || DataPlayer.pthis.MemberDepot.Count >= GameDefine.iMaxMemberDepot)
            return;

        DataPlayer.pthis.iCurrency -= GameDefine.iPriceHire;
        P_UI.pthis.UpdateCurrency();
        DataPlayer.pthis.Save();

        P_AddMember.pthis.GetNewRole();
    }
    // ------------------------------------------------------------------
}
