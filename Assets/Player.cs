using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HealthComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private HealthComponent playerHealthComponent;
    [SerializeField] private GameObject playerModel;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthComponent.HandleOnKilled += OnKilled;
        playerHealthComponent.HandleHealthUpdated += OnHealthUpdated;
    }

    public void Attack()
    {
        if (Physics.Raycast(transform.position, playerModel.transform.forward,out RaycastHit hitInfo, 1f, 1 << 7))
        {
            hitInfo.transform.GetComponent<Enemy>().ReceiveHit(Random.Range(30f,50f));
        }
    }

    void OnHealthUpdated(float updatedHealth)
    {
        
    }

    void OnKilled()
    {
        SceneManager.LoadScene(5);//GameOver
    }

    public void TakeDamage(float damage)
    {
        playerHealthComponent.TakeDamage(damage);
    }
}
