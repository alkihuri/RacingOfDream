using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource engineSound;
    public float startVolume;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        startVolume = 0.15f;
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude/ 25 - startVolume;
        engineSound.volume = startVolume + speed;
    }
}
