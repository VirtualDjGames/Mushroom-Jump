using UnityEngine;

public class DefeatZone : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            Debug.Log("¡Has perdido!");
            // Aquí puedes agregar el código para la acción que deseas realizar cuando el jugador colisiona con la zona de derrota
        }
    }
}
