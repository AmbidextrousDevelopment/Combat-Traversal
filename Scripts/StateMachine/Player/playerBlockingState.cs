using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlockingState : playerBaseState
{
    private readonly int ShieldBlockHash = Animator.StringToHash("Shield Block");

    public playerBlockingState(playerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Health.IsInvunerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(ShieldBlockHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new playerTargetingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new playerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.IsInvunerable(false);
    }

   
}
