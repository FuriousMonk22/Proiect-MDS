using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctionalities : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}