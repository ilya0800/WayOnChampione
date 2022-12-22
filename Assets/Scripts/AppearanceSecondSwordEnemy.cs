using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceSecondSwordEnemy : MonoBehaviour
{
    Animation animation;
    Animator animator;
    [SerializeField] GameObject PosForFallSword;
    [SerializeField] float SpeedFallSword;

    void Start()
    {
        animation = GetComponent<Animation>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FallSwordInPosition();
    }

    private void FallSwordInPosition()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PosForFallSword.transform.position, SpeedFallSword * Time.deltaTime);
    }
}


