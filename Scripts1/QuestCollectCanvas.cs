using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCollectCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject SectorCompleted;
    private int materialsCollected=0;
    private int materialsRequired;


    void Start()
    {
        materialsRequired = GameObject.FindGameObjectsWithTag("Material").Length;

        textMesh.text = "Materials Collected "+materialsCollected + "/" + materialsRequired;
    }

    // Update is called once per frame
    public void GetCollect()
    {
        materialsCollected++;

        textMesh.text = "Materials Collected " + materialsCollected + "/" + materialsRequired;

        if (materialsCollected==materialsRequired)
        {
            SectorCompleted.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
