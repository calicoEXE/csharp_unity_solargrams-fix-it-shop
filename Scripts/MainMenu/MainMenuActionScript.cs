using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuActionScript : MonoBehaviour
{

    public Slider loadingSlider;
    public GameObject loading;
    public GameObject mainmenu;

    private bool isTransitioning = false;

    public void Start()
    {
      // loadingSlider = FindObjectOfType<Slider>();
      // mainmenu = GameObject.Find("MainMenu");
      // loading = GameObject.Find("Loading");
      // loading.SetActive(false);
    }

    public void onStartGame()
    {
        SceneManager.LoadScene(1);



       // if (!isTransitioning)
       // {
       //     loading.SetActive(true);
       //     mainmenu.SetActive(false);
       //
       //     StartCoroutine(TransitionToNextSceneAsync());
       // }
    }

 // IEnumerator TransitionToNextSceneAsync()
 // {
 //     isTransitioning = true;
 //
 //     // Start a coroutine to reset Scene 1 while transitioning to the next scene
 //     StartCoroutine(ResetScene1Async());
 //
 //     // Create an AsyncOperation object to track the progress of the load for the next scene
 //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Gameloop"); // Replace "NextSceneName" with the name or index of the next scene
 //
 //     // Wait until the asynchronous scene load is complete
 //     while (!asyncLoad.isDone)
 //     {
 //         float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
 //         loadingSlider.value = progressValue;
 //
 //         yield return null;
 //     }
 //
 //     isTransitioning = false;
 // }
 //
 // IEnumerator ResetScene1Async()
 // {
 //     // Reset Scene 1 here
 //     yield return new WaitForSeconds(1.0f); // Add a delay if needed for any animations or effects
 //
 //     // Reload the current scene (Scene 1)
 //     SceneManager.LoadScene(1);
 // }

    public void onReturnMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Returning to Main Menu.");
    }

    public void onQuitEndGame()
    {
        Application.Quit();
        Debug.Log("Game Quit.");
    }

    public void GameEnd()
    {
        SceneManager.LoadScene(2);
    }

    //public Slider loadingSlider;
    //public GameObject loading;
    //public GameObject mainmenu;

    //public void Start()
    //{
    //    loadingSlider = FindObjectOfType<Slider>();
    //    mainmenu = GameObject.Find("MainMenu");
    //    loading = GameObject.Find("Loading");
    //    loading.SetActive(false);
    //}

    //public void onStartGame()
    //{
    //   // SceneManager.LoadScene(1);//gets scene 1 from the build settings can be changed

    //    loading.SetActive(true);
    //    mainmenu.SetActive(false);

    //    StartCoroutine(LoadLevelASync());

    //    Debug.Log("Loading Scene...");
    //}


    //IEnumerator LoadLevelASync()
    //{
    //    AsyncOperation loadOperation = SceneManager.LoadSceneAsync(1);
    //    Debug.Log("loadscene 1");
    //    while (!loadOperation.isDone)
    //    {
    //        float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
    //        loadingSlider.value = progressValue;
    //        yield return null;
    //    }

    //}

    //public void onReturnMenu()
    //{
    //    SceneManager.LoadScene(0);
    //    Debug.Log("Returning to Main Menu.");
    //}

    //public void onQuitEndGame()
    //{
    //    Application.Quit();
    //    Debug.Log("Game Quit.");
    //}
}
