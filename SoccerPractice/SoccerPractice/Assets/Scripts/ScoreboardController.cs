using TMPro;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    TextMeshPro tmpro;
    int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        tmpro = gameObject.GetComponent<TextMeshPro>();
        totalScore = 0;
    }

    public void UpdateScore(int scoreIncrement)
    {
        totalScore += scoreIncrement;
        tmpro.text = "Score: " + totalScore;
    }
}
