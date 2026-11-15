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

    [SerializeField] private SpriteRenderer shipHud;
    [SerializeField] private SpriteRenderer earthBack;


    [SerializeField] private Sprite healthyHud;
    [SerializeField] private Sprite medHud;
    [SerializeField] private Sprite lowHud;

    [SerializeField] private Sprite healthyEarth;
    [SerializeField] private Sprite medEarth;
    [SerializeField] private Sprite lowEarth;


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
            shipHud.sprite = healthyHud;
        }
        else if (currentPlayerHealth >= 30)
        {
            shipHud.sprite = medHud;
        }
        else
        {
            shipHud.sprite = lowHud;
        }


        if (earthHealth >= 60)
        {
            earthBack.sprite = healthyEarth;
        }
        else if (earthHealth >= 30)
        {
            earthBack.sprite = medEarth;
        }
        else
        {
            earthBack.sprite = lowEarth;
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
