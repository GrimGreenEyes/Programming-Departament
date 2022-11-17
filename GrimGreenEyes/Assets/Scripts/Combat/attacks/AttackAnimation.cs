using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Animate(int animation)
    {

        animator.SetInteger("Attack", animation);
    }
    public void Destroy()
    {
        GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
        Destroy(gameObject);
    }
}
