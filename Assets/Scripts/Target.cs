using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { Blue, Red, Green, Yellow, Other};
public enum DeathType { Explode, Nothing, AI, Objective}
public class Target : MonoBehaviour
{
    float health;
    public float maxHealth;
    public Team team;
    public DeathType deathType;

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
    }
    public float GetHealth()
    {
        return health;
    }
    void Die()
    {

        switch (deathType)
        {
            case DeathType.Explode:
                Explode tmp = this.GetComponentInParent<Explode>();
                tmp.Explosion();
                break;

            case DeathType.Nothing:
                GameManager.RespawnPlayer(this);
                break;
            case DeathType.AI:
                Destroy(this.gameObject);
                break;
            case DeathType.Objective:
                GameManager.Instance.UpdateTargetCounter();
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
