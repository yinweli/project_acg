using UnityEngine;
using System.Collections;

public class Btn_GetCurrency : MonoBehaviour {

    public int iItemID = 0;

    public Shader ClickShader;

    public UI2DSprite pSprite;
    public UILabel pLbCount;
    // ------------------------------------------------------------------
    void OnPress(bool bIsPress)
    {
        if (bIsPress)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
            // 變更為不受光.
            pSprite.shader = ClickShader;
            pSprite.depth = 5998;
            // 取得數量.
            pLbCount.text = "+" + GameData.pthis.PickupList[iItemID].iCount;
            GetComponent<Animator>().Play("GetItem");
            // 飛行至定位.
            StartCoroutine(FlyToPos());
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Look")
            GetComponent<Animator>().Play("TalkShing");
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Look")
            GetComponent<Animator>().Play("Wait");
    }
    // ------------------------------------------------------------------
    IEnumerator FlyToPos()
    {
        //放大
        int iCount = 1;
        while (iCount <= 9)
        {
            pSprite.transform.localScale = new Vector3(pSprite.transform.localScale.x + (0.01f * iCount), pSprite.transform.localScale.y + (0.01f * iCount), 1);
            iCount++;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.8f);

        Vector3 VecPos = P_UI.pthis.ObjCurrency.transform.position;

        float fFrame = 1;
        while (Vector2.Distance(pSprite.transform.position, VecPos) > 0.03f)
        {
            yield return new WaitForEndOfFrame();
            ToolKit.MoveTo(gameObject, VecPos - pSprite.transform.position, 0.82f * fFrame);
            fFrame += 0.05f;
        }

		GoogleAnalytics.pthis.LogEvent("Count", "Pickup Currency", "", 0);

        GameData.pthis.PickupList[iItemID].bPickup = true;
		Rule.CurrencyAdd(GameData.pthis.PickupList[iItemID].iCount);
        P_UI.pthis.UpdateCurrency();

        Destroy(gameObject);
    }
    
}
