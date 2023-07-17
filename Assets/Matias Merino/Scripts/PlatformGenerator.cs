using UnityEngine;
using TMPro;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab de la plataforma
    public GameObject defeatPrefab; // Prefab de la derrota
    public float spawnDelay = 2f; // Retraso entre la generaci�n de plataformas
    public float minXPosition = -5f; // Posici�n m�nima en el eje X para la generaci�n de plataformas
    public float maxXPosition = 5f; // Posici�n m�xima en el eje X para la generaci�n de plataformas
    public float ySeparation = 15f;
    public int maxPlatforms = 50; // Cantidad m�xima de plataformas generadas al mismo tiempo

    private Camera mainCamera; // Referencia a la c�mara principal
    private float lastPlatformY; // Posici�n Y de la �ltima plataforma generada
    private int platformCount; // Cantidad actual de plataformas generadas
    private int score = 0; // Puntaje actual
    private int maxScore = 0; // Puntaje m�ximo

    public TextMeshProUGUI scoreText; // Referencia al TextMeshProUGUI para mostrar el puntaje actual
    public TextMeshProUGUI maxScoreText; // Referencia al TextMeshProUGUI para mostrar el puntaje m�ximo

    void Start()
    {
        mainCamera = Camera.main;
        lastPlatformY = transform.position.y;
        platformCount = 0;
        maxScore = PlayerPrefs.GetInt("MaxScore", 0); // Obtener el puntaje m�ximo guardado

        UpdateScoreText(); // Actualizar el texto del puntaje actual
        UpdateMaxScoreText(); // Actualizar el texto del puntaje m�ximo

        InvokeRepeating("GeneratePlatform", 0f, spawnDelay);
    }

    void GeneratePlatform()
    {
        // Verificar si se alcanz� el l�mite de plataformas
        if (platformCount >= maxPlatforms)
        {
            return;
        }

        // Generar la posici�n aleatoria en el eje X dentro del rango establecido
        float randomX = Random.Range(minXPosition, maxXPosition);

        // Calcular la posici�n de generaci�n de la plataforma
        Vector3 spawnPosition = new Vector3(randomX, lastPlatformY, 0f);

        // Generar la plataforma en la posici�n calculada
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Obtener el tama�o de la plataforma generada
        float platformHeight = newPlatform.GetComponent<Renderer>().bounds.size.y;

        // Actualizar la posici�n Y de la �ltima plataforma generada
        lastPlatformY += platformHeight + ySeparation;

        platformCount++;
    }

    void Update()
    {
        // Obtener todas las plataformas generadas en la escena
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            // Si la plataforma sale de la pantalla, destruirla y generar la zona de derrota
            if (platform.transform.position.y < mainCamera.ScreenToWorldPoint(Vector3.zero).y-8)
            {
                Destroy(platform);
                platformCount--;

                // Incrementar el puntaje
                score++;
                if (score > maxScore)
                {
                    maxScore = score;
                    PlayerPrefs.SetInt("MaxScore", maxScore); // Guardar el nuevo puntaje m�ximo
                    UpdateMaxScoreText(); // Actualizar el texto del puntaje m�ximo
                }

                UpdateScoreText(); // Actualizar el texto del puntaje actual

                // Generar la zona de derrota en la posici�n de la plataforma eliminada
                Instantiate(defeatPrefab, platform.transform.position, Quaternion.identity);

                // Generar una nueva plataforma
                GeneratePlatform();
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void UpdateMaxScoreText()
    {
        maxScoreText.text = "Record: " + maxScore.ToString();
    }
}
