using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class playerBaseState : State
{
    protected playerStateMachine stateMachine;

    public playerBaseState(playerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        //new PlayerJumpingState(stateMachine);
    }

    protected void Move(Vector3 motion, float deltaTime) //move fully with controls
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime) //move with dash/dodge
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new playerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new playerFreeLookState(stateMachine));
        }
    }

}
