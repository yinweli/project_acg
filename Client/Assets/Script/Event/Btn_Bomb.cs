﻿using UnityEngine;
using System.Collections;

public class Btn_Bomb : MonoBehaviour 
{
    public UILabel pLbCount;    
    // 冷卻.
    public float fCoolDown = 0;

    UIButton pBtn;

    void Start()
    {
        pBtn = GetComponent<UIButton>();
    }

    void Update()
    {
        if (PlayerData.pthis.iBomb <= 0 && pBtn.isEnabled)
            pBtn.isEnabled = false;

        pLbCount.text = PlayerData.pthis.iBomb.ToString();
    }

    void OnClick()
    {
        if (fCoolDown > Time.time || PlayerData.pthis.iBomb <= 0)
            return;

        pBtn.isEnabled = false;

        // 播放大絕.
        SysBomb.pthis.StartBomb();

        // 計算冷卻.
        fCoolDown = Time.time + 3;

		GoogleAnalytics.pthis.LogEvent("Count", "Use Bomb", "", 0);

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        while (fCoolDown > Time.time)
            yield return new WaitForEndOfFrame();

        pBtn.isEnabled = true;       
    }
}
