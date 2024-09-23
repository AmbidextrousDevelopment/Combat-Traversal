using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");

    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float crossFadeDuration = 0.1f; //speed value to play animation
    private const float animDampTime = 0.1f; //speed value to change animation value

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

       

        stateMachine.Animator.SetFloat(SpeedHash, 0, animDampTime, deltaTime);
    }   

    public override void Exit() { }

    


}
