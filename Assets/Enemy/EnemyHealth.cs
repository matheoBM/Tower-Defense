using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("The amount added to enemy max health after it dies")]
    [SerializeField] int healthRamp = 1;
    [SerializeField] AudioClip destructSound;

    int currentHitPoints = 0;
    Enemy enemy;
    

    void Start()
    {
        AddRigidbody();
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        //Deal damage
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            PlayDestroyAudio();
            gameObject.SetActive(false);
            enemy.DepositBank();
            maxHitPoints += healthRamp; //Increase deficulty
        }
    }

    void PlayDestroyAudio()
    {
        AudioSource.PlayClipAtPoint(destructSound, transform.position);
    }

}
