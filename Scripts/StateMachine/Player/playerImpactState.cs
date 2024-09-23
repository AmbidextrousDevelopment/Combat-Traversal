using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerImpactState : playerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Hit Impact");

    private const float CrossFadeDuration = 0.1f;

    private float duration = 1f;

    public playerImpactState(playerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

   
}
