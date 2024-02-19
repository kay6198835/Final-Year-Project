using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private WeaponMelee weaponMelee;
    [SerializeField] private StatsBar healthBar;
    [SerializeField] private StatsBar manaBar;
    [SerializeField] private float manaCurrent;
    [SerializeField] private bool isBlocking;

    public float ManaCurrent { get => manaCurrent; set => manaCurrent = value; }
    public StatsBar ManaBar { get => manaBar; set => manaBar = value; }
    public WeaponMelee WeaponMelee { get => weaponMelee; set => weaponMelee = value; }
    public bool IsBlocking { get => isBlocking; set => isBlocking = value; }

    private void Awake()
    {
        LoadCharacter();
    }
    protected override void LoadCharacter()
    {
        base.LoadCharacter();

        weaponMelee = GetComponentInChildren<WeaponMelee>();
        healthBar = GameObject.Find("HealthBar").GetComponent<StatsBar>();
        manaBar = GameObject.Find("ManaBar").GetComponent<StatsBar>();
    }
    private void Start()
    {
        SettingCharacter();
    }
    public override void TakeDamage(int damage, GameObject caller)
    {
        if (isBlocking && weaponMelee.ShieldArea.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Debug.Log("Block");
            damage -= weaponMelee.Stats.blockDamage;
        }
        base.TakeDamage(damage, caller);
        healthBar.SetValue(currentHealth);
    }
    protected override void SettingCharacter()
    {
        base.SettingCharacter();
        knockBackForce = 300f;
        manaCurrent = stats.maxMana;
        healthBar.SetMaxValue(currentHealth);
    }

    public void SetManaCurrent(float manaCurrent)
    {
        manaCurrent = this.manaCurrent;
    }
}
