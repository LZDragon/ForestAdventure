using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAggressive : Enemy
{
    [SerializeField] private AimConstraint aimConstraint;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeDecision());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected IEnumerator MakeDecision()
    {

        while (isAlive)
        {
            if (DetectPlayer())
            {
                aimConstraint.constraintActive = true;

                    Attack();

            }
            else
            {
                aimConstraint.constraintActive = false;
            }
            yield return new WaitForSeconds(3f);
        }

    }

    void Attack()
    {
        
        enemyAnimator.SetTrigger("IsAttacking");
    }


}
