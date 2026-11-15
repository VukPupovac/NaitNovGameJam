using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Threat : MonoBehaviour
{
    [SerializeField] private ThreatSO ThreatData;

    private GameManager Manager;
    private float closeness = 0; 
    private Vector3 randomDrift;

    void Start()
    {
        if(GameManager.Instance != null)
        {
            Manager = GameManager.Instance;
        }
        if(ThreatData.threatType != ThreatType.Moon)
        {
            randomDrift = new Vector3(UnityEngine.Random.Range(-3f,3f), UnityEngine.Random.Range(-3f,3f), 0); // Generate Random Drift
        }
        else
        {
            randomDrift = new Vector3(UnityEngine.Random.Range(4f,7f), UnityEngine.Random.Range(-1f,1f), 0); // Special Random Drift for The Moon. Always moves significantly to the right.
        }
    }

    void OnEnable()
    {
        if (Manager != null)
        {
            Manager.incomingThreats.Add(this);
        }        
    }

    void OnDisable()
    {
        if (Manager != null)
        {
            Manager.incomingThreats.Remove(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(closeness/2, closeness/2, 1); // Scale the Threat based on how close it is.

        if (closeness >= 10)
        {
            Manager.currentPlayerHealth -= ThreatData.damage;
            Destroy(gameObject);
        }
        closeness += Time.deltaTime * ThreatData.speed; 

        gameObject.transform.position = gameObject.transform.position + randomDrift/(10/ThreatData.speed)*Time.deltaTime; // Apply Random Drift

        if(ThreatData.threatType == ThreatType.Rocket) // Make the rocket spin!
        {
            gameObject.transform.eulerAngles += new Vector3(0,0,40)*Time.deltaTime;
        }
    }

    public ThreatType GetThreatType()
    {
        return ThreatData.threatType;
    }
}
