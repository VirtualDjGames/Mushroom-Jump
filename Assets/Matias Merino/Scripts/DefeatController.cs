using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatController : MonoBehaviour
{
    public GameObject defeatScreen;
    public void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }

    public void Defeat()
    {
        SceneManager.LoadScene(0);
    }
}
