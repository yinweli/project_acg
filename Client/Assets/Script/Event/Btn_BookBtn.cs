using UnityEngine;
using System.Collections;

public class Btn_BookBtn : MonoBehaviour 
{
    public ENUM_BookBtn pType = ENUM_BookBtn.Record; 
    public P_Book pBook;
    public UIButton pBtn;
    public UISprite pMySprite;
    
    void OnClick()
    {
        pBook.ResetBtn();        
        pBook.SetSelect(gameObject, pMySprite);
        pBook.CreatePage(pType);

        pBtn.isEnabled = false;
    }
}
