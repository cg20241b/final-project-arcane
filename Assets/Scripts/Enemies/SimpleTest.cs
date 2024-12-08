using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTest : MonoBehaviour
{
    public float MaxSpeed;
    private float Speed;

    private Collider[] hitColliders;
    private RaycastHit Hit;

    public float SightRange;
    public float DetectionRange;

    public Rigidbody rb;
    public GameObject Target;

    private bool seePlayer;

    void Start()
    {
        Speed = MaxSpeed;
    }

    void Update()
    {
        if(!seePlayer){
            hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var HitColloder in hitColliders){
                if(HitColloder.tag == "Player"){
                    Target = HitColloder.gameObject;
                    seePlayer = true;
                }
            }
        } else {
            if (Physics.Raycast(transform.position, (Target.transform.position - transform.position), out Hit, SightRange)){
                if (Hit.collider.tag != "Player"){
                    seePlayer = false;  
                } else {
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    Vector3 Move = new Vector3(Direction.x * Speed, 0, Direction.z * Speed);
                    rb.linearVelocity = Move;
                    transform.forward = Move;
                }
            }
        }
    }
}