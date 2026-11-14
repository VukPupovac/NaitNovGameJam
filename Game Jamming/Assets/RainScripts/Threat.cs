using Unity.VisualScripting;
using UnityEngine;

public class Threat : MonoBehaviour
{
    [SerializeField] private ThreatSO ThreatData;

    private GameManager Manager;
    private float closeness = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameManager.Instance;
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
        closeness += Time.deltaTime * (ThreatData.speed);
    }

    public ThreatType GetThreatType()
    {
        return ThreatData.threatType;
    }
}
