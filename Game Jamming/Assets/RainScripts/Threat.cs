using Unity.VisualScripting;
using UnityEngine;

public class Threat : MonoBehaviour
{
    [SerializeField] private ThreatSO ThreatData;

    private GameManager Manager;
    private float closeness = 0; 

    void Start()
    {
        if(GameManager.Instance != null)
        {
            Manager = GameManager.Instance;
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
        gameObject.transform.localScale = new Vector3(closeness/2, closeness/2, 1);
        if (closeness >= 10)
        {
            Manager.currentPlayerHealth -= ThreatData.damage;
            Destroy(gameObject);
        }
        closeness += Time.deltaTime * ThreatData.speed;
    }

    public ThreatType GetThreatType()
    {
        return ThreatData.threatType;
    }
}
