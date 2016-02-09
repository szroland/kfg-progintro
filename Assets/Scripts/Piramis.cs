using UnityEngine;
using System.Collections;

public class Piramis : MonoBehaviour {

    public GameObject epitoElem;
    public int meret = 10;
    public float xTavolsag = 1;
    public float yTavolsag = 1;
    public float zTavolsag = 1;

	void Start () {

        for (int i=0; i< meret; i++)
        {
            int len = meret - i;
            float comp = i * 0.5f;
            Sor(len, i * yTavolsag, comp, comp, xTavolsag, 0);
            Sor(len, i * yTavolsag, comp+xTavolsag * len, comp, 0, zTavolsag);
            Sor(len, i * yTavolsag, comp+xTavolsag * len, comp+zTavolsag * len, -xTavolsag, 0);
            Sor(len, i * yTavolsag, comp, comp+zTavolsag * len, 0, -zTavolsag);
        }

    }

    void Sor(int hossz, float h, float x, float z, float dx, float dz)
    {
        for (int i = 0; i < hossz; i++)
        {
            Vector3 pos = new Vector3(x + i * dx, h, z + i * dz);
            Instantiate(epitoElem, transform.position + pos, Quaternion.identity);
        }
    }

}
