using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RockballSpell : MonoBehaviour
{
    public SpellScriptableObject spellToCast;

    private SphereCollider myCollider;
    private Rigidbody myRigidbody;

    private void Awake() 
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = spellToCast.SpellRadius;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = true;

        Destroy(this.gameObject, spellToCast.LifeTime);
    }

    private void Update() 
    {
        if(spellToCast.Speed > 0) transform.Translate(Vector3.forward * spellToCast.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Apply spell effect to whatever we hit
        // Apply hit particle effect
        // Apply sound effect

        Destroy(this.gameObject);

    }
}
