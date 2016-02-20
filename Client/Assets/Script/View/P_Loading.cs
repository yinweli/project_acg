using UnityEngine;
using System.Collections;

public class P_Loading : MonoBehaviour 
{
    static public P_Loading pthis;
    public UILabel pLb_Text;
	void Awake () 
    {
        pthis = this;
	}

    public void Show()
    {
        GetComponent<UIPanel>().alpha = 1;
    }

    public void Hide()
    {
        GetComponent<UIPanel>().alpha = 0;
    }

    public void SetText(string Str)
    {
        if (pLb_Text == null)
            return;

        pLb_Text.text = Str;
    }
}
