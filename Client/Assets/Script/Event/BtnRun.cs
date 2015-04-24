using UnityEngine;
using System.Collections;

public class BtnRun : MonoBehaviour 
{
    public bool bIsRun = false;
    public float fCoolDown = 1;
    // ------------------------------------------------------------------
    public void StartNew()
    {
		RecoveryTime();
        StartCoroutine(StaRecovery());
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if(GetComponent<UIButton>().isEnabled && !CheckCanMove())
            GetComponent<UIButton>().isEnabled = false;
        else if (!GetComponent<UIButton>().isEnabled && CheckCanMove())
            GetComponent<UIButton>().isEnabled = true;
    }
    // ------------------------------------------------------------------
	void OnPress(bool bDown)
    {
        if (bDown && CheckCanMove())
        {
            bIsRun = true;
            StartCoroutine(StaCost());
        }
        else
            bIsRun = false;
    }
    // ------------------------------------------------------------------
    public bool CheckCanMove()
    {
        if (!SysMain.pthis.bCanRun)
            return false;

        if (SysMain.pthis.CatchRole.Count <= 0)
            return false;

        return true;
    }
    // ------------------------------------------------------------------
    IEnumerator StaCost()
    {
        if (CheckCanMove() && SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
        {
			ConsumeTime();
            GameData.pthis.fRunDouble = 3.0f;
            yield return new WaitForSeconds(0.9f);
        }

        while (bIsRun && CheckCanMove())
        {            
            if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
				if (SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
                    GameData.pthis.fRunDouble = 3.0f;
				ConsumeTime();
                yield return new WaitForSeconds(0.9f);
            }
        }

        if (!CheckCanMove() && PlayerData.pthis.iStamina < 10)
        {
            GetComponent<UIButton>().isEnabled = false;
            GameData.pthis.fRunDouble = 0;
        }
        else
            GameData.pthis.fRunDouble = 1.0f;

        bIsRun = false;
		RecoveryTime();
    }
    // ------------------------------------------------------------------
    IEnumerator StaRecovery()
    {
        if (!CheckCanMove() && PlayerData.pthis.iStamina < 10)
            GameData.pthis.fRunDouble = 0;
        else
            GameData.pthis.fRunDouble = 1.0f;

        while (SysMain.pthis.bIsGaming)
        {
            if (bIsRun && SysMain.pthis.bCanRun)
                yield return new WaitForEndOfFrame();
			else if (PlayerData.pthis.iStamina >= PlayerData.pthis.iStaminaLimit)
				yield return new WaitForEndOfFrame();
            else if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
				RecoveryTime();

                SysMain.pthis.AddStamina(1);

                if (SysMain.pthis.bCanRun && PlayerData.pthis.iStamina >= 20)
                    GetComponent<UIButton>().isEnabled = true;
            }
        }
    }
	// ------------------------------------------------------------------
	void RecoveryTime()
	{
		if(PlayerData.pthis.iStaminaRecovery > 0)
			fCoolDown = Time.time + (float)GameDefine.iStaminaRecoveryTime / PlayerData.pthis.iStaminaRecovery;
		else
			fCoolDown = Time.time + (float)GameDefine.iStaminaRecoveryTime;
	}
	// ------------------------------------------------------------------
	void ConsumeTime()
	{
		fCoolDown = Time.time + GameDefine.iStaminaConsumeTime;
	}
}
