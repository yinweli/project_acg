using UnityEngine;
using System.Collections;

public class Btn_Pause : MonoBehaviour 
{
    void OnClick()
    {
        Time.timeScale = 0;
    }
}
