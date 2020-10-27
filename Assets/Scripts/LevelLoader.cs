using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public static LevelLoader Instance;

    private bool loading = false;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameSystem.Instance == null) return;

        if (GameSystem.Instance.gameWon == true && loading == false)
        {
            Debug.Log("Level Won");
            LoadNextLevel();
        }
    }

    #region Level Load Functions
    public void LoadSpecificLevel(int index)
    {
        Debug.Log("Trying to load in level " + index);
        loading = true;
        StartCoroutine(LoadLevel(index));
    }

    public void LoadNextLevel()
    {
        Debug.Log("In Load next Level");
        loading = true;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReloadCurrentLevel()
    {
        Debug.Log("Reloading current level");
        loading = true;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play the animation
        transition.SetTrigger("FadeOut");

        Debug.Log("Loading Level");

        //Wait
        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
    #endregion

    public void QuitApp()
    {
        Application.Quit();
    }

}
