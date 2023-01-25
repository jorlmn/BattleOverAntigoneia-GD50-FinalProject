using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        StartCoroutine(PlayAgainRoutine());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        StartCoroutine(BackToMenuRoutine());
    }

    public void Continue()
    {
        StateManager.instance.GameStateChange(StateManager.gameStates.playState);
    }

    IEnumerator PlayAgainRoutine()
    {
        yield return new WaitForSecondsRealtime(2);
        StateManager.instance.GameStateChange(StateManager.gameStates.playState);
        SceneManager.LoadScene(1);
    }

    IEnumerator BackToMenuRoutine()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(0);
    }
}
