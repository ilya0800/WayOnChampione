using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Starting()
    {
        SceneManager.LoadScene(1);
    }
}
