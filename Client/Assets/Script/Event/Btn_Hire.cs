using UnityEngine;
using System.Collections;

public class Btn_Hire : MonoBehaviour
{
    public UISprite pS_Check = null;
    // ------------------------------------------------------------------
    void Start()
    {
        CheckStatu();
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        P_AddMember.pthis.HireSelect();
        CheckStatu();
    }
    // ------------------------------------------------------------------
    public void CheckStatu()
    {
        if (DataPlayer.pthis.MemberDepot.Count <= 0)
        {
            if (GetComponent<UIButton>())
                GetComponent<UIButton>().isEnabled = false;
            pS_Check.color = Color.gray;
        }
        else
        {
            if (GetComponent<UIButton>())
                GetComponent<UIButton>().isEnabled = true;
            pS_Check.color = Color.white;
        }

        if (DataPlayer.pthis.MemberParty.Count >= GameDefine.iMaxMemberParty)
            if (GetComponent<UIButton>())
            {
                GetComponent<UIButton>().isEnabled = false;
                pS_Check.color = Color.gray;
            }
    }	
    // ------------------------------------------------------------------
    public void MovePos(Vector3 vecTarget)
    {
        transform.position = vecTarget;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 43, 0);
        CheckStatu();
    }
}
