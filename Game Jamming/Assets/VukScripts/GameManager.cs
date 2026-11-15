using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public int playerMaxHealth = 100;
    public int earthHealth = 100;
    public int cowCount = 0;
    public int currentPlayerHealth;

    [SerializeField] private UIDocument uIDocument;

    public List<Threat> incomingThreats = new List<Threat>();
    
    private void Awake()
    {
        if ( Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        

        currentPlayerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentPlayerHealth <= 0)
        {
            Lose();
        }

        if (earthHealth <= 0)
        {
            Win();
        }

        if(currentPlayerHealth < 60)
        {
            
        }
    }

    private void Lose()
    {
        //Update Lose scene UI to display cowCount
        SceneManager.LoadScene("LoseCutscene");
        cowCount = 0;
    }

    private void Win()
    {
        //display cowCountUI when cutscene is complete
        SceneManager.LoadScene("WinCutscene");
    }
}
