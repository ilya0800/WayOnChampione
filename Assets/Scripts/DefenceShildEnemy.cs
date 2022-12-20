using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceShildEnemy : MonoBehaviour, IDefenceShild
{
    private Animator animator;
    private int RandomNumber;
    private float distance;
    private bool CdRandomNumber = true;
    const int ActivationNumber = 3;
    const int deactivationActiveNumber = 2;
    
    [SerializeField] GameObject Player;
        

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        DistanceBetweenPlayer();
        RandomNumberForActiveShild();
        ActiveShelder();
    }

    public void ActiveShelder()
    {
       if(EnemyHpBarAndStamina.instance.PermissionUseStamin && distance < 3 && RandomNumber == ActivationNumber)
       {
            RandomNumber = deactivationActiveNumber;
            animator.SetBool("Shelder", true);
            StealStamin();
            StartCoroutine(CoolDownAnimation());
            StartCoroutine(CoolDownColliderShild());
       }     
    }
    
    private void DistanceBetweenPlayer()
    {
       distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
    }

    private void RandomNumberForActiveShild()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CdRandomNumber)
        {
            RandomNumber = Random.Range(1, 5);
            StartCoroutine(CdGenerationRandomNumber());
        }
    }

    IEnumerator CdGenerationRandomNumber()
    {
        CdRandomNumber = false;
        yield return new WaitForSeconds(1.5f);
        CdRandomNumber = true;
    }

    IEnumerator CoolDownAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Shelder", false);
    }
    
    IEnumerator CoolDownColliderShild()
    {
        transform.parent.GetComponentInParent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        transform.parent.GetComponentInParent<Collider2D>().enabled = true;
    }

    private void StealStamin()
    {
        EnemyHpBarAndStamina.instance.StealStaminActivShelder();
    }
}
