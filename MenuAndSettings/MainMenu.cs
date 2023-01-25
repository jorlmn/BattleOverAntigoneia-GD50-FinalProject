using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Animator mainMenu;

    void Start()
    {
        mainMenu.SetTrigger("OpenMenu");
    }
    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator PlayRoutine()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(1);
    }
}
