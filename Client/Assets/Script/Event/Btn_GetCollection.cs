using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Btn_GetCollection : MonoBehaviour 
{
	public int iItemID = 0;
	
	public SpriteRenderer pSprite;
	public UILabel pLbCount;
	// ------------------------------------------------------------------
	void OnPress(bool bIsPress)
	{
		if (bIsPress)
		{
			GetComponent<BoxCollider2D>().enabled = false;
			NGUITools.PlaySound(Resources.Load("Sound/FX/GetItem") as AudioClip);
			// 變更為不受光.
			pSprite.material = Resources.Load("Sprite") as Material;
			pSprite.sortingLayerName = "Top";
			// 取得數量.
			pLbCount.text = "+" + DataGame.pthis.PickupList[iItemID].iCount;
			GetComponent<Animator>().Play("GetItem");
			// 飛行至定位.
			StartCoroutine(FlyToPos());
		}        
	}
	// ------------------------------------------------------------------
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Look")
			GetComponent<Animator>().Play("TalkShing");
	}
	// ------------------------------------------------------------------
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Look")
			GetComponent<Animator>().Play("Wait");
	}
	// ------------------------------------------------------------------
	IEnumerator FlyToPos()
	{/*
		//轉加放大
		int iCount = 1;
		while (iCount <= 9)
		{
			pSprite.transform.localScale = new Vector3(pSprite.transform.localScale.x + (0.01f * iCount), pSprite.transform.localScale.y + (0.01f * iCount), 1);
			iCount++;
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(0.8f);

		GoogleAnalytics.pthis.LogEvent("Count", "Pickup Collection", "", 0);

		DataGame.pthis.PickupList[iItemID].bPickup = true;

		List<int> Complete = Rule.SetCollection(DataGame.pthis.PickupList[iItemID].iLooks);

		// 完成圖鑑提示!
*/
		yield return new WaitForEndOfFrame();
		Destroy(gameObject);
	}
}
