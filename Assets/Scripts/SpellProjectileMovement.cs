using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpellProjectileMovement : MonoBehaviour
{
    public float spellSpeed;
    [SerializeField] private VisualEffect impactEffect;

    void Update()
    {
        StartCoroutine(SpellTimer());

        transform.position += Vector3.forward * spellSpeed * Time.deltaTime;

        IEnumerator SpellTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(impactEffect, transform.position, Quaternion.LookRotation(transform.forward));
        if (other.name == "Target")
        {
            other.GetComponent<EnemyHealthController>().health -= 10;
        }
        Destroy(gameObject);
    }
}
