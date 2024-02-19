using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static UnityEngine.GraphicsBuffer;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] protected float lastClickedTime;
    [SerializeField] protected float lastComboEnd;
    [SerializeField] protected int comboCounter;
    [SerializeField] protected float timeBetweenCombos = 0.5f;

    [SerializeField] protected Player player;
    [SerializeField] protected WeaponMelee weaponMelee;
    [SerializeField] protected Animator animator;
    [SerializeField] protected KeyCode keyCode;
    [SerializeField] protected Vector2 attackPosition;
    [SerializeField] protected AttackSO attackState;
    public AttackSO AttackState { get => attackState; }
    [Range(0f,2f)]
    [SerializeField] protected float speedAnimation;
    public WeaponMelee WeaponMelee { get => weaponMelee;}
    public Animator Animator { get => animator; set => animator = value; }
    public Player Player { get => player; set => player = value; }

    private void Awake()
    {
        //attackStates = weaponMelee.Stats.attackState;
        attackState = weaponMelee.Stats.attackState[0];
    }
    private void Start()
    {
        //animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (player.IsDeath)
        {
            return;
        }
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotationAttack();
        PositionAttack();
        //
        //Combat();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyCode = KeyCode.Space;
            SpecicalAttack();
        }
        if (Input.GetMouseButtonDown(0))
        {
            keyCode = KeyCode.Mouse0;
            Combat();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            keyCode = KeyCode.Mouse1;
            Auxiliary();
        }
        //Combat();
        ExitAttack();
    }

    private void RotationAttack()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void PositionAttack()
    {
        Vector2 mousePosition = transform.gameObject.GetComponentInParent<PlayerMovement>().MousePosition;
        Vector2 direction = mousePosition - (Vector2)transform.parent.position;
        attackPosition = (Vector2)transform.parent.position + direction.normalized * attackState.distanceAttackPosition / 2;
    }
    void Combat()
    {
        attackState = weaponMelee.Stats.attackState[comboCounter];
        if (Time.time - lastComboEnd > 2f)
        {
            CancelInvoke("EndCombo");
            if (Time.time - lastClickedTime >= 0.9f)
            {
                animator.speed = speedAnimation;
                // test
                if (attackState.directionAttackAnimatorOV != null)
                {
                    PositionAttack();
                    animator.runtimeAnimatorController = attackState.directionAttackAnimatorOV[(int)animator.GetFloat("Direction")];
                    //Play Animation 
                    animator.Play("Attack", 0, 0);
                }
                //Do attack
                PlayerAttack();
                if (attackState.ability != null)
                {
                    weaponMelee.AttackAbility(Player.gameObject, keyCode);
                }
                //Set up wait time
                comboCounter++;
                lastClickedTime = Time.time;


                if (comboCounter + 1 > weaponMelee.Stats.attackState.Count)
                {
                    Invoke("EndCombo", 0f);
                    lastComboEnd = Time.time;
                }
            }
        }
    }
    void PlayerAttack()
    {
        //Debug.Log("Player Attacked");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackState.attackRange / 2, attackState.enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(attackState.attackDamege + " Player Attacked " + enemy.name);
            if (enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().TakeDamage(attackState.attackDamege, gameObject);
        }
    }
    void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 0.5f);
            //isAttacking = false;
            //animator.SetBool("IsAttacking", false);
        }
    }
    void EndCombo()
    {
        comboCounter = 0;
    }
    void Auxiliary()
    {
        if (weaponMelee.Stats.auxiliaryAbility != null)
        {
            weaponMelee.AuxiliaryAbility(Player.gameObject, keyCode);
        }
    }
    void SpecicalAttack()
    {
        if (weaponMelee.Stats.specialAbility != null)
        {
            weaponMelee.SpecialAbility(Player.gameObject, keyCode);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackState)
        {
            Gizmos.DrawWireSphere(attackPosition, attackState.attackRange / 2);
        }
    }
}
