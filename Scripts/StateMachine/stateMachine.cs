using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class stateMachine : MonoBehaviour
{

    private State currentState; // what state is now
    
    
    private void Update()
    {      
       currentState?.Tick(Time.deltaTime); //same as "if (currentState !=null)"        
    }

    public void SwitchState(State newState) //to change state
    {
        currentState?.Exit(); 
        currentState = newState;
        currentState?.Enter(); //? because could be null
    }


}
