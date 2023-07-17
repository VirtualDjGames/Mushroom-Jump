using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public MushroomController mushroomController;
    public GameObject tutorial;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        tutorial.SetActive(false);
        mushroomController.enabled = true;
        Destroy(gameObject);
    }
}
