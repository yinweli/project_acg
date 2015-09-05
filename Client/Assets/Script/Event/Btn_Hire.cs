using UnityEngine;
using System.Collections;

public class Btn_Hire : MonoBehaviour
{
    public GameObject ObjQuestion = null;
    // ------------------------------------------------------------------
    void Start()
    {
        CheckStatu();
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        P_AddMember.pthis.HireSelect();        
    }
    // ------------------------------------------------------------------
    public void CheckStatu()
    {
        if (DataPlayer.pthis.MemberDepot.Count <= 0)
        {
            if (GetComponent<UIButton>())
                GetComponent<UIButton>().isEnabled = false;
            ObjQuestion.SetActive(true);
        }
        else
        {
            if (GetComponent<UIButton>())
                GetComponent<UIButton>().isEnabled = true;
            ObjQuestion.SetActive(false);
        }
    }	
    // ------------------------------------------------------------------
    public void MovePos(Vector3 vecTarget)
    {
        transform.position = vecTarget;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 50, 0);
        CheckStatu();
    }
}
