using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceHero : MonoBehaviour
{
    private Animator anim;
    private bool PermissionActiveShelder = true;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        ActiveShelder();
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponentInParent<BoxCollider2D>().enabled = true;
            gameObject.GetComponentInParent<Collider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.Play();
            Debug.Log("audi");
        }
    }

    private void ActiveShelder()
    {
        if (Input.GetMouseButton(1) && PermissionActiveShelder && HpBarAndStaminaPlayer.instance.PermissionUseStamin)
        {
            anim.SetBool("Shelder", true);
            anim.SetBool("Attack", false);
            StartCoroutine(CoolDownActiveShild());
        }
    }

    IEnumerator CoolDownActiveShild()
    {
        gameObject.GetComponentInParent<Collider2D>().enabled = false;
        gameObject.GetComponentInParent<BoxCollider2D>().enabled = false;
        PermissionActiveShelder = false;
        HpBarAndStaminaPlayer.instance.StealStaminActivShelder();
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("Shelder", false);
        PermissionActiveShelder = true;
        gameObject.GetComponentInParent<Collider2D>().enabled = true;
        gameObject.GetComponentInParent<BoxCollider2D>().enabled = true;
    }
}
