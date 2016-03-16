using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    public SpriteRenderer[] pChangeList;
    public Color Change_Color;
	
	void Start ()
    {
        for (int i = 0; i < pChangeList.Length; i++)
            pChangeList[i].color = Change_Color;
    }
}
