﻿using UnityEngine;
using System.Collections;

public class P_UI : MonoBehaviour {

    static public P_UI pthis = null;

    public UILabel[] pLbBullet = new UILabel[(int)ENUM_Resource.Resource_Count-1];

    void Awake()
    {
        pthis = this;
    }

    // Use this for initialization
    void Update()
    {
        for (int i = 0; i < (int)ENUM_Resource.Resource_Count - 1; i++)
            if (pLbBullet[i])
                pLbBullet[i].text = SysMain.pthis.Data.Resource[i].ToString();
    }

    public bool UseBullet(WeaponType pType)
    {
        DBFEquip DataEquip = GameDBF.This.GetEquip((int)pType) as DBFEquip;

        if (DataEquip == null)
            return false;

        ENUM_Resource emResource = (ENUM_Resource)DataEquip.Resource;

        if (Rule.ResourceChk(emResource, 1) == false)
            return false;

        Rule.ResourceAdd(emResource, -1);

        return true;
    }
}
