using UnityEngine;
using UnityEngine.SceneManagement;

//////////////////////////////////////////////  
//                                          //
//       This script shall live on:         //
//          An Empty GameObject !           //
//                                          //
//////////////////////////////////////////////

// script's purpose: Add functionality to any UI (Canvas) buttons to allow the player to use them !

// script's requirements: UI(Canvas) set to Overlay and Scale with Screen Size, adding the scenes to the Build
//                        Settings / Profile, and finally a pretty background and Buttons :3

// important to note: there are two methods to load scenes (that i know lol), one is LoadSceneAsync(#) where we
//                    just put the number or scene name to load it. The other is LoadNextScene which calculates
//                    the next scene and plays it based on their order in the Build Profile.

public class Menu : MonoBehaviour

{
    public AudioSource clicka;              // sound to play when buttons are pressed

    //////////////////////// methods ////////////////////////
    public void PlayGame()
    {
        clicka.Play();
        SceneManager.LoadSceneAsync(1);
    }
    public void DeathBeTime()
    {
        clicka.Play();
        SceneManager.LoadSceneAsync(2);
    }
    public void QuitGame()
    {
        clicka.Play();
        Application.Quit();
    }
    void LoadNextScene()
    {
        // this gets the current scene's index and adds 1 to get to the next scene :O
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // this checks if there's a next scene available in Build Settings :3
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}

