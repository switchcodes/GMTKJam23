using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCredits : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform.Find("BgOst").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("AudioManagerLoaded").transform.Find("BgCredit").GetComponent<AudioSource>().Play();
    }
}
