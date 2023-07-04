using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab de la plataforma
    public float spawnDelay = 2f; // Retraso entre la generaci�n de plataformas
    public float minXPosition = -5f; // Posici�n m�nima en el eje X para la generaci�n de plataformas
    public float maxXPosition = 5f; // Posici�n m�xima en el eje X para la generaci�n de plataformas
    public float ySeparation = 15f;

    private Camera mainCamera; // Referencia a la c�mara principal
    private float lastPlatformY; // Posici�n Y de la �ltima plataforma generada

    void Start()
    {
        mainCamera = Camera.main;
        lastPlatformY = transform.position.y;
        InvokeRepeating("GeneratePlatform", 0f, spawnDelay);
    }

    void GeneratePlatform()
    {
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
    }

    void Update()
    {
        // Obtener todas las plataformas generadas en la escena
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            // Si la plataforma sale de la pantalla, destruirla
            if (platform.transform.position.y < mainCamera.ScreenToWorldPoint(Vector3.zero).y)
            {
                Destroy(platform);
            }
        }
    }
}