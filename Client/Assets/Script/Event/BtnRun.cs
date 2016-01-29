using UnityEngine;
using System.Collections;

public class BtnRun : MonoBehaviour
{
    public bool bIsRun = false;
    public float fCoolDown = 1;
    public UISprite pBg = null;
    // ------------------------------------------------------------------
    public void StartNew()
    {
        RecoveryTime();
        StartCoroutine(StaRecovery());
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        if (GetComponent<UIButton>().isEnabled && !CheckCanMove())
            GetComponent<UIButton>().isEnabled = false;
        else if (!GetComponent<UIButton>().isEnabled && CheckCanMove())
            GetComponent<UIButton>().isEnabled = true;

        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Space) && CheckCanMove())
            PressDownSpace();
        else if (Input.GetKeyUp(KeyCode.Space))
            PressUpSpace();
    }
    // ------------------------------------------------------------------
    void OnPress(bool bDown)
    {
        if (bDown && CheckCanMove ()) {
			NGUITools.PlaySound(Resources.Load("Sound/FX/Run") as AudioClip);
			StartCoroutine (StaCost ());
		}else
            bIsRun = false;
    }
    // ------------------------------------------------------------------
    void PressDownSpace()
    {
        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().enabled = false;

        NGUITools.PlaySound(Resources.Load("Sound/FX/Run") as AudioClip);
        pBg.color = new Color(0.72f, 0.64f, 0.48f);
        StartCoroutine(StaCost());
    }
    // ------------------------------------------------------------------
    void PressUpSpace()
    {
        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().enabled = true;
        pBg.color = Color.white;
        bIsRun = false;
    }
    // ------------------------------------------------------------------
    public bool CheckCanMove()
    {
        if (!SysMain.pthis.bCanRun)
            return false;

        if (ToolKit.CatchRole.Count <= 0)
            return false;

        return true;
    }
    // ------------------------------------------------------------------
    IEnumerator StaCost()
    {
        bIsRun = true;

        if (CheckCanMove() && SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
        {
            ConsumeTime();
            DataGame.pthis.fRunDouble = 3.0f;
            yield return new WaitForSeconds(0.9f);
        }

        while (bIsRun && CheckCanMove())
        {
            if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
                if (SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
                    DataGame.pthis.fRunDouble = 3.0f;
                ConsumeTime();
                yield return new WaitForSeconds(0.9f);
            }
        }

        if (!CheckCanMove() && DataPlayer.pthis.iStamina < 10)
        {
            GetComponent<UIButton>().isEnabled = false;
            DataGame.pthis.fRunDouble = 0;
        }
        else
            DataGame.pthis.fRunDouble = 1.0f;

        bIsRun = false;
        RecoveryTime();
    }
    // ------------------------------------------------------------------
    IEnumerator StaRecovery()
    {
        while (SysMain.pthis.bIsGaming)
        {
            if (bIsRun && SysMain.pthis.bCanRun)
                yield return new WaitForEndOfFrame();
            else if (DataPlayer.pthis.iStamina >= DataPlayer.pthis.iStaminaLimit)
                yield return new WaitForEndOfFrame();
            else if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
                RecoveryTime();

                SysMain.pthis.AddStamina(1);

                if (SysMain.pthis.bCanRun && DataPlayer.pthis.iStamina >= 20)
                    GetComponent<UIButton>().isEnabled = true;
            }
        }
    }
    // ------------------------------------------------------------------
    void RecoveryTime()
    {
        if (DataPlayer.pthis.iStaminaRecovery > 0)
            fCoolDown = Time.time + (float)GameDefine.iStaminaRecoveryTime / DataPlayer.pthis.iStaminaRecovery;
        else
            fCoolDown = Time.time + (float)GameDefine.iStaminaRecoveryTime;
    }
    // ------------------------------------------------------------------
    void ConsumeTime()
    {
        fCoolDown = Time.time + GameDefine.iStaminaConsumeTime;
    }
}
