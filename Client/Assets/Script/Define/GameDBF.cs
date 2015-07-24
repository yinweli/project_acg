using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class GameDBF : MonoBehaviour
{
	private DBFData m_DBF = new DBFData();

	public static GameDBF This = null;

	void Awake()
	{
		This = this;
        Add<DBFEquip>(GameDefine.szDBFEquip);
        Add<DBFFeature>(GameDefine.szDBFFeature);
        Add<DBFLanguage>(GameDefine.szDBFLanguage);
        Add<DBFMonster>(GameDefine.szDBFMonster);
		Add<DBFAtlas>(GameDefine.szDBFAtlas);
	}

    public void Add<T>(string szDBFName) where T : DBF
	{
		Debug.Log("load dbf " + szDBFName + " " + (m_DBF.Add<T>(szDBFName, "DBF/" + szDBFName) ? "success" : "failed"));
	}
	public DBF GetEquip(Argu GUID)
	{
		return m_DBF.Get(GameDefine.szDBFEquip, GUID);
	}
	public DBFItor GetEquip()
	{
		return m_DBF.Get(GameDefine.szDBFEquip);
	}
	public DBF GetFeature(Argu GUID)
	{
		return m_DBF.Get(GameDefine.szDBFFeature, GUID);
	}
	public DBFItor GetFeature()
	{
		return m_DBF.Get(GameDefine.szDBFFeature);
	}
	public string GetLanguage(Argu GUID)
	{
		DBFLanguage Data = m_DBF.Get(GameDefine.szDBFLanguage, GUID) as DBFLanguage;

		if(Data == null)
			return "";

		switch(GameData.pthis.Language)
		{
		case ENUM_Language.zhTW: return Data.zhTW;
		case ENUM_Language.enUS: return Data.enUS;
		default: return "";
		}//switch
	}
	public DBFItor GetLanguage()
	{
		return m_DBF.Get(GameDefine.szDBFLanguage);
	}
	public DBF GetMonster(Argu GUID)
	{
		return m_DBF.Get(GameDefine.szDBFMonster, GUID);
	}
	public DBFItor GetMonster()
	{
		return m_DBF.Get(GameDefine.szDBFMonster);
	}
	public DBF GetAtlas(Argu GUID)
	{
		return m_DBF.Get(GameDefine.szDBFAtlas, GUID);
	}
	public DBFItor GetAtlas()
	{
		return m_DBF.Get(GameDefine.szDBFAtlas);
	}
}
