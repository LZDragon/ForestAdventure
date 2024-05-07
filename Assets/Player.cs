using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(HealthComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private HealthComponent playerHealthComponent;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Slider playerHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthComponent.HandleOnKilled += OnKilled;
        playerHealthComponent.HandleHealthUpdated += OnHealthUpdated;
        playerHealthBar.maxValue = playerHealthComponent.Health;
        playerHealthBar.value = playerHealthComponent.Health;
    }

    public void Attack()
    {
        if (Physics.SphereCast(transform.position,5f, playerModel.transform.forward, out RaycastHit hitInfo, 5f, 1 << 7))
        {
            hitInfo.transform.GetComponent<Enemy>().ReceiveHit(Random.Range(30f,50f));
        }
    }

    void OnHealthUpdated(float updatedHealth)
    {
        playerHealthBar.value = updatedHealth;
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
