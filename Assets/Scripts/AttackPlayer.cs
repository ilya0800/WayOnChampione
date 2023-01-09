using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    private bool CdDefultAttack = true;
    private Animator anim;
    [SerializeField] AudioSource[] audio = new AudioSource[2];
    Collider2D[] Collider;
    RaycastHit2D raycast;

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
            audio[0].Play();
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            audio[1].Play();
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
            FindEnemy();
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
    
    private void FindEnemy()
    {
        raycast = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 3f);
        if (raycast.collider == null)
            audio[1].Play();
        Debug.Log("2");
    }
}
