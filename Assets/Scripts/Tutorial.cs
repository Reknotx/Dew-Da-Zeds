using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [Header("Place the tutorial boxes in the list in order of appearance.")]
    public List<GameObject> tutorialBoxes;

    private int _counter = 0;
    private GameObject _activeBox;

    private void Start()
    {
        if (tutorialBoxes.Count == 0)
        {
            Debug.LogError("Tutorial List empty");
        }
        foreach (GameObject Object in tutorialBoxes)
        {
            Object.SetActive(false);
        }

        tutorialBoxes[0].SetActive(true);
        _activeBox = tutorialBoxes[0];
        _counter++;
    }


    private void Update()
    {

        ///Run this block if we are in the unity editor.
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (_counter < tutorialBoxes.Count)
            {
                _activeBox.SetActive(false);
                _activeBox = tutorialBoxes[_counter];
                _activeBox.SetActive(true);
                _counter++;
            }
            else
            {
                LevelLoader.Instance.LoadSpecificLevel(0);
            }
        }
#else
        ///Run this block if we are testing on our phones.
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (_counter < tutorialBoxes.Count)
            {
                _activeBox.SetActive(false);
                _activeBox = tutorialBoxes[_counter];
                _activeBox.SetActive(true);
                _counter++;
            }
            else
            {
                LevelLoader.Instance.LoadSpecificLevel(0);
            }
        }
#endif


    }


}
