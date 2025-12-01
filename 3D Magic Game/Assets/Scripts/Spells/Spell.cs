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

        Destroy(this.gameObject, spellToCast.Lifetime);
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

        Destroy(this.gameObject);
    }
}
