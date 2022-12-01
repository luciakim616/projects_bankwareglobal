using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountSaveTime : MonoBehaviour
{
    public string user;
    public Text Text_time;
    public float TimeLimit = 300;
    public string Playtime;
    public string TimeUrl = "http://sinavro.dothome.co.kr/SaveTime.php?select=show";

    public static int GetMinute(float _time)
    {
        return (int)((_time / 60) % 60);
    }

    public static int GetSecond(float _time)
    {
        return (int)(_time % 60);
    }

    void Start()
    {
        user = Account.transferID.id;
    }

    IEnumerator SaveTimeCo()
    {
        // 제한시간에서 남은시간을 빼 플레이시간을 계산
        Playtime = (300 - TimeLimit).ToString();
        Debug.Log(Playtime);

        // 디비에 저장
        WWWForm form = new WWWForm();
        form.AddField("play_time", Playtime);
        form.AddField("Input_user", user);
        WWW webRequest = new WWW(TimeUrl, form);
        yield return webRequest;

        // 저장 후 랭킹보드로 이동 : DB에서 성공했다는 메세지를 보낼경우
        if (webRequest.text.Equals("Success"))
        {
            SceneManager.LoadScene("Quiz1");
        }
    }


    void Update()
    {
        // 남은 시간이 있는 동안 1초씩 감소
        if (TimeLimit > 0)
        {
            TimeLimit -= Time.deltaTime;

            // 시간 내 클리어한 경우 : 현재는 왼쪽 마우스 클릭 시 클리어 처리
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(SaveTimeCo());

            }
        }

        // 남은 시간이 0인 경우
        else
        {
            StartCoroutine(SaveTimeCo());
        }

        // 화면에 보여주는 남은 시간
        Text_time.text = user + "님의 남은시간 " + GetMinute(TimeLimit) + " : " + GetSecond(TimeLimit);

    }
}
