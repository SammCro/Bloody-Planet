using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHeroScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject nameCanvas;



    public void CharacterName(string nameOfCharacter)
    {
        PlayerPrefs.SetString("CharacterName",nameOfCharacter);
    }

    public void ChangeToName()
    {
        nameCanvas.SetActive(true);
        gameObject.SetActive(false);
        
    }

}
