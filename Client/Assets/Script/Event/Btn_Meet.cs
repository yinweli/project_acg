using UnityEngine;
using System.Collections;

public class Btn_Meet : MonoBehaviour
{
    public Animator pAni = null;
    public GameObject ObjLb = null;

    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            Destroy(gameObject);
    }

	void OnClick()
    {
        pAni.Play("open");
        // 關閉點擊
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(ObjLb);
    }

    public void AddMeet()
    {
        SysMain.pthis.ListMeet.Add(gameObject);
    }

    public void DelAMeet()
    {
        SysMain.pthis.ListMeet.Remove(gameObject);
        Destroy(gameObject);
    }
}
