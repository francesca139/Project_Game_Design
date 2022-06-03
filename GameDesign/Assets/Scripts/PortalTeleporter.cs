using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



//this script must be attached on the player
//on on what have to pass between scenes
public class PortalTeleporter : MonoBehaviour
{
    public string nextSceneString;
    GameObject go;


    // bool load;

    private void OnTriggerEnter(Collider other)
    {
        
        go = other.gameObject;

        if(other.gameObject.tag == "Player")
          StartCoroutine(LoadScene());


    }


    IEnumerator LoadScene()
    {
        Debug.Log("Passed");
 
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(nextSceneString, LoadSceneMode.Single);

        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync(currentScene);
        int index = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("You are currently in scene " + index);


    }
}
