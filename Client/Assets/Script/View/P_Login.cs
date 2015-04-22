using UnityEngine;
using System.Collections;

public class P_Login : MonoBehaviour {

    void Start()
    {
        GetComponent<Animator>().Play("Wait");
    }
	// Update is called once per frame
    void Update()
    {
        if (GetComponent<UIPanel>().alpha == 0)
        {
            SysMain.pthis.NewGame();
            Destroy(gameObject);
        }
    }
}
