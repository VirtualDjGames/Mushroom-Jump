using UnityEngine;

public class PlatformGoTrough : MonoBehaviour
{
    public Transform player;
    private Collider2D platformCollider;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player.position.y > transform.position.y+2.5f)
        {
            // Activar el Collider2D de la plataforma
            platformCollider.enabled = true;
        }
        else
        {
            // Desactivar el Collider2D de la plataforma
            platformCollider.enabled = false;
        }
    }
}
