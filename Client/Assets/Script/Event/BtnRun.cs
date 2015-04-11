using UnityEngine;
using System.Collections;

public class BtnRun : MonoBehaviour 
{
    public bool bIsRun = false;
    public float fCoolDown = 1;
    // ------------------------------------------------------------------
    void Start()
    {
        fCoolDown = Time.time + 1.0f;
        StartCoroutine(StaRecovery());
    }
    // ------------------------------------------------------------------
	void OnPress(bool bDown)
    {
        if (bDown)
        {
            bIsRun = true;
            StartCoroutine(StaCost());
        }
        else
            bIsRun = false;
    }
    // ------------------------------------------------------------------
    IEnumerator StaCost()
    {
        if (SysMain.pthis.bCanRun)
            GameData.pthis.fRunDouble = 3.0f;

        while (bIsRun && SysMain.pthis.bCanRun)
        {            
            if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
				if (SysMain.pthis.AddStamina(-GameDefine.iStaminaConsume))
                    GameData.pthis.fRunDouble = 3.0f;
                fCoolDown = Time.time + 1.0f;
            }
        }

        if (!SysMain.pthis.bCanRun && PlayerData.pthis.iStamina < 10)
        {
            GetComponent<UIButton>().isEnabled = false;
            GameData.pthis.fRunDouble = 0;
        }
        else
            GameData.pthis.fRunDouble = 1.0f;

        bIsRun = false;
        fCoolDown = Time.time + 1.0f;
    }
    // ------------------------------------------------------------------
    IEnumerator StaRecovery()
    {
        if (!SysMain.pthis.bCanRun && PlayerData.pthis.iStamina < 10)
            GameData.pthis.fRunDouble = 0;
        else
            GameData.pthis.fRunDouble = 1.0f;

        while (SysMain.pthis.bIsGaming)
        {
            if (bIsRun && SysMain.pthis.bCanRun)
            {
                Debug.Log("Recovery Pause!");
                yield return new WaitForEndOfFrame();
            }
            else if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
                SysMain.pthis.AddStamina(PlayerData.pthis.iStaminaRecovery);
                fCoolDown = Time.time + 1.0f;

                if (SysMain.pthis.bCanRun && PlayerData.pthis.iStamina >= 20)
                    GetComponent<UIButton>().isEnabled = true;
            }
        }        
    }
}
