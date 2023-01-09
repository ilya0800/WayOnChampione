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
    private int RandomNuberForActiveKickLeg;
    private bool PermissionDefultAttack = true;
    private bool PermissionSecondAttack = true;
    private bool PermissionKickLeg = true;
    private bool KickLeg = true;
    private const int ConstNumberForActiveKickLeg = 5;
    public GameObject Enemy;
    private Rigidbody2D rigidbody2D;
    [SerializeField] AudioSource[] Sounds = new AudioSource[3];
    private RaycastHit2D raycast;
    

    private void Awake()
    {
        MoveToPlayer();
    }
    
    private void Start()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        MoveToPlayer();
        DefultAttackOnPlayer();
        SecondAttack();
        KickLegEnemy();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !EnemyHpBarAndStamina.instance.ActivelyStateEnemy)
        {
            HpBarAndStaminaPlayer.instance.HpDamageDefultAttack();
            Sounds[0].Play();
        }
        else if (collision.collider.CompareTag("Player") && EnemyHpBarAndStamina.instance.ActivelyStateEnemy)
            HpBarAndStaminaPlayer.instance.HpDamageSecondAttack();
        
        if (collision.collider.CompareTag("Player") && KickLeg)
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0), ForceMode2D.Force);
            HpBarAndStaminaPlayer.instance.HpDamageOfKickLeg();
            KickLeg = false;
            Sounds[2].Play();
        }
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
            FindPlayer();
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
            FindPlayer();
            Sounds[0].Play();

        }
    }
    
    IEnumerator CheckKdSecondAttack()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("SecondAttack", false);
        yield return new WaitForSeconds(2);
        PermissionSecondAttack = true;
    }

    private void KickLegEnemy()
    {
        RandomNuberForActiveKickLeg = Random.Range(0, 6);
        if (DistanceBetweenEnemyAndPlayer <= 2.50 && RandomNuberForActiveKickLeg == ConstNumberForActiveKickLeg && PermissionKickLeg)
        {
            animator.SetBool("KickLeg", true);
            animator.SetBool("IdleFirst", false);
            PermissionKickLeg = false;
            StartCoroutine(KickLegCoolDown());
            GetComponentInParent<EnemyHpBarAndStamina>().StealStaminKickLeg();
        }
        else if (DistanceBetweenEnemyAndPlayer <= 2.50 && RandomNuberForActiveKickLeg == ConstNumberForActiveKickLeg && PermissionDefultAttack &&
            EnemyHpBarAndStamina.instance.ActivelyStateEnemy)
        {
            animator.SetBool("KickLegWithTwoSwords", true);
            animator.SetBool("IdleStateSecond", false);
            PermissionKickLeg = false;
            StartCoroutine(KicklegWithTwoSwordsCoolDown());
            GetComponentInParent<EnemyHpBarAndStamina>().StealStaminKickLeg();
            Debug.LogWarning("Done");
            Sounds[2].Play();
        }
    }

    IEnumerator KickLegCoolDown()
    {
        KickLeg = false;
        RandomNuberForActiveKickLeg = 3;
        yield return new WaitForSeconds(2);
        animator.SetBool("KickLeg", false);
        animator.SetBool("IdleFirst", true);
        yield return new WaitForSeconds(4);
        PermissionKickLeg = true;
        KickLeg = true;
    }

    IEnumerator KicklegWithTwoSwordsCoolDown()
    {
        KickLeg = false;
        RandomNuberForActiveKickLeg = 3;
        yield return new WaitForSeconds(2);
        animator.SetBool("KicklegWithTwoSwords", false);
        animator.SetBool("IdleStateSecond", true);
        yield return new WaitForSeconds(4);
        PermissionKickLeg = true;
        KickLeg = true;
    }

    private void FindPlayer()
    {
      raycast = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 3f);
        if (raycast.collider == null)
            Sounds[1].Play();
        Debug.Log(1);
    }
}
