using UnityEngine;
using System.Collections;

public class G_Talk : MonoBehaviour 
{
    public UILabel pLb = null;
    public UISprite pSBg = null;

    int iMaxRan = 5;
    // ------------------------------------------------------------------
    public void Talk(bool bNeedRan, string pTalk, int iSort)
    {
        StartCoroutine(WaitTalk(bNeedRan, pTalk, iSort));
    }
    // ------------------------------------------------------------------
    IEnumerator WaitTalk(bool bNeedRan, string pTalk, int iSort)
    {
        float fSec = 0.1f;
        if (bNeedRan)
            fSec = Random.Range(2.8f, 6.5f);
        
        yield return new WaitForSeconds(fSec);

        pLb.depth = 6001 - (iSort * 5);
        pSBg.depth = pLb.depth - 1;

        // 設定位置.
        transform.localPosition = new Vector3(0, 90, 0);

        int iRandom = iMaxRan;
        if (bNeedRan)
            iRandom = Random.Range(0, 5);

        Debug.Log("Talk Ran: " + iRandom);
        // 沒電.
        if (pTalk == "Battery")
            pTalk = GetStrBattery(iRandom);
        // 可以跑步.
        else if (pTalk == "Run")
            pTalk = GetStrRun(iRandom);
        // 被抓住.
        else if (pTalk == "Help")
            pTalk = GetStrHelp(iRandom);

        pLb.text = pTalk;

        pSBg.width = pLb.width + 10;
        pSBg.height = pLb.height + 5;

        GetComponent<Animator>().Play("TalkFadIn");

        if (iRandom == iMaxRan)
            yield return new WaitForSeconds(5);
        else
            yield return new WaitForSeconds(3);
        
        GetComponent<Animator>().Play("TalkFadOut");
    }
    // ------------------------------------------------------------------
    public void DelTalk()
    {
        Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    string GetStrBattery(int iRan)
    {
        if (iRan == 0)
            return "No!!!";
        else if (iRan == 1)
            return "Gosh!";
        else if (iRan == 2)
            return "Oh my God!";
        else if (iRan == 3)
            return "Darkness Coming.";
        else if (iRan == 4)
            return "What happened?";
        else
            return "Flashlight running out of power.";
    }
    // ------------------------------------------------------------------
    string GetStrRun(int iRan)
    {
        if (iRan == 0)
            return "Run!!!";
        else if (iRan == 1)
            return "Energetic!";
        else if (iRan == 2)
            return "Too slow?";
        else if (iRan == 3)
            return "Can we Speed up?";
        else if (iRan == 4)
            return "I fell asleep.";
        else
            return "Time to full speed ahead.";
    }
    // ------------------------------------------------------------------
    string GetStrHelp(int iRan)
    {
        if (iRan == 0)
            return "No!!!";
        else if (iRan == 1)
            return "Shot it!!!";
        else if (iRan == 2)
            return "Kill Monster!!";
        else if (iRan == 3)
            return "Please Don't!";
        else if (iRan == 4)
            return "He loves me!";
        else
            return "Help me!";
    }
    // ------------------------------------------------------------------
}
