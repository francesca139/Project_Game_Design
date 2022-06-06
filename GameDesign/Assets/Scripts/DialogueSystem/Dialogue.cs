using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Dialogue //class
{
    public string name;

    //da modellare a piacimento
    [TextArea(3, 10)] //min and max amount of lines of the area
    public string[] sentences;

}
