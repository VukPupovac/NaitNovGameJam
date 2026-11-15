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
    private bool dodged = false;
    private float dodgeCounter = 0;


    void Start()
    {
        if(GameManager.Instance != null)
        {
            Manager = GameManager.Instance;
            Manager.incomingThreats.Add(this);
        }
        if(ThreatData.threatType != ThreatType.Moon && ThreatData.threatType != ThreatType.Satellite)
        {
            randomDrift = new Vector3(UnityEngine.Random.Range(-3f,3f), UnityEngine.Random.Range(-3f,3f), 0); // Generate Random Drift
        }
        else
        {
            randomDrift = new Vector3(UnityEngine.Random.Range(7f,10f), UnityEngine.Random.Range(-1f,1f), 0); // Special Random Drift for The Moon and Sattelites. Always moves significantly to the right.
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

        if(!dodged)
        {
            if (closeness >= 10)
            {
                Manager.currentPlayerHealth -= ThreatData.damage;
                Destroy(gameObject);
            }
            closeness += Time.deltaTime * ThreatData.speed; 

            gameObject.transform.position = gameObject.transform.position + randomDrift/(10/ThreatData.speed)*Time.deltaTime; // Apply Random Drift

            if(ThreatData.threatType == ThreatType.Rocket) // Make the rocket spin!
            {
                gameObject.transform.eulerAngles += new Vector3(0,0,60)*Time.deltaTime;
            }
        }
        else
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(-10,0,0)*Time.deltaTime;
            dodgeCounter += Time.deltaTime;
            if (dodgeCounter >= 3)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Dodged()
    {
        if (ThreatData.threatType == ThreatType.Satellite || (ThreatData.threatType == ThreatType.Moon && dodgeCounter >=2))
        {
            dodged = true;
            dodgeCounter = 0;
        }
        else if (ThreatData.threatType == ThreatType.Moon)
        {
            dodgeCounter += 1;
            closeness -= 0.2f;
        }
    }

    public ThreatType GetThreatType()
    {
        return ThreatData.threatType;
    }
}
