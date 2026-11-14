using UnityEngine;

public enum ThreatType
{
    Rocket,
    BombCluster,
    Satellite,
    Spaceship,
    Moon
}

[CreateAssetMenu(fileName = "Threat", menuName = "Scriptable Object/Threat")]
public class ThreatSO : ScriptableObject
{
    public ThreatType threatType;
    public GameObject prefab;
    public float cooldown;
}
