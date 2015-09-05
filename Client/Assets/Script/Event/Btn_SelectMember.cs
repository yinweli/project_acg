using UnityEngine;
using System.Collections;

public class Btn_SelectMember : MonoBehaviour
{
    public int iListID = -1;

    void OnClick()
    {
        P_AddMember.pthis.MoveSelect(iListID);
    }
}
