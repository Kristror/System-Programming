using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _health;
    private Coroutine _healigCoroutine;
    public int Health
    {
        get => _health;
        set
        {
            if (value > _maxHealth) _health = _maxHealth;
            else _health = value;
        }
    }
    public void Start()
    {
        _health = 50;
        ReceiveHealing();
        ReceiveHealing();
    }
    
    public void ReceiveHealing()
    {
        if (_healigCoroutine != null)
        {
            Debug.Log("Alredy Healing");
            StopCoroutine(_healigCoroutine);
        }
        Debug.Log("Started Healing");
        _healigCoroutine = StartCoroutine(Healing(3));
    }

    private IEnumerator Healing(float duration)
    {
        while (duration > 0)
        {
            if (Health == _maxHealth)
            {
                Debug.Log("Reached max health");
                yield break;
            }
            Health += 5;
            Debug.Log($"Health: {Health}");
            yield return new WaitForSeconds(0.5f);
            duration -= 0.5f;
        }
        Debug.Log("Finished Healing");
    }
}