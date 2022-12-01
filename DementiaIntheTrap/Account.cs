// Account

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Account : MonoBehaviour
{
    public InputField IDInputField;
    public GameObject VirtualKey_Create;
    public string LoginUrl = "http://sinavro.dothome.co.kr/Login.php?select=show";
    public string CreateAccountUrl = "http://sinavro.dothome.co.kr/CreateAccount.php?select=show";
    public string ID = "";
    public Text noticemsg;

    public static class transferID
    {
        public static string id = "";
    }

    public void LoginBtn()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        // DB 연결준비 : 아이디 문자열을 폼에 추가
        WWWForm form = new WWWForm();
        form.AddField("Input_user", IDInputField.text);

        // URL 연결 : webRequest에 성공메세지가 올 때까지 대기
        WWW webRequest = new WWW(LoginUrl, form);
        yield return webRequest;
        Debug.Log(webRequest.text);

        // 로그인 여부 체크 : 웹에서 Success를 보내면 아이디와 함께 메인씬 로드
        if (webRequest.text.Equals("Success"))
        {
            transferID.id = IDInputField.text;
            Fading.loginFlag.flag = true; // 페이드인아웃 플래그를 올림
            Debug.Log(transferID.id);
        }

        // 아이디가 없는 경우
        else if (webRequest.text.Equals("NO"))
        {
            noticemsg.text = "해당 아이디가 존재하지 않습니다.";
        }

    }

    public void SignUpBtn()
    {
        VirtualKey_Create.SetActive(true);
    }

    public void CreateBtn()
    {
        if (IDInputField.text == null)
        {
            noticemsg.text = "숫자 4자리를 입력해주세요.";
        }

        else
            StartCoroutine(CreateCo());
    }

    IEnumerator CreateCo()
    {
        Debug.Log(IDInputField.text);
        WWWForm form = new WWWForm();
        form.AddField("Input_user", IDInputField.text);

        WWW webRequest = new WWW(CreateAccountUrl, form);
        yield return webRequest;

        Debug.Log(webRequest.text);

        if (webRequest.text == "Success")
        {
            noticemsg.text = "계정만들기 성공! 로그인해주세요.";
            VirtualKey_Create.SetActive(false);
        }

        else if (webRequest.text == "Exist")
        {
            noticemsg.text = "이미 존재하는 아이디입니다.";
            IDInputField.text = "";
        }

        else if (webRequest.text == "Retry")
        {
            noticemsg.text = "다시 시도해주세요.";
            IDInputField.text = "";
        }
    }


}