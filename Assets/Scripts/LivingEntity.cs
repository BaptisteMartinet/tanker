using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead = false;

    public event System.Action OnDeath;

    public virtual void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage)
    {
        if ((health -= damage) <= 0)
            this.Die();
    }

    protected void Die()
    {
        if (this.dead)
            return;
        this.dead = true;
        if (OnDeath != null)
            OnDeath();
        GameObject.Destroy(this.gameObject);
    }
}
