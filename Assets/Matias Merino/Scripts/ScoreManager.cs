using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Puntaje actual
    public int maxScore = 0; // Puntaje m�ximo
    public int pointsPerCoin = 5; // Puntos que se suman al recolectar una moneda

    public static ScoreManager instance; // Instancia est�tica para acceder al ScoreManager desde otros scripts

    // Referencias a los TextMeshProUGUI para mostrar el puntaje actual y m�ximo
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI maxScoreTextMenu;
    public TextMeshProUGUI maxScoreTextDefeat;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0); // Obtener el puntaje m�ximo guardado

        UpdateScoreText(); // Actualizar el texto del puntaje actual
        UpdateMaxScoreText(); // Actualizar el texto del puntaje m�ximo
    }

    public void AddScore(int points)
    {
        score += points;

        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore); // Guardar el nuevo puntaje m�ximo
            UpdateMaxScoreText(); // Actualizar el texto del puntaje m�ximo
        }

        UpdateScoreText(); // Actualizar el texto del puntaje actual
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void UpdateMaxScoreText()
    {
        maxScoreText.text = "Record: " + maxScore.ToString();
        maxScoreTextMenu.text = "Record: " + maxScore.ToString();
        maxScoreTextDefeat.text = "Record: " + maxScore.ToString();
    }
}
