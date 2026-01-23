using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    public SpellScriptableObject spellToCast;

    private PlayerMagicSystem playerMagicSystem;
    private GameObject owner;

    private SphereCollider myCollider;
    private Rigidbody rb;

    public bool isCorrectSpell;

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
    public void Initialize(GameObject owner, PlayerMagicSystem pms)
    {
        this.owner = owner;
        this.playerMagicSystem = pms;
    }

    private void Start()
    {
        RaycastHit hit;
        float maxAimDistance = 100f;

        //if hit, spell goes to hit point, otherwise use camera.forward.
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxAimDistance))
        {
            float distanceToHit = Vector3.Distance(Camera.main.transform.position, hit.point);
            if (distanceToHit > 3f)
            {
                transform.LookAt(hit.point);
            }
            else
            {
                //hit too close = camera.forward
                transform.rotation = Camera.main.transform.rotation;
            }
        }
        else
        {
            //no hit = camera.forward
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

        EnemyAI hitEnemy = other.GetComponentInParent<EnemyAI>();
        if (hitEnemy == null)
        {
            // destroy if not an enemy
            Destroy(this.gameObject);
            return;
        }

        //ensure there is a reference to player magic system
        if (playerMagicSystem == null)
            playerMagicSystem = FindFirstObjectByType<PlayerMagicSystem>();

        // determine if the spell matches the enemy type
        isCorrectSpell = false;
        if (hitEnemy.fireGolem && playerMagicSystem.fireBorder) isCorrectSpell = true;
        if (hitEnemy.waterGolem && playerMagicSystem.waterBorder) isCorrectSpell = true;
        if (hitEnemy.lightningGolem && playerMagicSystem.lightBorder) isCorrectSpell = true;
        if (hitEnemy.psychicGolem && playerMagicSystem.psychBorder) isCorrectSpell = true;

        if (isCorrectSpell)
        {
            hitEnemy.health -= spellToCast.Damage;
        }

        Destroy(this.gameObject);
    }
}
