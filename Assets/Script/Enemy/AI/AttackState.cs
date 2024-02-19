using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private CheckIsInRange checkInRangeAttack;

    private void Awake()
    {
        LoadState();
        chaseState = this.transform.parent.GetComponentInChildren<ChaseState>();
        checkInRangeAttack = stateManager.CheckInRange;
        enemyAttack = stateManager.EnemyAttack;
    }
    private void Start()
    {
        StartCurrentStare();
    }

    public override void StartCurrentStare()
    {
        checkInRangeAttack.TargetMask = stateManager.Stats.layerMask;
        checkInRangeAttack.Range = stateManager.Stats.attackRange;

        enemyAttack.AttackRange = stateManager.Stats.attackRange;
        enemyAttack.PlayerMask = stateManager.Stats.layerMask;
        enemyAttack.AttackDamage = stateManager.Stats.damage;
        enemyAttack.AttackRate = stateManager.Stats.rateAttack;
    }
    public override State RunCurrentStare()
    {
        enemyAttack.Attack();
        if (stateManager.IsAttack)
        {
            stateManager.IsAttack = false;
        }
        checkInRangeAttack.Check();
        stateManager.IsInAttackRange = checkInRangeAttack.IsInRange;
        return CheckState();
    }
    public override State CheckState()
    {
        //stateManager.IsInAttackRange=false;
        if(stateManager.IsInAttackRange) 
        {
            return this;
        }
        else
        {
            return chaseState;
        }
        
    }
}
