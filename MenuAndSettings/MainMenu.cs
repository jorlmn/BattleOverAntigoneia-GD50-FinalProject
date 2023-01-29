using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Animator mainMenu;

    [SerializeField] Slider difficultySlider;
    [SerializeField] TextMeshProUGUI difficultySliderText;

    void Start()
    {
        mainMenu.SetTrigger("OpenMenu");

        difficultySlider.onValueChanged.AddListener(delegate { DifficultySliderCheck(); });
    }

    private void DifficultySliderCheck()
    {
        switch ((int)difficultySlider.value)
        {
            case 0:
                difficultySliderText.text = "EASY";
                break;
            case 1:
                difficultySliderText.text = "MEDIUM";
                break;
            case 2:
                difficultySliderText.text = "HARD";
                break;
            case 3:
                difficultySliderText.text = "VERY HARD";
                break;
        }
    }

    public void ShipSelection(int index)
    {
        MatchSettings.shipChosenIndex = index;
    }
    public void Play()
    {
        switch ((int)difficultySlider.value)
        {
            case 0:
                GameSettings.difficultyModifier = 0.5f;
                break;
            case 1:
                GameSettings.difficultyModifier = 1f;
                break;
            case 2:
                GameSettings.difficultyModifier = 1.5f;
                break;
            case 3:
                GameSettings.difficultyModifier = 2f;
                break;
        }
        
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
