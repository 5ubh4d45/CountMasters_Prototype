using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    public int Score => score;

    public static ScoreManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void RemoveScore(int scoreToRemove)
    {
        score -= scoreToRemove;
    }
}
