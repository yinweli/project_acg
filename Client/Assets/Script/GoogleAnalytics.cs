using UnityEngine;
using System.Collections;

public class GoogleAnalytics : MonoBehaviour {
	
	public string propertyID;
	
	public static GoogleAnalytics pthis;
	
	public string bundleID;
	public string appName;
    public string Version;
	public string Language = "en_us";
	
	private string screenRes;
	private string clientID;    
	
	void Awake()
	{
        if (pthis)
			DestroyImmediate(gameObject);
		else
		{
			DontDestroyOnLoad(gameObject);
            pthis = this;
		}
	}
	
	void Start() 
	{	
		screenRes = Screen.width + "x" + Screen.height;		
		clientID = SystemInfo.deviceUniqueIdentifier;			
	}

	public void LogScreen(string title)
	{
        
		title = WWW.EscapeURL(title);

        string url = "http://www.google-analytics.com/collect?v=1&ul=" + DataGame.pthis.Language + "&t=appview&sr=" + screenRes +
            "&an=" + WWW.EscapeURL(appName) + "&a=448166238&tid=" + propertyID + "&aid=" + bundleID + "&cid=" + WWW.EscapeURL(clientID) + "&_u=.sB&av=" + Version + "&_v=ma1b3&cd=" + title + "&qt=2500&z=185";
		WWW request = new WWW(url);
        if (request.error != null)
            Debug.Log("Send Error");
	}

    // "&ec="Event Category. Required.
    // "&ea="Event Action. Required.
    // "&el="Event label.
    // "&ev="Event value.
    public void LogEvent(string strCategory, string strAction, string strLable, int Value)
    {
        var url = "http://www.google-analytics.com/collect?v=1&tid=" + propertyID + "&an=" + WWW.EscapeURL(appName) + "&aid=" + bundleID + "&av=" + Version + "&cid=" + WWW.EscapeURL(clientID) +
            "&t=event&ec=" + strCategory + "&ea=" + WWW.EscapeURL(strAction) + "&el=" + strLable + "&ev=" + Value;
		WWW request = new WWW(url);

        if (request.error != null)
            Debug.Log("Send Error");
    }

    // 電子商務追蹤.
    /*
    v=1              // Version.
    &tid=UA-XXXX-Y   // Tracking ID / Web property / Property ID.
    &cid=555         // Anonymous Client ID.

    &t=transaction   // Transaction hit type.
    &ti=12345        // transaction ID. Required.
    &ta=westernWear  // Transaction affiliation.
    &tr=50.00        // Transaction revenue.
    &ts=32.00        // Transaction shipping.
    &tt=12.00        // Transaction tax.
    &cu=EUR          // Currency code.
     *//*
    public void LogEcommerce(Event_Category enumCategory, string strAction, Event_Lable enumLable, int Value)
    {
        var url = "http://www.google-analytics.com/collect?v=1&tid=" + propertyID + "&an=" + WWW.EscapeURL(appName) + "&aid=" + bundleID + "&av=" + Version + "&cid=" + WWW.EscapeURL(clientID) +
            "&t=transaction&ti
        Debug.Log(url);
        WWW request = new WWW(url);
    }*/

}