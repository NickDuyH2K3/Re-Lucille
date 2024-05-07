using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    Collider2D attackCollider;
    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the target is damageable/hitable
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //hit the target
           bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
           if(gotHit)
                Debug.Log("Hit: " + collision.gameObject.name+" for "+attackDamage+" damage");
        }
    }
}
