using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI textBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        textBox.text = $"Score: {gameManager.cowCount * 100}";
    }
}
