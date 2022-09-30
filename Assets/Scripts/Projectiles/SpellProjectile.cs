using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpellProjectile : MonoBehaviour
{
    public Entity player;
    public float spellSpeed;
    [SerializeField] private VisualEffect impactEffect;
    public float DamageValue = 10f;

    void Update()
    {
        StartCoroutine(SpellTimer());

        transform.position += transform.forward * spellSpeed * Time.deltaTime;

        IEnumerator SpellTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject collisionObject = other.gameObject;
        Vector3 collisionPoint = other.GetContact(0).point;

        VisualEffect impactEffectObject = Instantiate(impactEffect, collisionPoint, Quaternion.LookRotation(transform.forward));
        Destroy(impactEffectObject.gameObject, 1);
        if (collisionObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(DamageValue, player);
        }
        Destroy(gameObject);
    }
}
