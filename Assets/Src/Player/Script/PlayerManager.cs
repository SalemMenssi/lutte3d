using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Player player;

    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private HealthBar StaminaBar;
    private Animator animator;
    [SerializeField]
    private SceneController sceneController;


    private float staminaRecoveryRate = 3f;
    private float maxStamina;

    void Start()
    {
        player = new Player("Slimre");
        player.DisplayPlayerInfo();
        animator = GetComponentInChildren<Animator>();
        maxStamina = player.Stamina;
        healthBar.SetMaxHealth(player.Health);
        StaminaBar.SetMaxHealth(player.Stamina);

        
        StartCoroutine(RecoverStamina());
    }

    private IEnumerator Hitplayer()
    {
        Debug.Log("hit Player " + player.Name);
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.2f);
        
    }

    public void TakeDamage(float damage)
    {
        
        player.Health -= damage;
        healthBar.SetHealth(player.Health);
        
        if (player.Health <= 0)
        {
            Die();
            sceneController.LoadScene("City");
        }
        else
        {
           StartCoroutine(Hitplayer()); 
        }
    }

    public void StaminaDecrease(float dec)
    {
        player.Stamina -= dec;
        StaminaBar.SetHealth(player.Stamina);
    }

    private void Die()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die")) 
        {
            StartCoroutine(DiePlayer());
        }
    }

    private IEnumerator DiePlayer()
    {
        Debug.Log("Die " + player.Name);
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.1f);
        animator.SetBool("isDie", true);
    }
    private IEnumerator RecoverStamina()
    {
        while (true)
        {
            if (player.Stamina < maxStamina) 
            {
                player.Stamina += staminaRecoveryRate * Time.deltaTime;
                player.Stamina = Mathf.Min(player.Stamina, maxStamina); 
                StaminaBar.SetHealth(player.Stamina);
            }

            yield return null; 
        }
    }
}
   

