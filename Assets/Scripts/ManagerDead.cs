using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDead : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene(0);
        Debug.LogWarning("press");
    }

    public void Return()
    {
        SceneManager.LoadScene(1);
    }
}
