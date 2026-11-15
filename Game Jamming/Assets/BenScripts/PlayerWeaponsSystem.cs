using UnityEngine;

public class PlayerWeaponsSystem : MonoBehaviour
{
    [Header("Universal")]
    private float currentActTimer;
    [SerializeField] private float cooldownTimer = 2f;// so the player can't spam click options
    private GameManager gameManager;
    [SerializeField] private float dodgeTimer = 1f;
    private float currentDodgeTimer;
    public bool isDodging { get => currentDodgeTimer > 0f;} // for outside refernces

    [SerializeField] private int repairAmount = 15;



    [Header("Weapon Prefabs")]
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject UFOPrefab;
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private GameObject CowPrefab;


    private void Awake()
    {
        currentActTimer = 0f;
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        if (currentActTimer > 0f)
        {
            Debug.Log("Can't Act");
            currentActTimer -= Time.deltaTime;
        }

        if (currentDodgeTimer > 0f)
        {
            Debug.Log("Dodging!");
            currentDodgeTimer -= Time.deltaTime;
        }
    }

    public void Laser()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }
        currentActTimer = cooldownTimer;
        Instantiate(LaserPrefab);
    }

    public void UFO()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }
        currentActTimer = cooldownTimer;
        Instantiate(UFOPrefab);
    }

    public void Asteroid()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }
        currentActTimer = cooldownTimer;
        Instantiate(AsteroidPrefab);
    }

    public void Cow()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }
        currentActTimer = cooldownTimer;
        Instantiate(CowPrefab);
    }

    public void Dodge()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }

        currentDodgeTimer = dodgeTimer;
        currentActTimer = cooldownTimer;
    }

    public void Repair()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }

        if (gameManager.currentPlayerHealth == gameManager.playerMaxHealth)
        {
            currentActTimer = cooldownTimer;
            return; // Player already at max Health
        }
        gameManager.currentPlayerHealth += repairAmount;
        if (gameManager.currentPlayerHealth > gameManager.playerMaxHealth)
        {
            gameManager.currentPlayerHealth = gameManager.playerMaxHealth;
        }
        currentActTimer = cooldownTimer;
        
    }

}

