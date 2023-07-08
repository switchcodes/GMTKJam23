using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStyle : MonoBehaviour
{
    public void ScaleButtonUp()
    {
        //transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);
    }
    
    public void ScaleButtonDown()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
    }
}
