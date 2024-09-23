using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHangingState : playerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("HangingIdle");
   

    private const float CrossFadeDuration = 0.1f;

    private Vector3 ledgeforward;

    public playerHangingState(playerStateMachine stateMachine, Vector3 _ledgeforward) : base(stateMachine)
    {        
        ledgeforward = _ledgeforward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeforward, Vector3.up);

        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
    }

    public override void Exit()
    {
       
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y <0f)  //if button S is pressed
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new playerFallingState(stateMachine));
        }
        if (stateMachine.InputReader.MovementValue.y > 0f)  //if button W is pressed
        {
            
            stateMachine.SwitchState(new playerPullUpState(stateMachine));
        }
    }
}
