using UnityEngine;
using System.Collections;

public class P_Pause : MonoBehaviour
{
    // ------------------------------------------------------------------
    void Update()
    {
        if(Time.timeScale > 0)
            Destroy(gameObject);
    }
}
