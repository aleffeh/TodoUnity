using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DialogStateMachine : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsHidden = Animator.StringToHash("isHidden");

    public void ChangeState(bool hide)
    {
        gameObject.SetActive(true);
        if (_animator != null)
            _animator.SetBool(IsHidden, hide);
        else
        {
            _animator = GetComponent<Animator>();
            ChangeState(hide);
        }
    }
}