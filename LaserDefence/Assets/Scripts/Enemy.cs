using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Singleton")]
    [SerializeField] GameSession gameSession;

    [Header("Enemey Stats")]
    [SerializeField] float health = 100f;
    float shotCounter;

    [Header("Shooting")]
    [SerializeField] float minTimeBetweenShot = 0.2f;
    [SerializeField] float maxTimeBetweenShot = 3f;
    [SerializeField] GameObject energyPrefab;
    [SerializeField] float projectileSpeed = 5f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    [SerializeField] int scoreValue = 100;

    [Header("Visual Effecs")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float deathDurationVFX = 0.5f;


    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        gameSession = FindObjectOfType<GameSession>();
    }
	
	// Update is called once per frame
	void Update () {
        CountDownAndShot();
	}

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        }
    }

    private void Fire()
    {
        GameObject energy = Instantiate(
            energyPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        energy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddtoScore(scoreValue);
        Destroy(gameObject);
        GameObject flames = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(flames, deathDurationVFX);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

    }
}
