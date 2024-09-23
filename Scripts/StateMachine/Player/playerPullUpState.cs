using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPullUpState : playerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("PullUp");

    private readonly Vector3 OffSet = new Vector3(0f, 2.325f, 0.65f);
    public playerPullUpState(playerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, stateMachine.CrossFadeDuration);
    }

    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) { return; } //basically return until animation is done

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(OffSet, Space.Self);
        stateMachine.Controller.enabled = false;

        stateMachine.SwitchState(new playerFreeLookState(stateMachine));
    }
}
