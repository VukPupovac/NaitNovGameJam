using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private GameObject backupTarget;

    [SerializeField] private GameEvent fireEvent;
    [SerializeField] private GameEvent hitEvent;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask earthLayerMask;

    [SerializeField] private float scaleSpeed = 0.01f; //pretty much 1% scale down per fixed physics frame
    [SerializeField] private float minScaleFactor = 0.1f;

    private Vector3 minScale;
    void Awake()
    {
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
                // target.DamageTarget();
                Destroy(gameObject);
            }
        }
    }

    public void FindTarget()
    {
        int layerNumber = (int)Mathf.Log(targetLayer.value, 2);
        int earthLayerNumber = (int)Mathf.Log(earthLayerMask.value, 2);
        //search the scene for it's correct target on awake, if no target, target earth
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Exclude);
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layerNumber)
            {
                target = obj; break; // get out of this loop as soon as possible
            }

            if (obj.layer == earthLayerNumber)
            {
                backupTarget = obj; break;
            }
        }

        if (target == null && backupTarget != null)
        {
            target = backupTarget;
        }

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
