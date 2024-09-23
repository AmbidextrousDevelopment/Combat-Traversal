using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackingState : playerBaseState
{
    private Attack attack;

    private bool alreadyAppliedForce; //force for attack animation

    private float previousFrameTime;

    public playerAttackingState(playerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.attacks[attackIndex];
    }

    public override void Enter()
    {      
        stateMachine.AttackDamage.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime); //without that piece of shit animation will not change position
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator/*, "Attack"*/);

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f) 
        {
            if (normalizedTime >= attack.ForceTime)
            {            
                TryApplyForce();
             
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);             
            }
        }
        else
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

        previousFrameTime = normalizedTime;
    }


    private void TryComboAttack(float normalizedTime)
    {
      if (attack.ComboStateIndex == -1) { return; }

      if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new playerAttackingState(stateMachine, attack.ComboStateIndex)); 
        
    }

    private void TryApplyForce() 
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }

    public override void Exit()
    {
       
    }   
}
