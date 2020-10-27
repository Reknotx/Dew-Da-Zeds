using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [Header("Place the tutorial boxes in the list in order of appearance.")]
    public List<GameObject> tutorialBoxes;

    private int _counter = 0;
    private GameObject _activeBox;

    private bool _tutorialEnded = false;

    private void Start()
    {
        GameSystem.Instance.playButton.interactable = false;
        GameSystem.Instance.pauseButton.interactable = false;
        GameSystem.Instance.normalSpeedButton.interactable = false;
        GameSystem.Instance.fastSpeedButton.interactable = false;

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
        if (_tutorialEnded) return;

        ///Run this block if we are in the unity editor.
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceTutorial();
        }
#else
        ///Run this block if we are testing on our phones.
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            AdvanceTutorial();
        }
#endif


    }

    private void AdvanceTutorial()
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
            _tutorialEnded = true;
            GameSystem.Instance.playButton.interactable = true;
            GameSystem.Instance.pauseButton.interactable = true;
            GameSystem.Instance.normalSpeedButton.interactable = true;
            GameSystem.Instance.fastSpeedButton.interactable = true;
            _activeBox.SetActive(false);
        }
    }
}
