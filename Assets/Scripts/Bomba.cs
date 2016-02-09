using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomba : MonoBehaviour {

    public float hatosugar = 10;
    public float robbanoEro = 5;
    public float elesSebesseg = 4;

    public Transform robbanasEffect;

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
        List<Rigidbody> rbs = new List<Rigidbody>();
        foreach (Collider c in colliders)
        {
            Rigidbody rb = c.attachedRigidbody;
            if (rb != null && !rbs.Contains(rb))
                rbs.Add(rb);
        }
        foreach (Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(robbanoEro, cp.point, hatosugar, 0.5f, ForceMode.Impulse);
        }

        if (robbanasEffect != null)
        {
            GameObject obj = new GameObject();
            obj.name = "Robbanas";
            obj.AddComponent<MonoBehaviour>().StartCoroutine(Robbanas(obj, robbanasEffect, transform.position));
        }
        Destroy(gameObject);

    }

    static IEnumerator Robbanas(GameObject obj, Transform robbanasEffect, Vector3 position)
    {
        Transform effect = (Transform) Instantiate(robbanasEffect, position, Quaternion.identity);
        effect.parent = obj.transform;
        yield return new WaitForSeconds(3);        
        Destroy(effect.gameObject);
        Destroy(obj);

    }

}
