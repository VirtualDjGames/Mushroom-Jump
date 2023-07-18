using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int points = 5; // Puntos a sumar al recolectar la moneda

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Sumar los puntos al puntaje del jugador
            ScoreManager.instance.AddScore(points);

            // Destruir la moneda actual
            Destroy(gameObject);

            // Disminuir la cantidad de monedas en el contador del generador de monedas
            CoinGenerator coinGenerator = FindObjectOfType<CoinGenerator>();
            if (coinGenerator != null)
            {
                coinGenerator.DecreaseCoinCount();
            }
        }
    }
}
