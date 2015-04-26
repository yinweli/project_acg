using UnityEngine;
using System.Collections;

public class Btn_GetResource : MonoBehaviour 
{
    public int iItemID = 0;

    public ENUM_Resource enumType;

    public Shader ClickShader;

    public UI2DSprite pSprite;
    public UILabel pLbCount;
    // ------------------------------------------------------------------
    void OnPress(bool bIsPress)
    {
        if (bIsPress)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            NGUITools.PlaySound(Resources.Load("Sound/FX/GetItem") as AudioClip);
            // 變更為不受光.
            pSprite.shader = ClickShader;
            pSprite.depth = 10000;
            // 取得數量.
            pLbCount.text = "+" + GameData.pthis.PickupList[iItemID].iCount;
            GameData.pthis.PickupList[iItemID].bPickup = true;
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
        //轉加放大
        int iCount = 1;
        while (iCount <= 9)
        {
            pSprite.transform.Rotate(0, 0, -10);
            pSprite.transform.localScale = new Vector3(pSprite.transform.localScale.x + (0.01f * iCount), pSprite.transform.localScale.y + (0.01f * iCount), 1);
            iCount++;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.8f);

        Vector3 VecPos = Vector3.zero;
        if (enumType == ENUM_Resource.Battery)
            VecPos = P_UI.pthis.ObjBattery.transform.position;
        else if (enumType == ENUM_Resource.LightAmmo)
            VecPos = P_UI.pthis.ObjAmmoLight.transform.position;
        else if (enumType == ENUM_Resource.HeavyAmmo)
            VecPos = P_UI.pthis.ObjAmmoHeavy.transform.position;

        float fFrame = 1;
        while (Vector2.Distance(pSprite.transform.position, VecPos) > 0.03f)
        {
            yield return new WaitForEndOfFrame();
            MoveTo(VecPos - pSprite.transform.position, 0.8f * fFrame);
            fFrame += 0.05f;
        }

		Rule.ResourceAdd(enumType, GameData.pthis.PickupList[iItemID].iCount);
		/*
        if (enumType == ENUM_Resource.Battery)
            PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] += GameData.pthis.PickupList[iItemID].iCount;
        else if (enumType == ENUM_Resource.LightAmmo)
            PlayerData.pthis.Resource[(int)ENUM_Resource.LightAmmo] += GameData.pthis.PickupList[iItemID].iCount;
        else if (enumType == ENUM_Resource.HeavyAmmo)
            PlayerData.pthis.Resource[(int)ENUM_Resource.HeavyAmmo] += GameData.pthis.PickupList[iItemID].iCount;
		*/
        P_UI.pthis.UpdateResource();

        Destroy(gameObject);
    }

    // ------------------------------------------------------------------
    void MoveTo(Vector3 vecDirection, float fSpeed)
    {
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        pSprite.transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
}
