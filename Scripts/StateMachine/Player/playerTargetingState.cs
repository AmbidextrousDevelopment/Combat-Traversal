using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTargetingState : playerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward"); 
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRIght");

    private const float CrossFadeDuration = 0.1f;

    public playerTargetingState(playerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration); //plays Targeting BlendTree
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new playerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new playerBlockingState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new playerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime); //deltatime to do it smoothly

        FaceTarget(); //from playerBaseState
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new playerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero) { return; }
        stateMachine.SwitchState(new playerDodgeState(stateMachine, stateMachine.InputReader.MovementValue));
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new playerJumpingState(stateMachine));
    }


    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0) //forward, back
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime); //0.1 for smooth, deltatime idk for what
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f; //if y > 0 then 1f, else -1f
            stateMachine.Animator.SetFloat(TargetingForwardHash, value); //value from previous line
        }

        if (stateMachine.InputReader.MovementValue.x == 0) //left, right
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime); 
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f; //if y > 0 then 1f, else -1f
            stateMachine.Animator.SetFloat(TargetingRightHash, value); //value from previous line
        }

    }

}
