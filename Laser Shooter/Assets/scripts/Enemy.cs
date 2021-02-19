using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float ShotCounter;
    [SerializeField] float MinTimeBetweenShots = 0.1f;
    [SerializeField] float MaxTimeBetweenSorts = 3f;
    [SerializeField] GameObject Projectile;
    [SerializeField] float ProjectileSpeed;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float Durationofexplosion =1f;

    // Start is called before the first frame update
    void Start()
    {
        ShotCounter = Random.Range(MinTimeBetweenShots, MaxTimeBetweenSorts);
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        
    }

    private void CountDownAndShoot()
    {
        ShotCounter -= Time.deltaTime;
        if(ShotCounter <= 0)
        {
            Fire();
            ShotCounter = Random.Range(MinTimeBetweenShots, MaxTimeBetweenSorts);

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other )
    {
        DamageDealer damagaeDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damagaeDealer) { return; }
        ProcessHit(damagaeDealer);
    }

    private void ProcessHit(DamageDealer damagaeDealer)
    {
        health -= damagaeDealer.GetDamage();
        damagaeDealer.hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, Durationofexplosion);
    }
}
