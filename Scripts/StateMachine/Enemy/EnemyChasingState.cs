using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

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
        
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            //chasing state
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, animDampTime, deltaTime);
    }

    private bool IsInAttackRange()
    {
        if (stateMachine.Player.isDead) { return false; }
        float playerDIstanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDIstanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
}
