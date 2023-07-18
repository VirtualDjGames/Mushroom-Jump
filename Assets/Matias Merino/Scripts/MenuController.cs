using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject menu;
    public GameStarter gameStarter;
    private float countmax = 0.2f;

    public void Play()
    {
        StartCoroutine(ShowTutorialAfterDelay());
    }

    private IEnumerator ShowTutorialAfterDelay()
    {
        menu.SetActive(false);
        yield return new WaitForSeconds(countmax);
        tutorial.SetActive(true);
        gameStarter.enabled = true;
    }
}
