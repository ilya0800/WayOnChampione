using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceSecondSwordEnemy : MonoBehaviour
{
    [SerializeField] GameObject PosForFallSword;
    [SerializeField] float SpeedFallSword;

    void Update()
    {
        FallSwordInPosition();
    }

    private void FallSwordInPosition()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PosForFallSword.transform.position, SpeedFallSword * Time.deltaTime);
    }
}


