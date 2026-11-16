using UnityEngine;

public class CowProjectile : MonoBehaviour
{
    private GameObject target;
    private GameObject start;

    [SerializeField] private GameEvent spawnEvent;
    [SerializeField] private GameEvent scoreEvent;

    private GameManager gameManager;

    [SerializeField] private LayerMask targetLayer; // is always player

    [SerializeField] private float scaleSpeed = 0.01f; //pretty much 1% scale down per fixed physics frame
    [SerializeField] private float maxScaleFactor = 0.1f;
    [SerializeField] private float projectileSpeed = 3f;

    private Vector3 maxScale; // cow increases in size as is going to player
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        start = GameObject.Find("Earth");
        spawnEvent.Raise();
        FindTarget();
        maxScale = transform.localScale / maxScaleFactor; // gives it about 1 and a half seconds before it's at min scale
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // make sure we are pointing at our target
            Vector2 direction = target.transform.position - transform.position;
            transform.right = direction; //  change to transform.right if our front is the right instead of the top

            if (Vector2.Distance(transform.position, target.transform.position) > 0f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, projectileSpeed * Time.deltaTime);
            }

            // scale the scale of the object down per frame whilst it's existent
            transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;

            if (transform.localScale.x >= maxScale.x || transform.localScale.y >= maxScale.y)
            {
                scoreEvent.Raise();
                gameManager.cowCount += 1;
                Destroy(gameObject);
            }
        }
    }

    public void FindTarget()
    {
        int layerNumber = (int)Mathf.Log(targetLayer.value, 2);
        //search the scene for it's correct target on awake, if no target, target earth
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude);
        foreach (GameObject obj in allObjects)
        {
            
            if (obj.layer == layerNumber)
            {
                target = obj; break; // get out of this loop as soon as possible
            }
        }
        if(target != null)
        {
            transform.position = start.transform.position;
            transform.localScale = transform.localScale * maxScaleFactor;
        }
    }
}
