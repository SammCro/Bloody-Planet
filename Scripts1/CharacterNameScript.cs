using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterNameScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject backUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image CharImage;


    [SerializeField] private GameObject Loadingscene;
    [SerializeField] private Slider LoadingSlider;

    void Start()
    {
        nextButton.interactable = false;
    }
    
    public void GetBack()
    {
        backUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void NameChecker(string name)
    {
        if (name.Length>=3)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }

    public void LoadBeginningScene(int index)
    {
        StartCoroutine(LoadingScene(index));
    }

    public void SetTheImage()
    {

        Sprite character = Resources.Load<Sprite>(PlayerPrefs.GetString("CharacterName")) as Sprite;
        CharImage.sprite= character;
    }

    IEnumerator LoadingScene(int index)
    {
        
        AsyncOperation loading = SceneManager.LoadSceneAsync(index);
        Loadingscene.SetActive(true);

        while (!loading.isDone)
        {
            float value = Mathf.Clamp01(loading.progress / .9f);
            LoadingSlider.value = value;
            yield return null;
        }



    }

}
