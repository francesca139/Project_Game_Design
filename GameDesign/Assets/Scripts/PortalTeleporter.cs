using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



//this script must be attached on the player
//on on what have to pass between scenes
public class PortalTeleporter : MonoBehaviour
{
    public string nextSceneString;
    public float nextPortalX;
    public float nextPortalY;
    public float nextPortalZ;
   

   private SoundManager soundManager;
   public int song = 4;

    GameObject go;
    Transform targetPosition;


    // bool load;

    private void OnTriggerEnter(Collider other)
    {
        
        go = other.gameObject;

        if (other.gameObject.tag == "Player")
        {
            targetPosition = other.transform;
            StartCoroutine(LoadScene()); 
        }


    }


    IEnumerator LoadScene()
    {
        Debug.Log("Passed");
 
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(nextSceneString, LoadSceneMode.Single);

        targetPosition.position = new Vector3(nextPortalX, nextPortalY, nextPortalZ);

        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync(currentScene);
        int index = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("You are currently in scene " + index);


        if(song == 4){
            song = 5;
        }else{
            song = 4;
        }
        soundManager = FindObjectOfType<SoundManager>();
	    soundManager.SeleccionAudio(song,0.5f);


    }
}
