using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStateMachine : stateMachine
{
    //didn't work before because there was update
    [field: SerializeField] public InputReader InputReader { get; private set; } //you can get it, but set only privatly //unity doesn't serialize fields without []
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage AttackDamage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeDistance { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    
    [field: SerializeField] public Attack[] attacks { get; private set; } //array, because there can be a lot of attacks

    [HideInInspector] public float CrossFadeDuration { get; private set; } = 0.1f; //for all crossfade animations

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity; //NegativeInfinity to make first dodge possible
    public Transform MainCameraTransform { get; private set; }
    

    private void Start() 
    {
        MainCameraTransform = Camera.main.transform;
      
        SwitchState(new playerFreeLookState(this)); 
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }


    private void HandleTakeDamage()
    {
        SwitchState(new playerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new playerDeadState(this));
    }
}
