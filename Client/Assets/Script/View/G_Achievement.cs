using UnityEngine;
using System.Collections;

public class G_Achievement : MonoBehaviour 
{
    public ENUM_Achievement pAchieve = ENUM_Achievement.Null;

    public UISprite pS_Icon;
    public UISprite pS_Check;

	public UILabel pLb_Name;
	public UILabel pLb_Progress;
	public UILabel pLb_Desc;
	public UILabel pLb_Effect;

	void Start()
	{
		Refresh();
	}
	public void Refresh()
	{

	}
}
