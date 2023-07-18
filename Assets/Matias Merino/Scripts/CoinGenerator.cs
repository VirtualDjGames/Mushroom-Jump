using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab de la moneda
    public float coinSpawnDelay = 1f; // Retraso entre la generaci�n de monedas (ajustado a 1 segundo)
    public float coinYSeparation = 8f; // Separaci�n vertical entre monedas
    public int maxCoins = 10; // Cantidad m�xima de monedas generadas al mismo tiempo

    private float lastCoinY; // Posici�n Y de la �ltima moneda generada
    private int coinCount; // Cantidad actual de monedas generadas
    private float minXPosition; // Posici�n m�nima en el eje X para la generaci�n de monedas
    private float maxXPosition; // Posici�n m�xima en el eje X para la generaci�n de monedas

    void Start()
    {
        lastCoinY = transform.position.y;
        coinCount = 0;

        // Calcular los l�mites de posici�n en el eje X para la generaci�n de monedas
        float coinWidth = coinPrefab.GetComponent<Renderer>().bounds.size.x;
        minXPosition = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + (coinWidth / 2);
        maxXPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - (coinWidth / 2);

        InvokeRepeating("GenerateCoin", 0f, coinSpawnDelay); // Invocar la generaci�n de monedas
    }

    void GenerateCoin()
    {
        // Verificar si se alcanz� el l�mite de monedas
        if (coinCount >= maxCoins)
        {
            return;
        }

        // Generar la posici�n aleatoria en el eje X dentro del rango establecido
        float randomX = Random.Range(minXPosition, maxXPosition);

        // Calcular la posici�n de generaci�n de la moneda
        float randomYOffset = Random.Range(coinYSeparation * 0.5f, coinYSeparation);
        Vector3 coinSpawnPosition = new Vector3(randomX, lastCoinY + randomYOffset, 0f);

        // Generar la moneda en la posici�n calculada
        GameObject newCoin = Instantiate(coinPrefab, coinSpawnPosition, Quaternion.identity);

        // Obtener el tama�o de la moneda generada
        float coinHeight = newCoin.GetComponent<Renderer>().bounds.size.y;

        // Actualizar la posici�n Y de la �ltima moneda generada (teniendo en cuenta la moneda generada)
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
