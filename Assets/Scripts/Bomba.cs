using UnityEngine;
using System.Collections;

public class Bomba : MonoBehaviour {

    public float hatosugar = 10;
    public float robbanoEro = 5;
    public float elesSebesseg = 4;

    private Rigidbody rb;
    private Light elesFeny;
    private bool eles = false;

	void Start () {
        elesFeny = GetComponentInChildren<Light>();
        rb = GetComponent<Rigidbody>();
        elesFeny.enabled = false;	
	}
	
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > elesSebesseg)
        {
            eles = true;
            elesFeny.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!eles)
            return;

        ContactPoint cp = collision.contacts[0];

        Collider[] colliders = Physics.OverlapSphere(cp.point, hatosugar);
        foreach (Collider c in colliders) {
            Rigidbody rb = c.attachedRigidbody; 
            if (rb)
            {
                rb.AddExplosionForce(robbanoEro, cp.point, hatosugar, 0.5f, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }

}
