using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameSettings : MonoBehaviour
{
    public static GameSettings instance = null;

    void Awake()
    {
        if (GameSettings.instance == null)
        {
            instance = this;
        }
        else if (GameSettings.instance != this)
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] Slider difficultySlider;
    [SerializeField] TextMeshProUGUI difficultySliderText;
    public static string difficulty = "medium";

    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] TextMeshProUGUI mouseSensitivitySliderText;
    public static int mouseSensitivity = 100;

    [SerializeField] TMP_Dropdown screenDropDown;
    public static bool screenFullscreen = true;

    [SerializeField] TMP_Dropdown screenResolutionDropDown;
    public static string screenResolution;

    void Start()
    {
        difficultySlider.onValueChanged.AddListener(delegate { DifficultySliderCheck(); });

        mouseSensitivitySlider.onValueChanged.AddListener(delegate { MouseSensitivitySliderCheck(); });
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

    private void MouseSensitivitySliderCheck()
    {
        mouseSensitivitySliderText.text = mouseSensitivitySlider.value.ToString();
    }

    public void PortrayGameSettings()
    {
        switch (difficulty)
        {
            case "easy":
                difficultySliderText.text = "EASY";
                difficultySlider.value = 0;
                break;
            case "medium":
                difficultySliderText.text = "MEDIUM";
                difficultySlider.value = 1;
                break;
            case "hard":
                difficultySliderText.text = "HARD";
                difficultySlider.value = 2;
                break;
            case "very hard":
                difficultySliderText.text = "VERY HARD";
                difficultySlider.value = 3;
                break;
        }

        mouseSensitivitySliderText.text = mouseSensitivity.ToString();
        mouseSensitivitySlider.value = mouseSensitivity;

        screenDropDown.value = Screen.fullScreen == true ? 0 : 1;

        screenResolutionDropDown.value = GetResolutionIndex(Screen.width + "x" + Screen.height);
    }

    public void ResetSettings()
    {
        difficulty = "medium";

        Screen.fullScreen = true;

        PortrayGameSettings();
    }

    public void ApplySettings()
    {
        switch ((int)difficultySlider.value)
        {
            case 0:
                difficulty = "easy";
                break;
            case 1:
                difficulty = "medium";
                break;
            case 2:
                difficulty = "hard";
                break;
            case 3:
                difficulty = "very hard";
                break;
        }

        mouseSensitivity = (int)mouseSensitivitySlider.value;

        string[] resolution = screenResolutionDropDown.options[screenResolutionDropDown.value].text.Split('x', 2);

        Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), screenDropDown.value == 0 ? FullScreenMode.FullScreenWindow: FullScreenMode.Windowed);
    }

    int GetResolutionIndex(string resolution)
    {
        int index = -1;
        switch (resolution)
        {
            case "640x360":
                index = 0;
                break;
            case "800x600":
                index = 1;
                break;
            case "1024x768":
                index = 2;
                break;
            case "1280x720":
                index = 3;
                break;
            case "1280x800":
                index = 4;
                break;
            case "1280x1024":
                index = 5;
                break;
            case "1360x768":
                index = 6;
                break;
            case "1366x768":
                index = 7;
                break;
            case "1440x900":
                index = 8;
                break;
            case "1536x864":
                index = 9;
                break;
            case "1600x900":
                index = 10;
                break;
            case "1680x1050":
                index = 11;
                break;
            case "1920x1080":
                index = 12;
                break;
            case "1920x1200":
                index = 13;
                break;
            case "2048x1152":
                index = 14;
                break;
            case "2048x1536":
                index = 14;
                break;
            case "2560x1080":
                index = 15;
                break;
            case "2560x1440":
                index = 16;
                break;
            case "3440x1440":
                index = 17;
                break;
            case "3840x2160":
                index = 18;
                break;
        }

        return index;
    }
}
