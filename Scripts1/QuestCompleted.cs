using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleted : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject objective;

    void OnEnable()
    {
        Invoke("SetActiveFunc", 7f);
    }

    void OnDisable()
    {
        objective.SetActive(true);
    }



    void SetActiveFunc()
    {
        gameObject.SetActive(false);
    }
}
