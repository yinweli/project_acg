using UnityEngine;
using System.Collections;

public class P_Pause : MonoBehaviour
{
    // ------------------------------------------------------------------
    void OnGUI()
    {
        if (SysMain.pthis.bIsGaming && Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Escape))
            Time.timeScale = 1;
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if(Time.timeScale > 0)
            Destroy(gameObject);
    }
}
