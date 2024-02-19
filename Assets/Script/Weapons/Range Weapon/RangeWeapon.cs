using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField]
    private RangeWeaponDataSO weaponData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponsController WPcontroller = collision.GetComponent<WeaponsController>();
        //Debug.Log("equip able");
        if(WPcontroller != null)
        {
            if(WPcontroller.slot < WPcontroller.maxSlot)
            {
                WPcontroller.equipped(weaponData);
                Destroy(gameObject);
            }
            else if (WPcontroller.slot >= WPcontroller.maxSlot)
            {
                WPcontroller.Drop();
                WPcontroller.equipped(weaponData);
                Destroy(gameObject);
            }
        }
    }
}
