using UnityEngine;
using System.Collections;

public class Btn_Bomb : MonoBehaviour 
{
    public UILabel pLbCount;
    // 冷卻.
    public float fCoolDown = 0;

    void Update()
    {
        pLbCount.text = PlayerData.pthis.iBomb.ToString();
    }

    void OnClick()
    {
        if (fCoolDown > Time.time || PlayerData.pthis.iBomb <= 0)
            return;

        GetComponent<UIButton>().isEnabled = false;

        // 播放大絕.
        SysBomb.pthis.StartBomb();

        // 計算冷卻.
        fCoolDown = Time.time + 5;

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        while (fCoolDown > Time.time)
            yield return new WaitForEndOfFrame();

        GetComponent<UIButton>().isEnabled = true;       
    }
}
