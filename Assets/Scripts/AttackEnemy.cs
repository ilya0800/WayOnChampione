using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject Player;
    private Animator animator;
    private float DistanceBetweenEnemyAndPlayer;
    private bool PermissionAttack = true;
    public GameObject Enemy;

    private void Awake()
    {
        MoveToPlayer();
    }
    
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        MoveToPlayer();
        DefultAttackOnPlayer();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            HpBarAndStaminaPlayer.instance.HpDamage();
    }

    private void MoveToPlayer()
    {
        DistanceBetweenEnemyAndPlayer = Vector3.Distance(Enemy.transform.position, Player.transform.position);
        if (DistanceBetweenEnemyAndPlayer > 3f)
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    private void DefultAttackOnPlayer()
    {
        if (DistanceBetweenEnemyAndPlayer <= 3f && PermissionAttack
        && EnemyHpBarAndStamina.instance.PermissionUseStamin)
        {
            animator.SetBool("Attack", true);
            PermissionAttack = false;
            GetComponentInParent<EnemyHpBarAndStamina>().StealStaminDefoultAttack();
            StartCoroutine(CheckKdAttack());
            Debug.LogError("ky");
        }
    }
    
    IEnumerator CheckKdAttack()
    {
        yield return new WaitForSeconds(1); 
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(1);
        PermissionAttack = true;
    }
    private void SecondAttackEnemy()
    {

    }
}
