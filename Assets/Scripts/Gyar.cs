﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gyar : MonoBehaviour {

    public GameObject[] gyartmany;
    public bool veletlenSorrend;
    public string beallitandoTag;

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
                if (beallitandoTag != null && beallitandoTag != "Untagged" && beallitandoTag.Trim() != "")
                    instance.tag = beallitandoTag;
            }

        }

    }

}


