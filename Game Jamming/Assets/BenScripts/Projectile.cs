using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;

    [SerializeField] private GameEvent fireEvent;
    [SerializeField] private GameEvent hitEvent;

    private GameManager gameManager;
    [SerializeField] private LayerMask targetLayer;
    private GameObject targetPlanet;

    [SerializeField] private float scaleSpeed = 0.01f; //scale down speed per frame
    [SerializeField] private float minScaleFactor = 0.1f;

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
    void FixedUpdate()
    {
        if (target != null)
        {
            // make sure we are pointing at our target
            Vector2 direction = target.transform.position - transform.position;
            transform.right = direction; //  change to transform.right if our front is the right instead of the top

            // scale the scale of the object down per frame whilst it's existent
            transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.fixedDeltaTime;

            if (transform.localScale.x <= minScale.x  && transform.localScale.y <= minScale.y)
            {
                hitEvent.Raise();
                if (target == targetPlanet) // meaning our target is the earth
                {
                    //target.GetComponent<EarthController>().DamageEarth(); or some method of damaging the earth
                }
                else if(target != targetPlanet)
                { // destroy associated target that this projectile can affect
                    //already determined by the layer mask used in find target
                    Destroy(target);
                }
                Destroy(gameObject);
            }
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
                target = threat.gameObject;
            }
        }

        if (target == null && targetPlanet != null)
        {
            target = targetPlanet;
        }

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        Debug.Log(target.name);
    }
}
