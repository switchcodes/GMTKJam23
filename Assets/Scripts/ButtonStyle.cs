using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    public void ScaleLogoUp()
    {
        //transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        transform.localScale = new Vector3(1.6f, 1.6f, 1.0f);
    }
    
    public void ScaleLogoDown()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
    }
    
    public void OpenGMTKJam23()
    {
        Application.OpenURL("https://itch.io/jam/gmtk-2023");
    }
    
    public void OpenWrseProd()
    {
        Application.OpenURL("https://worseproductions.itch.io/");
    }
    
    public void StopCreditMusic()
    {
        GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform.Find("BgCredit").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform.Find("BgOst").GetComponent<AudioSource>().Play();
    }
}
