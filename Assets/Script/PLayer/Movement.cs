using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool facingRight = true;
    [SerializeField] protected int direction;
    [SerializeField] protected float speed;
    [SerializeField] protected float angle;
    [SerializeField] protected Vector2 targetTowards;
    [SerializeField] protected Vector2 directionVector;
    public Vector2 DirectionVector { get => directionVector; set => directionVector = value; }

    protected virtual void Update()
    {
        //DirectionCharacter();
    }
    public virtual void Move()
    {
        rb.velocity = directionVector.normalized * speed * 0.01f;
    }
    protected virtual void DirectionCharacter()
    {
        //make each direction have 45
        
        if ((angle > 22 && angle <= 67))
        {
            direction = 0;

        }
        else if (angle > 67 && angle <= 112)
        {
            direction = 1;
        }
        else if (angle > 112 && angle <= 157)
        {
            direction = 2;

        }
        else if (angle > 157 && angle <= 202)
        {
            direction = 3;

        }
        else if (angle > 202 && angle <= 247)
        {
            direction = 4;

        }
        else if (angle > 247 && angle <= 292)
        {
            direction = 5;

        }
        else if (angle > 292 && angle <= 337)
        {
            direction = 6;
        }
        else if (angle > 337 || angle < 22)
        {
            direction = 7;
        }
    }
    public virtual void AngleCalculate(Vector2 targetTowards)
    {
        this.targetTowards = targetTowards;
        if (this.targetTowards != Vector2.zero)
        {
            Debug.Log(name+" "+ this.targetTowards);
            directionVector = (Vector2)this.targetTowards - (Vector2)this.transform.position;
        }
        angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        angle += 180;
    }
}
