using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckExistance : MonoBehaviour
{
    public static bool exists;
    void Start()
    {
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
