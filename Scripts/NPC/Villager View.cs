using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VillagerView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkAnim()
    {
        _animator.SetBool("Walk", true);
    }
    public void SetIdleAnim()
    {
        _animator.SetBool("Walk", false);
    }
}
