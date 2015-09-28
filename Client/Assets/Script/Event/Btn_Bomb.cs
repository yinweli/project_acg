using UnityEngine;
using System.Collections;

public class Btn_Bomb : MonoBehaviour
{
    // 冷卻.
    public float fCoolDown = 0;
    UIButton pBtn;
    // ------------------------------------------------------------------
    void Start()
    {
        pBtn = GetComponent<UIButton>();
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        if (DataPlayer.pthis.iBomb <= 0 && pBtn.isEnabled)
        {
            pBtn.isEnabled = false;
            return;
        }

        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.LeftControl))
            PressDownLCtrl();
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        StartBomb();
    }
    // ------------------------------------------------------------------
    void PressDownLCtrl()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        NGUITools.PlaySound(Resources.Load("Sound/FX/Select") as AudioClip);
        StartBomb();
    }
    // ------------------------------------------------------------------
    void StartBomb()
    {
        if (fCoolDown > Time.time || DataPlayer.pthis.iBomb <= 0)
            return;

        pBtn.isEnabled = false;

        // 播放大絕.
        SysBomb.pthis.StartBomb();

        // 計算冷卻.
        fCoolDown = Time.time + 3;

        GoogleAnalytics.pthis.LogEvent("Count", "Use Bomb", "", 0);

        StartCoroutine(CoolDown());
    }
    // ------------------------------------------------------------------
    public void ResetBomb()
    {
        StopAllCoroutines();
        fCoolDown = 0;
        pBtn.isEnabled = true;
    }
    // ------------------------------------------------------------------
    IEnumerator CoolDown()
    {
        while (fCoolDown > Time.time)
            yield return new WaitForEndOfFrame();

        pBtn.isEnabled = true;
    }
}
