using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 10;
    public int currentHealth;

    [Header("Boss States")]
    public bool isInvulnerable = false;

    [Header("FX")]
    public GameObject deathEffect;

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) 
            return;

        currentHealth -= damage; // damage is negative


        // --- FLASH EFFECT ---
        FlashOnHit flash = GetComponent<FlashOnHit>();
        if (flash != null)
            flash.Flash();

        // --- HIT SOUND ---
        HitSound hitSound = GetComponent<HitSound>();
        if (hitSound != null)
            hitSound.PlayRandomHitSound();

        // --- HIT PARTICLES (if attached using ParticleSystem) ---
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        if (ps != null)
            ps.Play();

        // --- ENRAGE CHECK ---
        if (currentHealth <= maxHealth / 2f)
        {
            if (anim != null)
                anim.SetBool("IsEnraged", true);
        }

        // --- DEATH ---
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        // Count boss kill (optional)
        if (EnemyKillTracker.Instance != null)
            EnemyKillTracker.Instance.AddKill();

        // Spawn death FX
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Big screen shake if available
        if (ScreenShake.Instance != null)
            ScreenShake.Instance.BigShake();

        // Disable Renderer
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.enabled = false;

        // Disable Collider
        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        // Disable ALL scripts except this one
        foreach (var behaviour in GetComponents<MonoBehaviour>())
        {
            if (behaviour != this && behaviour.enabled)
                behaviour.enabled = false;
        }

        Destroy(gameObject, 0.5f);
    }
}
