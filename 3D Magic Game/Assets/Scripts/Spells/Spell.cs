using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    public SpellScriptableObject spellToCast;

    private SphereCollider myCollider;
    private Rigidbody rb;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();

        myCollider.isTrigger = true;
        myCollider.radius = spellToCast.SpellRadius;

        rb.useGravity = false;
        rb.isKinematic = true;

        //destroy after a few seconds
        Destroy(this.gameObject, spellToCast.Lifetime);
    }

    private void Start()
    {
        RaycastHit hit;
        float maxAimDistance = 100f;

        // If we hit something, look at the hit point. Otherwise use camera forward.
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxAimDistance))
        {
            float distanceToHit = Vector3.Distance(Camera.main.transform.position, hit.point);
            if (distanceToHit > 3f)
            {
                transform.LookAt(hit.point);
            }
            else
            {
                // Hit too close — align with camera forward so it flies straight ahead
                transform.rotation = Camera.main.transform.rotation;
            }
        }
        else
        {
            // No hit: point the spell along the camera forward vector
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    private void Update()
    {
        if(spellToCast.Speed > 0)
        {
            transform.Translate(Vector3.forward * spellToCast.Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //apply particles
        //apply hit effects
        //apply sfx

        //apply damage
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.health -= spellToCast.Damage;
        }

        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHealth pHealth = other.GetComponent<PlayerHealth>();
            pHealth.health -= spellToCast.Damage;
        }

        Destroy(this.gameObject);
    }
}
