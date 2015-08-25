using UnityEngine;
using System.Collections;

public class Btn_Record : MonoBehaviour 
{
    public P_Book pBook;
    public UISprite pSelect;

    void OnClick()
    {
        pSelect.gameObject.transform.localPosition = transform.localPosition;
        pSelect.width = 154;
        pSelect.height = 65;

        pBook.OpenRecord();
    }

}
