using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] private float detectPlayerDistance = 5f;

    [SerializeField] HealthComponent healthComponent;
    [SerializeField] protected Animator enemyAnimator;
    
    protected static int HitHash = Animator.StringToHash("Hit");
    protected static int IsAttackingHash = Animator.StringToHash("IsAttacking");
    protected static int DiedHash = Animator.StringToHash("HasDied");

    [SerializeField]
    protected bool isAlive = true;
    // Start is called before the first frame update
    void Awake()
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

    public virtual void ReceiveHit(float damage)
    {
        if (isAlive)
        {
            Debug.Log($"AttackHit! Health - {healthComponent.Health}");
        }
        healthComponent.TakeDamage(damage);
        enemyAnimator.SetTrigger(HitHash);
        Reaction();
    }

    public virtual void Reaction()
    {
        
    }

    public virtual void OnKilled()
    {
        enemyAnimator.ResetTrigger(IsAttackingHash);
        enemyAnimator.SetBool(DiedHash, true);
        

        isAlive = false;
    }

    void OnHealthUpdate(float updatedHealth)
    {
        Debug.Log(updatedHealth);
    }


}
