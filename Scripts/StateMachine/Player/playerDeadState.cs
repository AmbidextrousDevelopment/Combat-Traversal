using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeadState : playerBaseState
{
    public playerDeadState(playerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.AttackDamage.gameObject.SetActive(false);
    }

    public override void Exit()
    {

    }

    public override void Tick(float deltaTime)
    {

    }
}
