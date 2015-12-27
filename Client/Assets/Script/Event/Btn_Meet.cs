using UnityEngine;
using System.Collections;

public class Btn_Meet : MonoBehaviour
{
    public Animator pAni = null;

	void OnClick()
    {
        pAni.Play("open");
    }

    public void AddMeet()
    {
        SysMain.pthis.ListMeet.Add(gameObject);
    }
}
