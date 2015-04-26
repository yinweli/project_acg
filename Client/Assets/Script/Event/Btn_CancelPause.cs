using UnityEngine;
using System.Collections;

public class Btn_CancelPause : MonoBehaviour
{
    void OnClick()
    {
        Time.timeScale = 1;
    }
}
