using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectileMovement : MonoBehaviour
{
    public float spellSpeed;

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
        if(other.name == "Target")
        {
            other.GetComponent<EnemyHealthController>().health -= 10;
        }
        Destroy(gameObject);
    }
}
