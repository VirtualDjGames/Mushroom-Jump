using UnityEngine;

public class DefeatZone : MonoBehaviour
{
    public Transform player;
    public GameObject playerGO;

    private DefeatController defeatScreen;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        defeatScreen = FindObjectOfType<DefeatController>();
    }

    void Update()
    {
        if (player.position.y > transform.position.y + 50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            defeatScreen.ShowDefeatScreen();
            Debug.Log("¡Has perdido!");
            playerGO.SetActive(false);
            // Aquí puedes agregar el código para la acción que deseas realizar cuando el jugador colisiona con la zona de derrota
        }
    }
}
