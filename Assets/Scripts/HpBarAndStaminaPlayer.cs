using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarAndStaminaPlayer : MonoBehaviour, IHpBarAndStaminaForPlayer
{
    public static HpBarAndStaminaPlayer instance = null;
    private float time = 0f;
    [Header("Hp")]
    public float Hp = 100;
    [SerializeField] Image FullHp;
    [Header("Stamina")]
    [SerializeField] Image PlayerStamina;
    [SerializeField] float FullStamina = 100; 
    private float CurrentStamina;
    public bool PermissionUseStamin;
    private Animator animator;
    [SerializeField] AudioSource Audio;
    [SerializeField] GameObject PanelDead;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance = this)
        {
            Destroy(gameObject);
        }
        CurrentStamina = FullStamina;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetStamin();
        CheckStamina();
        CheckHp();
    }

    public void CheckHp()
    {
        if (Hp <= 0)
           Dead();
        
    }

    public void CheckStamina()
    {
        if (CurrentStamina > 10 || CurrentStamina == 10)
            PermissionUseStamin = true;

        if (CurrentStamina < 20)
            PermissionUseStamin = false;    
    }

    public void HpDamageDefultAttack()
    {
        Hp -= 20;
        FullHp.fillAmount = Hp * 0.01f;
    }

    public void HpDamageSecondAttack()
    {
        Hp -= 40;
        FullHp.fillAmount = Hp * 0.01f;
    }

    public void HpDamageOfKickLeg()
    {
        Hp -= 10;
        FullHp.fillAmount = Hp * 0.01f;
    }

    public void StealStaminDefoultAttack()
    {
        CurrentStamina -= 20;
        PlayerStamina.fillAmount = CurrentStamina * 0.01f;
    }

    public void StealStaminActivShelder()
    {
        CurrentStamina -= 10;
        PlayerStamina.fillAmount = CurrentStamina * 0.01f;
    }

    public void Dead()
    {
        animator.SetBool("DiePlayer", true);
        Audio.Play();
        PanelDead.SetActive(true);
    }

    public void GetStamin()
    {
        if (CurrentStamina < 100)
        {
            time += Time.deltaTime;
            if (time > 0.02f)
            {
                time = 0;
            }
        }
        else if (CurrentStamina > 100)
        {
            CurrentStamina = 100;
        }
        else if (CurrentStamina <= 0f)
        {
            CurrentStamina = 0f;
        }
        CurrentStamina += time;
        PlayerStamina.fillAmount = CurrentStamina * 0.01f;  
    }
}
