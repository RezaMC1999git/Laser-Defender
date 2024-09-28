using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserProjectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Enemy Death")]
    [SerializeField] GameObject particle;
    [SerializeField] AudioClip enemyDeathSound;
    [Range(0, 1)] [SerializeField] float shootSoundVolume = 0.7f;
    [SerializeField] AudioClip enemyShootSound;
    [Range(0,1)][SerializeField] float deathSoundVolume =0.7f;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f) 
        { 
        Fire();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserProjectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyShootSound, Camera.main.transform.position, shootSoundVolume);
        if (gameObject.name == ("LifeEnemy(Clone)"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.getDamage();
        damageDealer.Hit();
        if (health <= 0)
        {

            GameObject dieParticle = Instantiate(particle ,transform.position, Quaternion.identity) as GameObject;
            Destroy(dieParticle, 1f);
            AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, deathSoundVolume);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
        }
    }
}
