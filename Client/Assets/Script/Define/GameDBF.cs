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
	}
	void Start()
	{
		Add<DBFEquip>(GameDefine.szDBFEquip);
		Add<DBFFeature>(GameDefine.szDBFFeature);
		Add<DBFLanguage>(GameDefine.szDBFLanguage);
		Add<DBFMonster>(GameDefine.szDBFMonster);
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
	public DBF GetLanguage(Argu GUID)
	{
		return m_DBF.Get(GameDefine.szDBFLanguage, GUID);
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
}
