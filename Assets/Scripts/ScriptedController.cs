using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

delegate void Execute();
delegate bool IsDone();

public class ScriptedController : MonoBehaviour {
    private Vector3 target;

    private Rigidbody rb;
    private Text text;

    public float alapMagassag = 5;

    public Vector3 Kp = new Vector3(10, 10, 10);
    public Vector3 Kv = new Vector3(5, 3, 5);
    public Vector3 maximalisToloEro = new Vector3(10, 20, 10);
    private float engineOn = 0.0f;

    public float maxCelTavolsag = 0.1f;
    public float maxCelSebesseg = 0.1f;

    Execute execute;
    IsDone isDone;

    private ParticleSystem engine;
    private Rigidbody lifting;

    private System.Threading.Thread m_Thread = null;
    EventWaitHandle _waitHandle = new AutoResetEvent(false);

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        engine = GetComponentInChildren<ParticleSystem>();
        text = GetComponentInChildren<Text>();

        m_Thread = new System.Threading.Thread(Run);
        m_Thread.Start();
        target = transform.position;
        target.y = alapMagassag;
    }

    public virtual void Run()
    {
        Varakozik(2);
        Motor(true);
        Varakozik(2);

        Felirat("Mit csináljak?");
    }

    public void Elenged()
    {        
        lock (this)
        {
                execute = delegate
                {
                    if (lifting != null)
                    {
                        lifting.transform.parent = null;
                        lifting.transform.rotation = Quaternion.identity;
                        lifting.velocity = Vector3.zero;
                        lifting.angularVelocity = Vector3.zero;
                        lifting.isKinematic = false;
                    }
                };
        }
        _waitHandle.WaitOne();
    }


    public bool Felszed(bool changeTag = false)
    { 
        lock (this)
        {
            float targetY = 0.0f;
            lifting = null;

            execute = delegate
            {
                Ray ray = new Ray(transform.position, -1 * transform.up);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    //Debug.Log("Hit something", hit.rigidbody);
                    if (hit.rigidbody)
                    {
                        lifting = hit.rigidbody;
                        targetY = this.transform.position.y - 0.8f;

                        lifting.transform.parent = this.transform;

                        if (changeTag)
                        {
                            lifting.gameObject.tag = tag;
                        }
                    }
                }
            };
            isDone = delegate
            {
                if (lifting == null)
                    return true;

                if (lifting.position.y > targetY)
                {
                    lifting.isKinematic = true;
                    lifting.velocity = Vector3.zero;
                    lifting.angularVelocity = Vector3.zero;
                    lifting.transform.position = transform.position + new Vector3(0, -0.8f, 0);
                    lifting.transform.rotation = Quaternion.identity;
                    return true;

                }
                else
                {
                    lifting.AddForce(0, 50, 0, ForceMode.Acceleration);
                    return false;
                }

            };
        }
        _waitHandle.WaitOne();
        return lifting != null;
    }

    public GameObject[] Keres(string tag)
    {
        GameObject[] result = null;
        lock (this)
        {
            execute = delegate
            {
                result = GameObject.FindGameObjectsWithTag(tag);
                //Debug.Log("Keres: " + result);
            };
        }
        _waitHandle.WaitOne();
        return result;
    }

    public GameObject KeresEgy(string tag)
    {
        GameObject[] kockak = Keres(tag);
        if (kockak == null || kockak.Length == 0)
            return null;
        return kockak[0];

    }

    public void Elore(float tavolsag)
    {
        lock (this)
        {
            execute = delegate
            {
                Vector3 dir = transform.forward.normalized;
                Vector3 t = target + tavolsag * dir;
                SetTarget(t.x, t.y, t.z);                                
            };
            isDone = delegate { return IsClose(); };
        }
        _waitHandle.WaitOne();
    }

    public void Fordul(float yRot)
    {
        lock(this)
        {
            execute = delegate
            {
                transform.Rotate(0, yRot, 0);
            };
        }
        _waitHandle.WaitOne();
    }

    public void Varakozik(float masodperc)
    {
        Thread.Sleep((int)(masodperc * 1000));
    }

    public void Kidob(GameObject objektum)
    {
        if (objektum != null)
        {
            lock (this)
            {
                execute = delegate
                {
                    Vector3 pos = transform.position + Vector3.down;
                    Instantiate(objektum, pos, Quaternion.identity);
                };
            }
            _waitHandle.WaitOne();
        }
    }

    public void Motor(bool on)
    {
        lock(this)
        {
            execute = delegate {
                engineOn = on ? 1.0f : 0.0f;
            };
        }
        _waitHandle.WaitOne();
    }

    public void Elmozdul(float deltaX, float deltaY, float deltaZ)
    {
        lock (this)
        {
            execute = delegate { SetTarget(target.x+ deltaX, target.y+ deltaY, target.z+ deltaZ); };
            isDone = delegate { return IsClose(); };
        }
        _waitHandle.WaitOne();

    }

    public void Menj(GameObject o, float deltaX, float deltaY, float deltaZ)
    {
        lock (this)
        {
            execute = delegate { SetTarget(o.transform.position.x + deltaX, o.transform.position.y + deltaY, o.transform.position.z + deltaZ); };
            isDone = delegate { return IsClose(); };
        }
        _waitHandle.WaitOne();


    }


    public void Menj(float x, float y, float z)
    {
        lock(this)
        {
            execute = delegate { SetTarget(x, y, z); };
            isDone = delegate { return IsClose(); };
        }
        _waitHandle.WaitOne();

    }

    private void SetTarget(float x, float y, float z)
    {
        target = new Vector3(x, y, z);
    }

    private bool IsClose()
    {
        Vector3 diff = transform.position - target;
        return diff.magnitude < maxCelSebesseg && rb.velocity.magnitude < maxCelSebesseg;
    }

    public void Felirat(string value)
    {
        if (text != null)
        {
            lock (this)
            {
                execute = delegate { text.text = value; };                
            }
            _waitHandle.WaitOne();
        }
    }

    void FixedUpdate () {

        Vector3 g = new Vector3(0, 9.81f, 0);
        Vector3 p = target - rb.transform.position;
        Vector3 v = -1*rb.velocity;
        
        Vector3 f = g+Vector3.Scale(Kv, v) +Vector3.Scale(Kp, p);

        f = Vector3.Min(f, maximalisToloEro);
        f = Vector3.Max(f, -maximalisToloEro);

        rb.AddForce(engineOn*f, ForceMode.Force);

        Debug.DrawLine(transform.position, target, Color.red);

        lock(this)
        {
            if (execute != null)
            {
                execute();
                execute = null;
                if (isDone == null)
                    _waitHandle.Set();
            }
            if (isDone != null)
            {
                if (isDone())
                {
                    isDone = null;
                    _waitHandle.Set();
                }
            }
        }
        if (engine != null)
        {
            ParticleSystem.EmissionModule emission = engine.emission;
            emission.enabled = engineOn > 0;            
            engine.transform.rotation = Quaternion.LookRotation(-f);
            engine.startSize = f.magnitude / 9.81f;

        }

    }

  
}
