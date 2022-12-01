using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountScore : MonoBehaviour
{
	
    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        SetCountText();
		if(gameObject.CompareTag("Answer")){
			UserData.score += 10;
		}
    }

    void SetCountText()
    {
        countText.text = "점수: " + UserData.score.ToString();
    }
}
