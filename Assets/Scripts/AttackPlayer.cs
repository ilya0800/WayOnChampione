using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    private bool CdDefultAttack = true;
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
  
    private void Update()
    {
        AttackPlayerToEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) 
        {   
            EnemyHpBarAndStamina.instance.HpDamageOfDefultAttack();
        }
    }

    private void AttackPlayerToEnemy()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CdDefultAttack && HpBarAndStaminaPlayer.instance.PermissionUseStamin)
        {
            CdDefultAttack = false;
            anim.SetBool("Attack", true);
            anim.SetBool("Shelder", false);
            StartCoroutine(CheckPermissionAttack());
        }
    }

    IEnumerator CheckPermissionAttack()
    {
        HpBarAndStaminaPlayer.instance.StealStaminDefoultAttack();
        yield return new WaitForSeconds(1);
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(1);
        CdDefultAttack = true;
    }
}
