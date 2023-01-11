using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DropItem : MonoBehaviour
{
    [Header("General")] 
    public string Name;
    public bool HasEffectIcon;
    public float RotationSpeed;
    public enum PickupEffect { InstaKill, HealthRegen, InfiniteMana };

    [Header("What the pickup does")]
    public PickupEffect Effect;
    [Tooltip("Duration in seconds. Leave as 0 if none.")]
    public int Duration = 0;

    [Header("UI")] 
    public Sprite EffectIcon;
    public TextMeshProUGUI PopUp;
    public Image EffectIconImage;
    
    private GameObject[] enemies;
    private ParticleSystem particleSystem;
    private bool isPickedUp = false;
    private Transform[] childTransforms;

    private void Start()
    {
        particleSystem = transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        // Rotating Animation
        transform.Rotate(0, RotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("AllyPassible") && !isPickedUp)
        {
            switch (Effect)
            {
                case PickupEffect.InstaKill:
                    PickedUp(false);
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (var enemy in enemies)
                    {
                        enemy.GetComponent<Entity>().TakeDamage(enemy.GetComponent<Entity>().MaxHealth, Player.Instance);
                    }

                    break;
                
                case PickupEffect.HealthRegen:
                    PickedUp(true);
                    Player.Instance.RegenerateHealthCoroutine(Duration);
                    break;
                
                case PickupEffect.InfiniteMana:
                    PickedUp(true);
                    Player.Instance.InfiniteManaCoroutine(Duration);
                    break;
                
                default:
                    Debug.LogWarning("No such item type found");
                    PickedUp(false);
                    break;
            }
        }
    }

    private void PickedUp(bool effectIcon)
    {
        isPickedUp = true;
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        StartCoroutine(ShowName());
        if (effectIcon)
        {
            StartCoroutine(ShowIcon(Duration));
        }
        StartCoroutine(StopSmoke());
    }

    private IEnumerator StopSmoke()
    {
        yield return new WaitForSeconds(6);
        particleSystem.Stop();
    }
    
    private IEnumerator ShowName()
    {
        PopUp.gameObject.SetActive(true);
        PopUp.text = Name;
        yield return new WaitForSeconds(3);
        PopUp.text = "";
        PopUp.gameObject.SetActive(false);
    }

    private IEnumerator ShowIcon(int duration)
    {
        EffectIconImage.gameObject.SetActive(true);
        EffectIconImage.sprite = EffectIcon;
        yield return new WaitForSeconds(duration);
        float execTime = 0f;
        float endTime = 10f;
        while (execTime < endTime)
        {
            execTime += 0.8f;

            Color c = EffectIconImage.color;
            if (Random.value<0.5f)
            {
                c.a = 0;
            }
            else
            {
                c.a = 255;
            }
            EffectIconImage.color = c;
            
            yield return null;
        }
        EffectIconImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}