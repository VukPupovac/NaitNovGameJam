using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private float lifetime = 1;
    [SerializeField] private GameEvent hitEvent;
    void Start()
    {
        hitEvent.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        lifetime -= Time.deltaTime;
    }
}
