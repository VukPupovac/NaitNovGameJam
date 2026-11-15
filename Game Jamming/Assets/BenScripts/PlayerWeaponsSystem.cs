 using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Transform firePoint;



    [Header("Weapon Prefabs")]
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject UFOPrefab;
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private GameObject CowPrefab;
    [SerializeField] private GameEvent dodgePerformed;
    [SerializeField] private GameEvent repairPerformed;

    [Header("Button Image Changes")]
    [SerializeField] private Sprite laserImg;
    [SerializeField] private Sprite repairImg;
    [SerializeField] private Sprite dodgeImg;
    [SerializeField] private Sprite funImg;
    [SerializeField] private Sprite cowImg;
    [SerializeField] private Sprite asteroidImg;
    [SerializeField] private Sprite UFOImg;

    [SerializeField] private Sprite newLaser;
    [SerializeField] private Sprite newRepair;
    [SerializeField] private Sprite newDodge;
    [SerializeField] private Sprite newFun;
    [SerializeField] private Sprite newCow;
    [SerializeField] private Sprite newAsteroid;
    [SerializeField] private Sprite newUFO;

    [SerializeField] private SpriteRenderer laserButton;
    [SerializeField] private SpriteRenderer repairButton;
    [SerializeField] private SpriteRenderer dodgeButton;
    [SerializeField] private SpriteRenderer funButton;
    [SerializeField] private SpriteRenderer cowButton;
    [SerializeField] private SpriteRenderer asteroidButton;
    [SerializeField] private SpriteRenderer UFOButton;

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

            laserButton.sprite = newLaser;
            repairButton.sprite = newRepair;
            funButton.sprite = newFun;
            cowButton.sprite = newCow;
            asteroidButton.sprite = newAsteroid;
            dodgeButton.sprite = newDodge;
            UFOButton.sprite = newUFO;
        }
        else
        {
            laserButton.sprite = laserImg;
            repairButton.sprite = repairImg;
            funButton.sprite = funImg;
            cowButton.sprite = cowImg;
            asteroidButton.sprite = asteroidImg;
            dodgeButton.sprite = dodgeImg;
            UFOButton.sprite = UFOImg;
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
        Instantiate(LaserPrefab, firePoint);
    }

    public void UFO()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }
        currentActTimer = cooldownTimer;
        Instantiate(UFOPrefab, firePoint);
    }

    public void Asteroid()
    {
        if (currentActTimer > 0f)
        {
            return; // on cooldown since last action, cannot act
        }
        currentActTimer = cooldownTimer;
        Instantiate(AsteroidPrefab, firePoint);
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
        dodgePerformed.Raise();
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
        repairPerformed.Raise();
        
    }

}

