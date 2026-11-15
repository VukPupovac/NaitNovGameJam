using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public int playerMaxHealth = 100;
    public int earthHealth = 100;
    public int cowCount = 0;
    public int currentPlayerHealth;

    private Image shipHud;
    private Image earthBack;


    [SerializeField] private Image healthyHud;
    [SerializeField] private Image medHud;
    [SerializeField] private Image lowHud;

    [SerializeField] private Image healthyEarth;
    [SerializeField] private Image medEarth;
    [SerializeField] private Image lowEarth;


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

        if(currentPlayerHealth >= 60)
        {
            shipHud = healthyHud;
        }
        else if (currentPlayerHealth >= 30)
        {
            shipHud = medHud;
        }
        else
        {
            shipHud = lowHud;
        }


        if (earthHealth >= 60)
        {
            earthBack = healthyEarth;
        }
        else if (earthHealth >= 30)
        {
            earthBack = medEarth;
        }
        else
        {
            earthBack = lowEarth;
        }

        Debug.Log(incomingThreats.Count);
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
