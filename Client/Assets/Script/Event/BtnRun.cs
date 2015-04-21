using UnityEngine;
using System.Collections;

public class BtnRun : MonoBehaviour 
{
    public bool bIsRun = false;
    public float fCoolDown = 1;
    // ------------------------------------------------------------------
    public void StartNew()
    {
		fCoolDown = Time.time + GameDefine.iStaminaTime;
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
			NextTime();
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
				NextTime();
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
		NextTime();
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
				NextTime();

                SysMain.pthis.AddStamina(PlayerData.pthis.iStaminaRecovery);

                if (SysMain.pthis.bCanRun && PlayerData.pthis.iStamina >= 20)
                    GetComponent<UIButton>().isEnabled = true;
            }
        }
    }
	// ------------------------------------------------------------------
	void NextTime()
	{
		fCoolDown = Time.time + GameDefine.iStaminaTime;
	}
}
