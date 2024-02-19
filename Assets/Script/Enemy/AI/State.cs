using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
    [SerializeField] protected StateManager stateManager;
After:
    [SerializeField] private StateManager stateManager;
*/
    [SerializeField] protected StateManager stateManager;

    public StateManager StateManager { get => stateManager; }

    protected virtual void LoadState()
    {
        stateManager=GetComponentInParent<StateManager>();
    }
    public abstract State RunCurrentStare();
    public abstract void StartCurrentStare();
    public abstract State CheckState();
}
