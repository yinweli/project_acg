﻿using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
    public AIPlayer pAI = null;
    public int iCount = 0;

    void Start()
    {
        pAI = transform.parent.gameObject.GetComponent<AIPlayer>();

        if (pAI)
            iCount = PlayerData.pthis.Members[pAI.iPlayer].iShield;
        else
            Destroy(gameObject);
    }

    public void CostShield()
    {
        iCount--;

        if (iCount <= 0)
            Destroy(gameObject);
    }
}