using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            Debug.Log("Healing Player");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Damageable damageable = player.GetComponent<Damageable>();
                if (damageable != null)
                {
                    if(damageable.Health == damageable.MaxHealth)
                    {
                        return false;
                    }
                    else
                    {
                        damageable.Heal(amountToChangeStat);
                        return true;
                    }
                }
                else
                {
                    Debug.LogError("Player does not have a Damageable component");
                    return false;
                }
            }
            else
            {
                Debug.LogError("No GameObject found with tag Player");
                return false;
            }
        }
        else if(statToChange == StatToChange.attack)
        {
            Attack[] attacks = GameObject.Find("Player").GetComponentsInChildren<Attack>();
            foreach (Attack attack in attacks)
            {
                attack.attackDamage += amountToChangeStat;
                return true;
            }
            return false;
        }
        return false;
    }



    public enum StatToChange
    {
        none,
        health,
        mana,
        attack
    };
    public enum AttributeToChange
    {
        none,
        strength,
        defense,
        agility
    };
}
