using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "Ability SO/Block Ability")]
public class BlockAbility : AbilitySO
{
    //[SerializeField] LayerMask enemyLayer;
    public override void Activate(GameObject player)
    {
        base.Activate(player);
        //playerClone.GetComponent<Player>().RigidbodyCharacter.bodyType = RigidbodyType2D.Static;
        playerClone.IsBlocking = true;
        Debug.Log("Block");
        playerClone.WeaponMelee.ShieldArea.isTrigger = false;

        //parent.GetComponentInParent<BoxCollider>().enabled = !parent.GetComponentInParent<Player>().isBlocking;
    }
    public override void CastSkill(GameObject player)
    {
        base.CastSkill(player);

        playerClone.Animator.runtimeAnimatorController = animators[(int)playerClone.Animator.GetFloat("Direction")];
        Debug.Log(playerClone.Animator.runtimeAnimatorController);
        playerClone.GetComponent<Player>().AnimationManager.Animation_5_Ability();
        //SetBool 
        //playerClone.Animator.Play("Ability", 0, 0);
    }
    public override void BeginCooldown(GameObject parent)
    {
        base.BeginCooldown(parent);
        playerClone.IsBlocking = false;
        Debug.Log("Un-Block");
        playerClone.WeaponMelee.ShieldArea.isTrigger = true;
        playerClone.GetComponent<Player>().AnimationManager.Animation_5_OfAbility();
    }
    
}
