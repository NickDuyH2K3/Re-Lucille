using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Area : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider;


    void OnTriggerEnter2D(Collider2D collider)
    {
        Damageable damageable = collider.GetComponent<Damageable>();
        if(damageable)
        {
            Vector2 killKnocback = new Vector2(0,1);
            damageable.Hit(100000000,killKnocback);
        }
    }
}
