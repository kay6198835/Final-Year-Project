using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveState : State
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private MovementEnemy movement;

    [SerializeField] private int moveTime;
    [SerializeField] private float endMoveTime;
    [SerializeField] private float currentTime;
    [SerializeField] private bool isMove;

    [SerializeField] private State state;

    private void Awake()
    {
        base.LoadState();
        movement = stateManager.GetComponent<MovementEnemy>();
        idleState = this.transform.parent.GetComponentInChildren<IdleState>();
    }
    public override void StartCurrentStare()
    {
        state = this;
        currentTime =Time.time;
        endMoveTime = Time.time+ stateManager.EnemyCtrl.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 10;
        movement.DirectionVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        movement.AngleCalculate(Vector2.zero);
        //Debug.Log(movement.DirectionVector);
    }
    public override State RunCurrentStare()
    {
        stateManager.EnemyCtrl.AnimationManager.Animation_2_Run();

        movement.Move();
        if (stateManager.IsChase)
        {
            currentTime += 0.02f;
        }
        return CheckState();
    }
    private void RandomPositon(GameObject player)
    {
        Vector2 randomVector = Random.onUnitSphere;
        movement.DirectionVector = randomVector.normalized * 1;
    }
    public override State CheckState()
    {
        if (currentTime >=endMoveTime )
        {
            movement.DirectionVector = Vector2.zero;
            state = idleState;
            //Debug.Log(name);
        }
        return state;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCurrentStare();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Turn");
        movement.DirectionVector = -movement.DirectionVector;
    }
}
