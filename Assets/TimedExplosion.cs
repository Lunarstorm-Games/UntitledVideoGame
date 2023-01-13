using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosion : MonoBehaviour
{
    private ParticleSystem[] particles;
    private AudioSource _audioSource;
    public float triggerAfterSeconds = 30;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        particles = GetComponentsInChildren<ParticleSystem>();
        StartCoroutine(StartDelay());

    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(triggerAfterSeconds);
        _audioSource.Play();
        foreach (var particle in particles)
        {
         particle.Play(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
