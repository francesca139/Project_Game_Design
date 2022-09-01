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
    public bool canPass;
   

   private SoundManager soundManager;
   public int song = 4;

    public GameObject pl;
    public GameObject mc;
    public GameObject fc;
    GameObject go;
    public Transform targetPosition;


    // bool load;

    /*   public void FixedUpdate()
       // void Update(Collider other)
       {
           if (Input.GetButtonDown("Jump"))
           {
               StartCoroutine(LoadScene());
           }
       }  */

    public void FixedUpdate()
    {
        if (canPass)
        {
            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine(LoadScene());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !canPass)
        {
            pl = other.gameObject;
            //  fc = GameObject.Find("FlyCollider");
            mc = GameObject.Find("MainCharacter");
            targetPosition = pl.transform;
            canPass = true;

        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPass = false;
        }
    }

    IEnumerator LoadScene()
    {
        Debug.Log("Passed");
 
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(nextSceneString, LoadSceneMode.Single);

        targetPosition.position = new Vector3(nextPortalX, nextPortalY, nextPortalZ);
        mc.transform.position = new Vector3(nextPortalX, nextPortalY + 0.7f, nextPortalZ);
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
