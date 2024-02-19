using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyMeleeAttack : EnemyAttack
{
    [SerializeField] private CircleCollider2D attackPoint;
    protected override void Awake()
    {
        base.Awake();
        attackPoint = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        attackPoint.radius = stateManager.EnemyCtrl.EnemySO.radiusAttack;
        attackPoint.offset = new Vector2(stateManager.EnemyCtrl.EnemySO.attackRange, 0);
        attackPoint.isTrigger = true;
    }
    protected override void Attacking()
    {
        Collider2D hitEnemies = Physics2D.OverlapCircle((Vector2)this.transform.position + attackPointVector * attackRange / 2, attackRange / 2, playerMask);
        if (hitEnemies.GetComponent<Player>() != null)
        {
            hitEnemies.GetComponent<Player>().TakeDamage(attackDamage, gameObject);
        }
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere((Vector2)this.transform.position + attackPointVector * attackRange / 2, attackRange / 2);
    }
}
