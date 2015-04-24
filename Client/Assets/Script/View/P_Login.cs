using UnityEngine;
using System.Collections;

public class P_Login : MonoBehaviour 
{
    void Start()
    {
        // 選音樂.
        AudioCtrl.pthis.PlayMusic("Start");
    }
	// Update is called once per frame
    void Update()
    {
        if (GetComponent<UIPanel>().alpha < 0.002)
        {
            SysMain.pthis.CheckStart();
            Destroy(gameObject);
        }
    }
}
