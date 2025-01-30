using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerControles playerController;

    [SerializeField]
    private Transform AttackPointK;
    [SerializeField]
    private Transform AttackPointP;

    
    private float attackRangeY = 0.25f;

    public LayerMask enemyLayer ;

    private PlayerManager playerManager;

    [SerializeField]
    private float moveSpeed = 2.5f;

    private InputAction move;
    private InputAction punch;
    private InputAction kick;

    private Rigidbody rb;
    private Animator animator;

    Vector2 moveDirection = Vector2.zero;
    private void Awake()
    {
        playerController = new PlayerControles();
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }


    private void OnEnable()
    {
        move = playerController.Player.Move;
        move.Enable();

        punch = playerController.Player.Punch;
        punch.Enable();
        punch.performed += Punch;

        kick = playerController.Player.Kick;
        kick.Enable();
        kick.performed += Kick;

    }

    private void OnDisable()
    {
        move.Disable();
        punch.Disable();
        kick.Disable();
    }



    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            animator.SetBool("isWalk", true);

            // Flip the object on the X-axis based on the direction
            if (moveDirection.x != 0)
            {
                float flipDirection = moveDirection.x > 0 ? 0.25f : -0.25f;
                transform.localScale = new Vector3(flipDirection, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }


    private void FixedUpdate()
    {

        transform.Translate(moveDirection.x * moveSpeed * Time.fixedDeltaTime, 0f, 0f);
    }

    private void Punch(InputAction.CallbackContext context)
    {
        float punchStaminaCost = 5f;
        if (playerManager.player.Stamina < punchStaminaCost)
        {
            Debug.Log("Not enough stamina to punch!");
            return;
        }

        animator.SetTrigger("Punch");
        playerManager.StaminaDecrease(punchStaminaCost);

        Collider[] hitEnemies = Physics.OverlapSphere(AttackPointP.position, attackRangeY, enemyLayer);
        Debug.Log(hitEnemies[0]);
        foreach (Collider hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy);
            Enemy enemy = hitEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(playerManager.player.Strength);
               
                Debug.Log("We hit " + enemy.name + ". Remaining health: " + enemy.Health);
            }
            else
            {
                Debug.LogWarning("Hit object does not have an Enemy component.");
            }
        }
    }


    private void Kick(InputAction.CallbackContext context)
    {
        float kickStaminaCost = 7f;
        if (playerManager.player.Stamina < kickStaminaCost)
        {
            Debug.Log("Not enough stamina to kick!");
            return;
        }

        animator.SetTrigger("Kick");
        playerManager.StaminaDecrease(kickStaminaCost);

        Collider[] hitEnemies = Physics.OverlapSphere(AttackPointK.position, attackRangeY, enemyLayer);
        
        foreach (Collider hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy);
            Enemy enemy = hitEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(playerManager.player.Strength);
                Debug.Log("We hit " + enemy.name + ". Remaining health: " + enemy.Health);
            }
            else
            {
                Debug.LogWarning("Hit object does not have an Enemy component.");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;  
        Gizmos.DrawWireSphere(AttackPointK.position, attackRangeY);
        Gizmos.DrawWireSphere(AttackPointP.position, attackRangeY);
    }
    

    

}