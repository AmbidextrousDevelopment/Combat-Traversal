using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private Attack attack;

    private readonly int HandAttackHash = Animator.StringToHash("HandAttack");

    public EnemyAttackingState(EnemyStateMachine stateMachine/*, int attackIndex*/) : base(stateMachine)
    {
       // attack = stateMachine.attacks[attackIndex];
    }

    public override void Enter()
    {
        FacePlayer();
        // stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback/*attack.Damage*/); //in future change to values from attack class
        stateMachine.Animator.CrossFadeInFixedTime(HandAttackHash, 0.1f/*attack.TransitionDuration*/);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
       
    }

    public override void Exit()
    {
        
    }

    
}
