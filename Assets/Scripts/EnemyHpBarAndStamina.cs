using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBarAndStamina : MonoBehaviour, IHpBarAndStaminaForEnemy
{
    public static EnemyHpBarAndStamina instance = null;
    public float Hp = 100;
    public bool PermissionUseStamin = true;
    public bool ActivelyStateEnemy = false;
    public bool PlayTimeLine = false;
    private float time = 0f;
    [SerializeField] private float CurrentStamin;
    private float CurrentHp;
    private float FullStamina = 100;
    
    [Header("Hp")]
    [SerializeField] Image FullHpEnemy;
    [Header("Stamina")]
    [SerializeField] Image EnemyStamina;
    private Animator animator;
    [SerializeField] AudioSource[] Audio; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        CurrentStamin = FullStamina;
        CurrentHp = Hp;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance = this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        GetStamin();
        CheckStamina();
        CheckHp();
    }

    public void CheckHp()
    {
        if (Hp <= 80)
        {
            ActivelyStateEnemy = true;
            ActiveAnimEnemyStateSecond();
            ActiveTimeLine();
        }   
        if(Hp <= 0)
        {
            Dead();
        }
    }

    private void ActiveAnimEnemyStateSecond()
    {
        if (ActivelyStateEnemy)
        {
            animator.SetBool("IdleStateSecond", true);
            animator.SetBool("Attack", false);
        }
    }

    private void ActiveTimeLine()
    {
        PlayTimeLine = true;
    }

    public void CheckStamina()
    {
        if (CurrentStamin < 20)
            PermissionUseStamin = false;
        else if(CurrentStamin > 21)
            PermissionUseStamin = true;
    }

    public void HpDamageOfDefultAttack()
    {
        Hp -= 15;
        FullHpEnemy.fillAmount = Hp * 0.01f;
    }

    public void HpDamageOfSecondAttack()
    {
        Hp -= 40;
        FullHpEnemy.fillAmount = Hp * 0.01f;
    }

    public void StealStaminDefoultAttack()
    {
        CurrentStamin -= 15;
        EnemyStamina.fillAmount = CurrentStamin * 0.01f;
    }

    public void StealStaminActivShelder()
    {
        CurrentStamin -= 10;
       EnemyStamina.fillAmount = CurrentStamin * 0.01f;
    }

    public void StealStaminSecondAttack()
    {
        CurrentStamin -= 25;
        EnemyStamina.fillAmount = CurrentStamin * 0.01f;
    }

    public void StealStaminKickLeg()
    {
        CurrentStamin -= 25;
        EnemyStamina.fillAmount = CurrentStamin * 0.01f;
    }

    public void Dead()
    {
        animator.SetBool("DieEnemy", true);
        Audio[0].Play();

    }

    public void GetStamin()
    {
        if (CurrentStamin < 100)
        {
            time += Time.deltaTime;
            if (time > 0.04)
                time = 0f;
        }
        if (CurrentStamin > 100)
        {
            CurrentStamin = 100;
        }
        else if (CurrentStamin <= 0f)
        {
            CurrentStamin = 0f;
        }
        CurrentStamin += time;
        EnemyStamina.fillAmount = CurrentStamin * 0.01f;
    }
}
