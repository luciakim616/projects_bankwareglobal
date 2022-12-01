using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;

	string[] idArr = new string[11];
	string[] scoreArr = new string[11];
	string[] timeArr  = new string[11];
	public string[] items;
	public bool isDownloaded = false;
	WWW www;
	WWW itemsData;

	IEnumerator Start () {
		//scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		isDownloaded = false;
		WWW itemsData = new WWW ("http://sinavro.dothome.co.kr/main.php");
		yield return itemsData;
		string itemsDataString = itemsData.text;
		//print (itemsDataString);
		items = itemsDataString.Split(';');
		//print(items);
		//print(items.Length);

		for(int i = 1; i < items.Length; i++){
			idArr[i] = GetDataValue(items[i], "ID:").ToString();
			scoreArr[i] = GetDataValue(items[i], "SCORE:").ToString();
			timeArr[i] = GetDataValue(items[i], "TIME:").ToString();
			Debug.Log("ID: " + idArr[i]);
			Debug.Log("SCORE: " + scoreArr[i]);
			Debug.Log("TIME: " + timeArr[i]);

		}
		while(this.transform.childCount > 0) {
			Transform c = this.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}
		for(int i = 0; i < idArr.Length; i++){
			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(this.transform);
			go.transform.Find ("Username").GetComponent<Text>().text = idArr[i];
			go.transform.Find ("Score").GetComponent<Text>().text = scoreArr[i];
			go.transform.Find ("Time").GetComponent<Text>().text = timeArr[i];
		}
	}
	string GetDataValue(string data, string index){

		string value = data.Substring(data.IndexOf(index) + index.Length);
		//Debug.Log("********" + value);
		if(value.Contains("|"))
			value = value.Remove(value.IndexOf("|"));

		return value;
	}

	void Update () {
	}
}
