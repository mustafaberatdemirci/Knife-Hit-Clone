using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public bool ready;
    Process _reach;
    BoxCollider2D col;
    AudioSource _source;
    public AudioClip woodsound;
    public GameObject woodeffect;
    //private float _waitTime=0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ready = true;
        _reach = GameObject.Find("Process").GetComponent<Process>();
        col = GetComponent<BoxCollider2D>();
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //_waitTime += Time.deltaTime;
        if (Input.GetMouseButton(0) && ready == true /*&& _waitTime>1*/)
        {
            //_waitTime = 0;
            Throw(); 
        }
    }

    void Throw()
    {
        rb.velocity = new Vector2(0, speed);
        ready = false; 
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Trunk")
        {
            _reach.throwtrueknife++;
            if (_reach.throwtrueknife == _reach.target)
            {
                _reach.Smash();
            }
            else
            {
                woodeffect.SetActive(true);
                Destroy(woodeffect, 2.0f);
                _source.PlayOneShot(woodsound);
                rb.isKinematic = true;
                rb.velocity = new Vector2(0, 0);
                transform.SetParent(other.transform);
                col.enabled = false;
                _reach.Spawn();
            }
        }
    }
}