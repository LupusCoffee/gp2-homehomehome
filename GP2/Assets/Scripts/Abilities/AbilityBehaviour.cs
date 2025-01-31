//Created by Mohammed

using System;
using System.Collections.Generic;
using UnityEngine;
using static CompactMath;
using static MultiTag;

public class AbilityBehaviour
{
    LayerMask spellLayerMask = 1 << 0;
    public enum SpellType
    { 
        LIGHT, // light something - fungus and enemies
        MIND_CONTROL, // take control of an enemy and move them
        ACTIVATE, // raise objects, move them, open door, special buttons
        PUSH // force push away from player
    }

    public enum SpellTarget
    {
        SELF, // player
        CORRUPTION, // enemies, corruption, mushrooms
        ROOTED_OBJECTS, // moving things like elevators, doors, etc
        PHYSICS_OBJECTS // push things using physics, +artifacts
    }

    public enum SpellProperties
    {
        SINGLE,
        AREA
    }

    public void Activate(SpellType ability, SpellTarget target, SpellProperties properties)
    {
        Debug.Log("Activating ability: " + ability + " on target: " + target + " with properties: " + properties);
        //ability behaviours are implemented here
        switch (ability)
        {   
            case SpellType.LIGHT:

                Spell_Light(target, properties);

                break;

            case SpellType.MIND_CONTROL:

                Spell_MindControl(target, properties);

                break;

            case SpellType.ACTIVATE:

                Spell_Activate(target, properties);

                break;

            case SpellType.PUSH:
                Spell_Push(target, properties);
                break;

            default:
                break;
        }
    }

    #region Ability Behaviours
    private void Spell_Light(SpellTarget target, SpellProperties properties)
    {
        if(properties == SpellProperties.SINGLE)
        {
            GameObject targetObject = GetSingleTarget(target, 2);
            if (targetObject == null)
                return;

            if (targetObject.TryGetComponent(out LightableObject lightobj))
            {
                lightobj.ActivateLight(30);
            }
        }
        else if (properties == SpellProperties.AREA)
        {
            List<GameObject> targetObjects = GetAreaTargets(target, 10);

            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject.TryGetComponent(out LightableObject lightobj))
                {
                    lightobj.ActivateLight(5);
                }
            }
        }
    }

    private void Spell_MindControl(SpellTarget target, SpellProperties effectType)
    {
        if(effectType == SpellProperties.SINGLE)
        {
            GameObject targetObject = GetSingleTarget(target, 2);
            if(targetObject == null)
                return;

            if(targetObject.TryGetComponent(out MindControl mindControl))
            {
                Player.Instance.DisableControls(targetObject);
                mindControl.ActivateMindControl();
            }
        }
        else if (effectType == SpellProperties.AREA)
        {
            // what are you, an neural network? You cant just mind control everything around you.
            //make plaeyr die
        }
    }

    private void Spell_Activate(SpellTarget target, SpellProperties properties)
    {
        if (properties == SpellProperties.SINGLE)
        {
            Debug.Log("Activating single object ACTIVATE spell");
            GameObject targetObject = GetSingleTarget(target, 2);
            if (targetObject == null)
                return;

            if (targetObject.TryGetComponent(out SpellActivatable movableObject)) {
                movableObject.ActivateBySpell();
                Debug.Log("Moved object");
            }
        }
        else if (properties == SpellProperties.AREA)
        {
            List<GameObject> targetObjects = GetAreaTargets(target, 3);
            Debug.Log("did i find any objects? " + targetObjects.Count);

            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject.TryGetComponent(out SpellActivatable movableObject))  {
                    movableObject.ActivateBySpell();
                }
            }
        }
    }

    private void Spell_Push(SpellTarget target, SpellProperties properties)
    {
        if (properties == SpellProperties.SINGLE)
        {
            GameObject targetObject = GetSingleTarget(target, 2);
            if (targetObject == null)
                return;

            if (targetObject.TryGetComponent(out Rigidbody pushableObject))
            {
                pushableObject.AddExplosionForce(200, Player.Instance.transform.position, 10);
            }

            if(targetObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Daze();
            }
        }
        else if (properties == SpellProperties.AREA)
        {
            List<GameObject> targetObjects = GetAreaTargets(target, 7);

            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject.TryGetComponent(out Rigidbody pushableObject))
                {
                    pushableObject.AddExplosionForce(100, Player.Instance.transform.position, 5);
                }

                if (targetObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.Daze();
                }
            }
        }
    }
    #endregion

    #region Ability Implementations
    private MultiTags[] ConvertTypeToMultiTag(SpellTarget target)
    {
        Debug.Log("Converting target to tag: " + target);
        switch (target)
        {
            case SpellTarget.SELF:
                return new MultiTags[] { MultiTags.Player };
            case SpellTarget.CORRUPTION:
                return new MultiTags[] { MultiTags.Corruption };
            case SpellTarget.ROOTED_OBJECTS:
                return new MultiTags[] { MultiTags.Nature, MultiTags.MovableObject };
            case SpellTarget.PHYSICS_OBJECTS:
                return new MultiTags[] { MultiTags.Pushable };
            default:
                return new MultiTags[] { MultiTags.None };
        }
    }

    public GameObject GetSingleTarget(SpellTarget type, float overrideRadius = 1)
    {
        //first check below player if they are standing on something
        Collider[] hitColliders = Physics.OverlapSphere(Player.Instance.transform.position, overrideRadius, spellLayerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            if (HasTag(hitCollider.gameObject, ConvertTypeToMultiTag(type)))
            {
                return hitCollider.gameObject;
            }
        }

        //spherecast forward in player direction
        RaycastHit hitForward;
        if (Physics.SphereCast(Player.Instance.transform.position, overrideRadius, Player.Instance.transform.forward, out hitForward, 10, spellLayerMask))
        {
            if (HasTag(hitForward.collider.gameObject, ConvertTypeToMultiTag(type))) {
                return hitForward.collider.gameObject;
            }
            else
            {
                Debug.Log("No matching tag found");
            }
        }

        return null;
    }

    public GameObject GetSingleTarget(MultiTags tag)
    {
        //first check below player if they are standing on something
        Collider[] hitColliders = Physics.OverlapSphere(Player.Instance.transform.position, 1.25f, spellLayerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            if (HasTag(hitCollider.gameObject, tag))
            {
                return hitCollider.gameObject;
            }
        }

        //spherecast forward in player direction
        RaycastHit hitForward;
        if (Physics.SphereCast(Player.Instance.transform.position, 1, Player.Instance.transform.forward, out hitForward, 10, spellLayerMask))
        {
            if (HasTag(hitForward.collider.gameObject, tag))
            {
                return hitForward.collider.gameObject;
            }
            else
            {
                Debug.Log("No matching tag found");
            }
        }

        return null;
    }

    public List<GameObject> GetAreaTargets(SpellTarget type, float range)
    {
        // get everything in an area
        Collider[] hitColliders = Physics.OverlapSphere(Player.Instance.transform.position, range);

        List<GameObject> targetObjects = new List<GameObject>();

        foreach (Collider hitCollider in hitColliders)
        {
            if (HasTag(hitCollider.gameObject, ConvertTypeToMultiTag(type)))
            {
                targetObjects.Add(hitCollider.gameObject);
            }
        }

        return targetObjects;
    }
    #endregion
}
