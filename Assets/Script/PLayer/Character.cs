using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[RequireComponent(typeof(CharacterAnimationManager))]
[RequireComponent(typeof(Rigidbody2D))] 
[RequireComponent(typeof(Animator))] 
public class Character : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] protected bool isFrezee;
    [SerializeField] public bool isDeath;
    [SerializeField] protected float frezeeTime;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rigidbodyCharacter;
    [SerializeField] protected CharacterAnimationManager animationManager;
    [SerializeField] protected StatsCharacter stats;
    [SerializeField] protected bool isAbility;

    [Header("Stun")]
    [SerializeField] protected EventHandler OnIsFrezeeChange;
    [SerializeField] protected bool isStun;

    public bool IsFrezee
    {
        get => isFrezee;
        set
        {
            if (isFrezee != value)
            {
                isFrezee = value;
                OnIsFrezeeChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public Rigidbody2D RigidbodyCharacter { get => rigidbodyCharacter;}
    public Animator Animator { get => animator; }
    public CharacterAnimationManager AnimationManager { get => animationManager;}
    public bool IsAbility { get => isAbility; set => isAbility = value; }
    public bool IsDeath { get => isDeath;}

    //public StatsCharacter Stats { get => stats; set => stats = value; }

    protected virtual void SettingCharacter()
    {
        OnIsFrezeeChange += CheckIsStun;
        //OnIsFrezeeChange += CheckIsDeath;
        currentHealth = stats.maxHealth;
    }
    public void IsOnAbility()
    {
        isAbility = true;
    }
    public void IsOffAbility()
    {
        isAbility = false;
    }
    void CheckIsStun(object sender, EventArgs e)
    {
        if (isStun == true)
        {
            isFrezee = true;
        }
    }
    void CheckIsDeath(object sender, EventArgs e)
    {
        if (isDeath == true)
        {
            isFrezee = true;
        }
    }
    protected virtual void LoadCharacter()
    {
        animator = GetComponent<Animator>();
        rigidbodyCharacter = GetComponent<Rigidbody2D>();
        animationManager = GetComponent<CharacterAnimationManager>();
    }
    public virtual void TakeDamage(int damage, GameObject caller)
    {
        if (damage > 0)
        {
            animationManager.Animation_3_Hit();
            KnockBack(caller, damage, knockBackForce);
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {

                rigidbodyCharacter.bodyType = RigidbodyType2D.Static;
                isDeath = true;
                animationManager.Animation_4_Death();
        }
    }
    protected virtual void Die()
    {
        Debug.Log(name + " Death");
        this.gameObject.SetActive(false);
    }
    public virtual void KnockBack(GameObject caller, int damage, float knockBackForce)
    {
        FrezeeRigidbody(frezeeTime);
        Vector2 direction;
        direction = this.transform.position - caller.transform.position;
        direction = direction.normalized;
        //gameObject.GetComponent<Movement>().TargetTowards=direction;
        rigidbodyCharacter.AddForce(direction * knockBackForce);
    }
    public virtual void FrezeeRigidbody(float timeFrezee)
    {
        if (!isFrezee)
        {
            RigidbodyType2D origanal = rigidbodyCharacter.bodyType;
            rigidbodyCharacter.bodyType = RigidbodyType2D.Dynamic;
            isFrezee = true;
            StartCoroutine(WaitFrezee(timeFrezee,origanal));
        }
    }
    protected virtual IEnumerator WaitFrezee(float timeFrezee, RigidbodyType2D origanal)
    {
        yield return new WaitForSeconds(timeFrezee);
        rigidbodyCharacter.bodyType = origanal;
        IsFrezee = false;
    }
    public void Stun(float timeStun)
    {
        isStun = true;
        isFrezee = true;
        StartCoroutine(WaitStun(timeStun));
    }
    protected virtual IEnumerator WaitStun(float timeStun)
    {
        yield return new WaitForSeconds(timeStun);
        isStun = false;
        isFrezee = false;
    }
}