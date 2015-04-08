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
        {
            bIsRun = false;
            StartCoroutine(StaRecovery());
        }
    }
    // ------------------------------------------------------------------
    IEnumerator StaCost()
    {
        if (SysMain.pthis.bCanRun)
            SysMain.pthis.fRunDouble = 3.0f;

        while (bIsRun && SysMain.pthis.bCanRun)
        {            
            if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else
            {
                if (SysMain.pthis.AddStamina(-SysMain.pthis.iStaminaCost))
                    SysMain.pthis.fRunDouble = 3.0f;
                fCoolDown = Time.time + 1.0f;
            }
        }

        if (!SysMain.pthis.bCanRun && SysMain.pthis.Data.iStamina < 10)
            SysMain.pthis.fRunDouble = 0;
        else
            SysMain.pthis.fRunDouble = 1.0f;

        bIsRun = false;
        fCoolDown = Time.time + 1.0f;
    }
    // ------------------------------------------------------------------
    IEnumerator StaRecovery()
    {
        if (!SysMain.pthis.bCanRun && SysMain.pthis.Data.iStamina < 10)
            SysMain.pthis.fRunDouble = 0;
        else
            SysMain.pthis.fRunDouble = 1.0f;

        while (SysMain.pthis.bIsGaming)
        {
            if (fCoolDown > Time.time)
                yield return new WaitForEndOfFrame();
            else if (bIsRun && !SysMain.pthis.bCanRun)
                yield return new WaitForEndOfFrame();
            else 
            {
                SysMain.pthis.AddStamina(SysMain.pthis.Data.iStaminaRecovery);
                fCoolDown = Time.time + 1.0f;
            }
        }        
    }
}
