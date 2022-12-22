using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllEnemyStateFirst : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject Player;
    private Animator animator;
    private float DistanceBetweenEnemyAndPlayer;
    private bool PermissionDefultAttack = true;
    private bool PermissionSecondAttack = true;
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
        SecondAttack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !EnemyHpBarAndStamina.instance.ActivelyStateEnemy)
            HpBarAndStaminaPlayer.instance.HpDamageDefultAttack();
        else if (collision.collider.CompareTag("Player") && EnemyHpBarAndStamina.instance.ActivelyStateEnemy)
            HpBarAndStaminaPlayer.instance.HpDamageSecondAttack();
    }

    private void MoveToPlayer()
    {
        DistanceBetweenEnemyAndPlayer = Vector3.Distance(Enemy.transform.position, Player.transform.position);
        if (DistanceBetweenEnemyAndPlayer > 3f)
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    private void DefultAttackOnPlayer()
    {
        if (DistanceBetweenEnemyAndPlayer <= 3f && PermissionDefultAttack
        && EnemyHpBarAndStamina.instance.PermissionUseStamin && !EnemyHpBarAndStamina.instance.ActivelyStateEnemy )
        {
            animator.SetBool("Attack", true);
            PermissionDefultAttack = false;
            GetComponentInParent<EnemyHpBarAndStamina>().StealStaminDefoultAttack();
            StartCoroutine(CheckKdDefultAttack());
        }
    }

    IEnumerator CheckKdDefultAttack()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Attack", false);
        animator.SetBool("SecondAttack", false);
        yield return new WaitForSeconds(1);
        PermissionDefultAttack = true;
    }

    private void SecondAttack()
    {
        if(DistanceBetweenEnemyAndPlayer <= 3f && PermissionSecondAttack
            && EnemyHpBarAndStamina.instance.PermissionUseStamin && EnemyHpBarAndStamina.instance.ActivelyStateEnemy)
        {
            animator.SetBool("SecondAttack", true);
            PermissionSecondAttack = false;
            GetComponentInParent<EnemyHpBarAndStamina>().StealStaminSecondAttack();
            StartCoroutine(CheckKdSecondAttack());
            Debug.Log("SecondAttack");
        }
    }
    
    IEnumerator CheckKdSecondAttack()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("SecondAttack", false);
        yield return new WaitForSeconds(2);
        PermissionSecondAttack = true;
    }
}

public class ControllEnemyStateSecond: MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {

    }

    private void TransitionEnemyStateSecond()
    {

    }
}
