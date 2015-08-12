using UnityEngine;
using System.Collections;

public class BtnChangePage : MonoBehaviour 
{
    public int iPage = 0;
    public P_Victory pVictory = null;

    void OnClick()
    {
        GetComponent<UIButton>().isEnabled = false;
        pVictory.ChangePage(iPage);
    }
}
