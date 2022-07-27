using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPanel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject objective;

    void OnEnable()
    {
        Invoke("SetActiveFunc",7f);
    }

    void OnDisable()
    {
      objective.SetActive(true);
    }

    public void SetEnable()
    {
        gameObject.SetActive(true);
    }

    void SetActiveFunc()
    {
        gameObject.SetActive(false);
    }
}
