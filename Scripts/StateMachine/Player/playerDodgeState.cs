using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDodgeState : playerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRIght");

    private const float CrossFadeDuration = 0.1f;

    private Vector3 _dodgingDirectionInput;

    private float remainingDodgeTime;

    public playerDodgeState(playerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        _dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DodgeForwardHash, _dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRightHash, _dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

        // stateMachine.Health.IsInvunerable(true); //if dodge should give invincibility
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * _dodgingDirectionInput.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * _dodgingDirectionInput.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;

        Move(movement, deltaTime);

        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new playerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
       
    }  
}
