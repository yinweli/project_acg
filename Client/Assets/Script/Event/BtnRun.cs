using UnityEngine;
using System.Collections;

public class BtnRun : MonoBehaviour
{
    public bool bIsRun = false;
    public float fCoolDown = 1;
    public UISprite pBg = null;

    public AudioClip Clip_Run;
    // ------------------------------------------------------------------
    public void StartNew()
    {
        CancelInvoke();
        CheckBtnEnble();
        InvokeRepeating("StaRecovery", 0, RecoveryTime());
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Space) && CheckCanMove())
            PressDownSpace();
        else if (Input.GetKeyUp(KeyCode.Space))
            PressUpSpace();
    }
    // ------------------------------------------------------------------
    void OnPress(bool bDown)
    {
        if (bDown && CheckCanMove ())
			StaCost();
		else
            bIsRun = false;
    }
    // ------------------------------------------------------------------
    void PressDownSpace()
    {
        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().enabled = false;

        pBg.color = new Color(0.72f, 0.64f, 0.48f);
        StaCost();
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
    void StaCost()
    {
        bIsRun = true;

        if (CheckCanMove() && SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
        {
            NGUITools.PlaySound(Clip_Run);
            DataGame.pthis.fRunDouble = 3.0f;
            Invoke("StopRun", GameDefine.fStaminaConsumeTime);
        }
    }
    // ------------------------------------------------------------------
    void StopRun()
    {
        // 還可以繼續跑.
        if (bIsRun && CheckCanMove() &&  SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
        {
            Invoke("StopRun", GameDefine.fStaminaConsumeTime);
            return;
        }

        if (!CheckCanMove() && DataPlayer.pthis.iStamina < 10)
        {
            GetComponent<UIButton>().isEnabled = false;
            DataGame.pthis.fRunDouble = 0;
        }
        else
            DataGame.pthis.fRunDouble = 1.0f;
    }
    // ------------------------------------------------------------------
    void StaRecovery()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        CheckBtnEnble();

        if (DataPlayer.pthis.iStamina >= DataPlayer.pthis.iStaminaLimit)
            return;
        
        if (bIsRun && SysMain.pthis.bCanRun)
            return;
                
        SysMain.pthis.AddStamina(1);

        if (SysMain.pthis.bCanRun && DataPlayer.pthis.iStamina >= 20)
            GetComponent<UIButton>().isEnabled = true;
             
    }
    // ------------------------------------------------------------------
    float RecoveryTime()
    {
        if (DataPlayer.pthis.iStaminaRecovery > 0)
            return (float)GameDefine.iStaminaRecoveryTime / DataPlayer.pthis.iStaminaRecovery;
        else
            return (float)GameDefine.iStaminaRecoveryTime;
    }
    // ------------------------------------------------------------------
    void CheckBtnEnble()
    {
        if (!CheckCanMove())
            GetComponent<UIButton>().isEnabled = false;
        else if (CheckCanMove())
            GetComponent<UIButton>().isEnabled = true;
    }
    // ------------------------------------------------------------------
}
