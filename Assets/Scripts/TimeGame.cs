using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimeGame : MonoBehaviour
{
    public GameObject panel;
   



    public float timer;
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // change 1 second
        }
        if (timer < 0)
        {
            panel.SetActive(false);
        }
    }



}
