using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject UIcanvas;

    [SerializeField] private GameObject howtoPlay;
    [SerializeField] private GameObject Settings;

    [SerializeField] private Dropdown Dropdown;
    [SerializeField] private Slider sliderVolume;


    void Start()
    {

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        sliderVolume.value = PlayerPrefs.GetFloat("Volume");
        Dropdown.value = PlayerPrefs.GetInt("Quality");
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            UIcanvas.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        UIcanvas.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ShowHow()
    {
        howtoPlay.SetActive(true);
        Settings.SetActive(false);
    }
    public void ShowQuality()
    {
        howtoPlay.SetActive(false);
        Settings.SetActive(true);
    }






    public void QualitySetting(int index)
    {
        PlayerPrefs.SetInt("Quality", index);
        QualitySettings.SetQualityLevel(index, true);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        AudioListener.volume = value;
    }

}
