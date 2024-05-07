using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float detectPlayerDistance = 5f;

    [SerializeField] HealthComponent healthComponent;
    [SerializeField] protected Animator enemyAnimator;

    protected bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        healthComponent.HandleOnKilled += OnKilled;
        healthComponent.HandleHealthUpdated += OnHealthUpdate;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool DetectPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < detectPlayerDistance)
        {
            return true;
        }
        return false;
    }

    public void ReceiveHit(float damage)
    {
        healthComponent.TakeDamage(damage);
        Reaction();
    }

    void Reaction()
    {
        
    }

    void OnKilled()
    {
        if (enemyAnimator)
        {
            enemyAnimator.SetBool("Died", true);
        }

        isAlive = false;
    }

    void OnHealthUpdate(float updatedHealth)
    {
        
    }


}
