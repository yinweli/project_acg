using UnityEngine;
using System.Collections;

public class Btn_DelTarget : MonoBehaviour 
{
    public GameObject pTarget = null;

    void OnClick()
    {
        if (pTarget)
            Destroy(pTarget);
    }
}
