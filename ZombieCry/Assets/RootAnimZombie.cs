using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAnimZombie : StateMachineBehaviour
{
    public string varName;
    public bool status;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(varName, status);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(varName, !status);

    }

}
