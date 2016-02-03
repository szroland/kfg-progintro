using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CelElnyelo : MonoBehaviour {

    public Text text;
    private string textPrefix;

    private Dictionary<string, int> count = new Dictionary<string, int>();

    void Start()
    {
        if (text)
        {
            textPrefix = text.text;
        }
        Kijelzo();
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        Destroy(other.gameObject);

        int current = 0;
        if (count.ContainsKey(tag))
        {
            current = count[tag];
        }
        current++;
        count[tag] = current;

        Kijelzo();
    }

    void Kijelzo()
    {
        if (text)
        {
            string s = "";
            if (count.Count > 0) {
                s = textPrefix;
                foreach (KeyValuePair<string, int> entry in count)
                {
                    s = s + "\n" + entry.Key + ": " + entry.Value;
                }
            }
            text.text = s;
        }
    }


}
