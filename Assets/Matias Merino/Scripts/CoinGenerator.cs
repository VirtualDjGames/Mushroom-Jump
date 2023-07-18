using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab de la moneda
    public float coinSpawnDelay = 1f; // Retraso entre la generación de monedas (ajustado a 1 segundo)
    public float coinYSeparation = 8f; // Separación vertical entre monedas
    public int maxCoins = 10; // Cantidad máxima de monedas generadas al mismo tiempo

    private float lastCoinY; // Posición Y de la última moneda generada
    private int coinCount; // Cantidad actual de monedas generadas
    private float minXPosition; // Posición mínima en el eje X para la generación de monedas
    private float maxXPosition; // Posición máxima en el eje X para la generación de monedas

    void Start()
    {
        lastCoinY = transform.position.y;
        coinCount = 0;

        // Calcular los límites de posición en el eje X para la generación de monedas
        float coinWidth = coinPrefab.GetComponent<Renderer>().bounds.size.x;
        minXPosition = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + (coinWidth / 2);
        maxXPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - (coinWidth / 2);

        InvokeRepeating("GenerateCoin", 0f, coinSpawnDelay); // Invocar la generación de monedas
    }

    void GenerateCoin()
    {
        // Verificar si se alcanzó el límite de monedas
        if (coinCount >= maxCoins)
        {
            return;
        }

        // Generar la posición aleatoria en el eje X dentro del rango establecido
        float randomX = Random.Range(minXPosition, maxXPosition);

        // Calcular la posición de generación de la moneda
        float randomYOffset = Random.Range(coinYSeparation * 0.5f, coinYSeparation);
        Vector3 coinSpawnPosition = new Vector3(randomX, lastCoinY + randomYOffset, 0f);

        // Generar la moneda en la posición calculada
        GameObject newCoin = Instantiate(coinPrefab, coinSpawnPosition, Quaternion.identity);

        // Obtener el tamaño de la moneda generada
        float coinHeight = newCoin.GetComponent<Renderer>().bounds.size.y;

        // Actualizar la posición Y de la última moneda generada (teniendo en cuenta la moneda generada)
        lastCoinY += coinHeight + coinYSeparation;

        coinCount++;
    }

    void Update()
    {
        // Obtener todas las monedas generadas en la escena
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            // Si la moneda sale de la pantalla por abajo, eliminarla y disminuir el contador de monedas
            if (coin.transform.position.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y)
            {
                Destroy(coin);
                coinCount--;
            }
        }
    }

    public void DecreaseCoinCount()
    {
        coinCount--;
    }
}
