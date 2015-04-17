using UnityEngine;
using System.Collections;

public class Btn_GetResource : MonoBehaviour 
{
    public ENUM_Resource enumType;

    public Shader ClickShader;

    public UI2DSprite pSprite;

    void OnClick()
    {
        gameObject.GetComponent<UIButton>().isEnabled = false;
        // 變更為不受光.
        pSprite.shader = ClickShader;
        pSprite.depth = 10000;
        // 飛行至定位.
        StartCoroutine(FlyToPos());
    }

    IEnumerator FlyToPos()
    {
        //轉加放大
        int iCount = 1;
        while (iCount <= 9)
        {
            transform.Rotate(0,0,-10);
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
        while (Vector2.Distance(transform.position, VecPos) > 0.01f)
        {
            yield return new WaitForEndOfFrame();
            MoveTo(VecPos - transform.position, 0.8f * fFrame);
            fFrame += 0.05f;
        }

        if (enumType == ENUM_Resource.Battery)
            PlayerData.pthis.Resource[(int)ENUM_Resource.Battery] += 10;
        else if (enumType == ENUM_Resource.LightAmmo)
            PlayerData.pthis.Resource[(int)ENUM_Resource.LightAmmo] += 10;
        else if (enumType == ENUM_Resource.HeavyAmmo)
            PlayerData.pthis.Resource[(int)ENUM_Resource.HeavyAmmo] += 10;

        Destroy(gameObject);
    }

    // ------------------------------------------------------------------
    void MoveTo(Vector3 vecDirection, float fSpeed)
    {
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
}
