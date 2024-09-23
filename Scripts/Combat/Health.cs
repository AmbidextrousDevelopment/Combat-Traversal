using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    private bool _isInvunerable;

    public event Action OnTakeDamage;
    public event Action OnDie;

    public bool isDead => health == 0;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    public void IsInvunerable(bool isInvunerable)
    {
        _isInvunerable = isInvunerable;
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) { return; }

        if (_isInvunerable) { return; }

        health = Mathf.Max(health - damage, 0); // health - damage, if (health < 0), health = 0

        OnTakeDamage?.Invoke();

        if (health == 0)
        {
            OnDie?.Invoke();
        }


        Debug.Log(health);
    }

    public void OnDIe()
    {
        
    }

   
}
