using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // initial(0)
    // login Scene(1)
    // Main Menu(2)
    // Game Scene(3)
    // End Scrolling(4)
    // 위와 같이 빌드 세팅 가정한 스크립트



    public void MovePreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MoveNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MoveEndScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void LoadingSceneStandard(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
