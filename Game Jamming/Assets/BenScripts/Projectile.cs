using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private Threat targetThreat;

    [SerializeField] private GameEvent fireEvent;
    [SerializeField] private GameEvent hitEvent;

    private GameManager gameManager;
    [SerializeField] private LayerMask targetLayer;
    private GameObject targetPlanet;

    [SerializeField] private float scaleSpeed = 0.01f; //scale down speed per frame
    [SerializeField] private float minScaleFactor = 0.1f;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float projectileSpeed = 3f;

    private Vector3 minScale;
    void Awake()
    {
        targetPlanet = GameObject.Find("Earth");
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        fireEvent.Raise();
        FindTarget();
        minScale = transform.localScale * minScaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // make sure we are pointing at our target
            Vector2 direction = target.transform.position - transform.position;
            transform.up = direction; //  change to transform.right if our front is the right instead of the top

            if (Vector2.Distance(transform.position, target.transform.position) > 0f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, projectileSpeed * Time.deltaTime);
            }
        }

        // scale the scale of the object down per frame whilst it's existent
        transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.fixedDeltaTime;

        if (transform.localScale.x <= minScale.x  || transform.localScale.y <= minScale.y)
        {
            hitEvent.Raise();
            if (target == targetPlanet) // meaning our target is the earth
            {
                gameManager.earthHealth -= damageAmount;
            }
            else if(target != targetPlanet && target != null)
            { // destroy associated target that this projectile can affect
             //already determined by the layer mask used in find target
                gameManager.incomingThreats.Remove(targetThreat);
                Destroy(target);
            }
            Destroy(gameObject);
        }
        
    }

    public void FindTarget()
    {
        int layerNumber = (int)Mathf.Log(targetLayer.value, 2);


        //search the scene for it's correct target on awake, if no target, target earth

        foreach (Threat threat in gameManager.incomingThreats)
        {
            if (threat.gameObject.layer == layerNumber)
            {
                targetThreat = threat;
                target = threat.gameObject;
                break;
            }
        }

        if (target == null && targetPlanet != null)
        {
            target = targetPlanet;
        }

    }
}
