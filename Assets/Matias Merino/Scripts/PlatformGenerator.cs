using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab de la plataforma
    public GameObject defeatPrefab; // Prefab de la derrota
    public float spawnDelay = 2f; // Retraso entre la generación de plataformas
    public float minXPosition = -5f; // Posición mínima en el eje X para la generación de plataformas
    public float maxXPosition = 5f; // Posición máxima en el eje X para la generación de plataformas
    public float ySeparation = 15f;

    private Camera mainCamera; // Referencia a la cámara principal
    private float lastPlatformY; // Posición Y de la última plataforma generada

    void Start()
    {
        mainCamera = Camera.main;
        lastPlatformY = transform.position.y;
        InvokeRepeating("GeneratePlatform", 0f, spawnDelay);
    }

    void GeneratePlatform()
    {
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
    }

    void Update()
    {
        // Obtener todas las plataformas generadas en la escena
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            // Si la plataforma sale de la pantalla, destruirla y generar la zona de derrota
            if (platform.transform.position.y < mainCamera.ScreenToWorldPoint(Vector3.zero).y)
            {
                Destroy(platform);

                // Generar la zona de derrota en la posición de la plataforma eliminada
                Instantiate(defeatPrefab, platform.transform.position, Quaternion.identity);
            }
        }
    }
}
