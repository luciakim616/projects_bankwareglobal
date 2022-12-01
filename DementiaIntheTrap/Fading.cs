// Fading

using UnityEngine;
using UnityEngine.SceneManagement;

// FadeToLevel("MainScene")

public class Fading : MonoBehaviour
{
    public Animator animator;
    private string sceneToLoad;

    // 로그인 성공 시 올라가는 플래그
    public static class loginFlag
    {
        public static bool flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Account 스크립트에서 로그인 성공 시 정적 변수 flag를 올림 -> 페이드아웃 실행
        if (loginFlag.flag == true)
        {
            FadeToLevel("MainScene");
        }
    }

    // 페이드아웃 트리거 작동
    public void FadeToLevel(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("Trig_fadeout");
    }

    // 페이드아웃 효과 끝날 때 MainScene 로드
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}