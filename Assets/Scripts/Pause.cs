using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool PressButton = false;

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPause()
    {
        if (!PressButton)
        {
            Time.timeScale = 0;
            PressButton = true;
        }
        else if (PressButton)
        {
            Time.timeScale = 1;
            PressButton = false;
        }
    }

    public void OnPlay()
    {
       
    }
}
