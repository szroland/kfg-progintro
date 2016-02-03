using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gyar : MonoBehaviour {

    public GameObject[] gyartmany;
    public string gyartmanyTag;
    public bool veletlenSorrend;

    private int utolso = 0;

    private SphereCollider sphere;

    void Start()
    {
        sphere = GetComponent<SphereCollider>();
        Emit();
    }

    void OnTriggerExit(Collider other)
    {
        Emit();
    }

    void Emit()
    {
        if (gyartmany.Length > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, sphere.radius);
            if (colliders.Length <= 1) //self 
            {
                int index = veletlenSorrend ? Random.Range(0, gyartmany.Length) : utolso;
                utolso = (utolso + 1) % gyartmany.Length;

                GameObject instance = Instantiate(gyartmany[index], transform.position, Quaternion.identity) as GameObject;
                if (gyartmanyTag != null && gyartmanyTag.Trim() != "")
                    instance.tag = tag;
            }

        }

    }

}


