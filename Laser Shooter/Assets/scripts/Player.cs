using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float padding = 0.5f;

    [Header("Projecile")] 
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float ProjectileSpeed = 20f;
    [SerializeField] float ProjectileFiringPeriod = 0.1f;
    float xMax;
    float xMin;
    float yMax;
    float yMin;
    Coroutine FiringCouroutine;
    [SerializeField] float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBroundries();
       
    }
    
    IEnumerator FiringContineously()
    {   while (true)
        {
            GameObject laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }

    }
    void Update()
    {
        move();
        Fire();
    }
    private void OnTriggerEnter2D(Collider2D other)
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
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
      if(Input.GetButtonDown("Fire1"))
      {
       FiringCouroutine =     StartCoroutine( FiringContineously());
      }
      if(Input.GetButtonUp("Fire1"))
      {
            StopCoroutine(FiringCouroutine);
      }
    }

    private void SetUpMoveBroundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y+padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-padding;


    }

    // Update is called once per frame
   

    private void move()
    {
        float DeltaX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        float DeltaY = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;

        float NewXpos = Mathf.Clamp( transform.position.x + DeltaX,xMin,xMax) ;

        float NewYpos =Mathf.Clamp( transform.position.y + DeltaY,yMin,yMax) ;

        transform.position = new Vector2(NewXpos,NewYpos);
        
       

    }
}
