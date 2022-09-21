using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class KnifeHit : MonoBehaviour
{
    Rigidbody2D _rb;
    Process _kod;
    Knife _kod2;
    AudioSource _source;
    public AudioClip sound;

    public GameObject appleeffect;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponentInParent<Rigidbody2D>();
        _kod = GameObject.Find("Process").GetComponent<Process>();
        _source = this.GetComponentInParent<AudioSource>();
        _kod2 = this.gameObject.GetComponentInParent<Knife>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "under" || other.gameObject.tag == "trap")
        {
            _source.PlayOneShot(sound);
            _rb.velocity = new Vector2(_rb.velocity.x, -20.0f);
            _kod.gameover = true;
        }

        if (other.gameObject.tag == "apple")
        {
            appleeffect.SetActive(true);
            _kod.applecount++;
            PlayerPrefs.SetInt("apple", _kod.applecount);
            Destroy(appleeffect, 2.0f);
            Destroy(other.gameObject);
        }
    }
}