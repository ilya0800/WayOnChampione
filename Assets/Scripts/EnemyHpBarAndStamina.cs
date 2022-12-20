using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBarAndStamina : MonoBehaviour, IHpBarAndStamina
{
    public static EnemyHpBarAndStamina instance = null;
    float time = 0f;
    [Header("Hp")]
    float CurrentHp;
    public float Hp = 100;
    [SerializeField] Image FullHpEnemy;
    [Header("Stamina")]
    [SerializeField] Image EnemyStamina;
    float FullStamina = 100;
    private float CurrentStamin;
    public bool PermissionUseStamin = true;

    private void Start()
    {
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
      //  Debug.Log(CurrentStamin);
    }

    public void CheckHp()
    {
        if (Hp <= 0)
        {
            Dead();
        }
    }

    public void CheckStamina()
    {
        if (CurrentStamin < 20)
            PermissionUseStamin = false;
        else if(CurrentStamin > 21)
            PermissionUseStamin = true;
    }

    public void HpDamage()
    {
        Hp -= 10;
        FullHpEnemy.fillAmount = Hp * 0.01f;
    }

    public void StealStaminDefoultAttack()
    {
        CurrentStamin -= 20;
        EnemyStamina.fillAmount = FullStamina * 0.01f;
    }
    public void Dead()
    {

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

    public void StealStaminActivShelder()
    {
        FullStamina -= 10;
        EnemyStamina.fillAmount = CurrentStamin * 0.01f;
    }
}
