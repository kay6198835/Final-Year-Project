using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AbilityHolder))]
public class WeaponMelee : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private WeaponMeleeStats stats;
    [SerializeField] private Vector2 shieldOffset;
    [SerializeField] private Vector2 shieldSize;
    [SerializeField] private BoxCollider2D shieldArea;
    [SerializeField] private AbilityHolder abilityHolder;
    //[SerializeField] private PlayerCombat playerCombat;
    public WeaponMeleeStats Stats { get => stats;}
    public Vector2 ShieldOffset { get => shieldOffset;}
    public Vector2 ShieldSize { get => shieldSize;}
    public BoxCollider2D ShieldArea { get => shieldArea; set => shieldArea = value; }
    public AbilityHolder AbilityHolder { get => abilityHolder; }

    protected virtual void LoadWeaponMelee()
    {
        abilityHolder = GetComponent<AbilityHolder>();
        shieldArea = GetComponent<BoxCollider2D>();
    }
    public virtual void AttackAbility(GameObject player, KeyCode keyCode)
    {
        abilityHolder.ability = player.GetComponent<PlayerCombat>().AttackState.ability;
        abilityHolder.ActivateAbility(player,keyCode);
    }
    public virtual void AuxiliaryAbility(GameObject player, KeyCode keyCode)
    {
        abilityHolder.ability = stats.auxiliaryAbility;
        abilityHolder.ActivateAbility(player,keyCode);
    }
    public virtual void SpecialAbility(GameObject player, KeyCode keyCode)
    {
        abilityHolder.ability = stats.specialAbility;
        abilityHolder.ActivateAbility(player, keyCode);
    }
    public virtual void SettingWeaponMelee()
    {
        shieldArea.offset = shieldOffset;
        shieldArea.size = shieldSize;
        shieldArea.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponsController WPcontroller = collision.GetComponent<WeaponsController>();
        Debug.Log("equip able");
        if (WPcontroller != null)
        {
            if (WPcontroller.slot < WPcontroller.maxSlot)
            {
                WPcontroller.equipped(stats);
                Destroy(gameObject);
            }
            else if (WPcontroller.slot >= WPcontroller.maxSlot)
            {
                WPcontroller.Drop();
                WPcontroller.equipped(stats);
                Destroy(gameObject);
            }
        }
    }
}