using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Dropdown Dropdown;

    void Start()
    {
        PlayerPrefs.SetString("CharacterName","male1");

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"),true);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        sliderVolume.value = PlayerPrefs.GetFloat("Volume");
        Dropdown.value = PlayerPrefs.GetInt("Quality");

    }

    

    public void QualitySetting(int index)
    {
        PlayerPrefs.SetInt("Quality",index);
        QualitySettings.SetQualityLevel(index,true);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume",value);
        AudioListener.volume = value;
    }
}
