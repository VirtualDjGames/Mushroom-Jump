using UnityEngine;
using TMPro;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab de la plataforma
    public GameObject defeatPrefab; // Prefab de la derrota
    public float spawnDelay = 2f; // Retraso entre la generación de plataformas
    public float minXPosition = -5f; // Posición mínima en el eje X para la generación de plataformas
    public float maxXPosition = 5f; // Posición máxima en el eje X para la generación de plataformas
    public float ySeparation = 15f;
    public int maxPlatforms = 50; // Cantidad máxima de plataformas generadas al mismo tiempo

    private Camera mainCamera; // Referencia a la cámara principal
    private float lastPlatformY; // Posición Y de la última plataforma generada
    private int platformCount; // Cantidad actual de plataformas generadas
    private int score = 0; // Puntaje actual
    private int maxScore = 0; // Puntaje máximo

    public TextMeshProUGUI scoreText; // Referencia al TextMeshProUGUI para mostrar el puntaje actual
    public TextMeshProUGUI maxScoreText; // Referencia al TextMeshProUGUI para mostrar el puntaje máximo

    void Start()
    {
        mainCamera = Camera.main;
        lastPlatformY = transform.position.y;
        platformCount = 0;
        maxScore = PlayerPrefs.GetInt("MaxScore", 0); // Obtener el puntaje máximo guardado

        UpdateScoreText(); // Actualizar el texto del puntaje actual
        UpdateMaxScoreText(); // Actualizar el texto del puntaje máximo

        InvokeRepeating("GeneratePlatform", 0f, spawnDelay);
    }

    void GeneratePlatform()
    {
        // Verificar si se alcanzó el límite de plataformas
        if (platformCount >= maxPlatforms)
        {
            return;
        }

        // Generar la posición aleatoria en el eje X dentro del rango establecido
        float randomX = Random.Range(minXPosition, maxXPosition);

        // Calcular la posición de generación de la plataforma
        Vector3 spawnPosition = new Vector3(randomX, lastPlatformY, 0f);

        // Generar la plataforma en la posición calculada
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Obtener el tamaño de la plataforma generada
        float platformHeight = newPlatform.GetComponent<Renderer>().bounds.size.y;

        // Actualizar la posición Y de la última plataforma generada
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
                    PlayerPrefs.SetInt("MaxScore", maxScore); // Guardar el nuevo puntaje máximo
                    UpdateMaxScoreText(); // Actualizar el texto del puntaje máximo
                }

                UpdateScoreText(); // Actualizar el texto del puntaje actual

                // Generar la zona de derrota en la posición de la plataforma eliminada
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
