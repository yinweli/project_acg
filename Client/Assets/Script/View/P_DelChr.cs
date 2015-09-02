using UnityEngine;
using System.Collections;

public class P_DelChr : MonoBehaviour
{
    public GameObject pObjChr = null;
    public G_ListRole pData = null;

    public UILabel pLb_Money = null;
    // ------------------------------------------------------------------
    void Start()
    {
        NGUITools.AddChild(pObjChr, pData.ObjHuman);
        pLb_Money.text = "" + GameDefine.iPriceLayoff;
    }
    // ------------------------------------------------------------------
}
