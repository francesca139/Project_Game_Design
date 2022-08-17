using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DwarfHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float damageModifier;
    public GameObject drop;
    public bool drops;
    public AudioClip deathSound;

    float currentHealth;

    public Slider enemyHealthIndicator;

   // AudioSource enemyAS;

    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemyHealthIndicator.maxValue = enemyMaxHealth;
        enemyHealthIndicator.value = currentHealth;

        // enemyAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        /* QUESTA PARTE DI CODICE CONSERVIAMOLA NEL CASO IN CUI VOLESSIMO APPLICARE DEI PARTICELLARI 
        
        if (onFire && burnTime.time > nextBurn)
        {
            addDamage(burnDamage);
            nextBurn += burnInterval;
        }
        if (onFire && burnTime.time > endBurn)
        {
            onFire = false;
            burnEffects.SetActive(false);
        }

        */
    }

    // Classico calcolo dei danni

    public void addDamage(float damage)
    {
        enemyHealthIndicator.gameObject.SetActive(true);
        damage = damage * damageModifier;

        if (damage <= 0) return;
        currentHealth -= damage;

        enemyHealthIndicator.value = currentHealth;
       
        // enemyAS.Play();
        
        if (currentHealth <= 0) makeDead();
    }

    /* Anche qua, ce lo teniamo nel momento in cui vogliamo inserire dei particellari
     
    public void damageFX (Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }

    public void addFire()
    {
        if (!canBurn) return;
        onFire = true;
        burnEffects.SetActive(true);
        endBurn = burnTime.time + burnTime;
        nextBurn = burnTime.time + burnInterval;
    }

    */

    void makeDead()
    {
        // AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.15f);

        Destroy(gameObject.transform.root.gameObject);

       //  if (drops) Instantiate(drop, transform.position, trasnform.rotation); Vediamo se i nemici lasciano cadere qualcosa quando vengono sconfitti
    }
}
