using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestsCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    private int countOfMonsters;
    private int deathOfMonsters;
    private string textString;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    [SerializeField] private GameObject completedCanvas;

    void Awake()
    {
        deathOfMonsters = 0;
        countOfMonsters = 18;
    }



    void OnEnable()
    {
        

        textString = deathOfMonsters + "/" + countOfMonsters;

        textMesh.text = textString;
    }

    // Update is called once per frame


    public void DeathMonster()
    {
        deathOfMonsters++;
        textString = deathOfMonsters + "/" + countOfMonsters;
        textMesh.text = textString;
        if (countOfMonsters == deathOfMonsters)
        {
            completedCanvas.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}
