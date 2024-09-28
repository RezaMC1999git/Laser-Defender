using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] GameObject particle;
    [SerializeField] AudioClip playerShootSound;
    [Range(0, 1)] [SerializeField] float shootSoundVolume = 0.7f;
    [SerializeField] AudioClip playerDeathSound;
    [Range(0,1)][SerializeField] float deathSoundVolume=0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed =10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    float xMin, xMax, yMin, yMax,avgX,avgY;
    Coroutine firingCoroutine;

    //cached references
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBounadries();
    }

    private void SetUpMoveBounadries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        avgX = xMax - xMin;
        Debug.Log(xMax);
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        avgY = yMax - yMin;
        Debug.Log(avgY);
    }

    // Update is called once per frame
    void Update()
    {
       Move();
        Fire();
    }
    private void Move()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            Vector2 TouchedPlace = Camera.main.ScreenToViewportPoint(touch.position);
            var deltaX = (TouchedPlace.x *avgX)-1;
            var deltaY = (TouchedPlace.y *avgY)-3;
            Debug.Log("DX" + deltaX + "DY" + deltaY);
            var newXPos = Mathf.Clamp(deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(deltaY, yMin, yMax);
            Vector2 FinallTouchedPlace = new Vector2(newXPos, newYPos);
            transform.position = Vector2.MoveTowards(transform.position, FinallTouchedPlace, moveSpeed *Time.deltaTime);
        }
    }
    private void NEWMove() 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 TouchedPlace = Camera.main.ScreenToViewportPoint(touch.position);
        //    Debug.Log(TouchedPlace);
            TouchedPlace.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, TouchedPlace, moveSpeed * Time.deltaTime);
        }
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) 
           firingCoroutine = StartCoroutine(FireContinuously());
        if (Input.GetButtonUp("Fire1"))
            StopCoroutine(firingCoroutine);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        health += damageDealer.getDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject dieParticle = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
            Destroy(dieParticle, 1f);
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, deathSoundVolume);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }
    public int GetPlayerHealth() 
    {
        if (health >= 0)
            return health;
        else
            return 0;
    }
}
