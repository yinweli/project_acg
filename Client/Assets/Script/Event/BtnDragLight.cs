using UnityEngine;
using System.Collections;

public class BtnDragLight : MonoBehaviour
{
    public GameObject ObjLight;

    void OnDrag(Vector2 delta)
    {
        float fFinalDeg = 0;
        float fDeg = Mathf.Atan(transform.localPosition.x / transform.localPosition.y) * Mathf.Rad2Deg;

        if (transform.localPosition.x > 0 && (int)Mathf.Abs(fDeg) == 90)
            fFinalDeg = 0;
        else if (transform.localPosition.x < 0 && (int)Mathf.Abs(fDeg) == 90)
            fFinalDeg = 180;
        else if (transform.localPosition.y > 0) // 第一象限.第三象限
            fFinalDeg = fDeg - 90;
        else
            fFinalDeg = fDeg + 90;

        Debug.Log("fDeg: " + fDeg + ", fFinalDeg: " + fFinalDeg);
        ObjLight.transform.localEulerAngles = new Vector3(fFinalDeg, 90, 90);
    }
}

