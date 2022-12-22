using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHpBarAndStaminaForEnemy
{      
    void HpDamageOfDefultAttack();
    void StealStaminDefoultAttack();
    void StealStaminActivShelder();
    void CheckStamina();
    void CheckHp();
    void Dead();
    void GetStamin();
    
}
