using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
    /// <summary>
    /// General behaviour for projectiles: bullets, rockets and other.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public List<Renderer> Renderers;
        public GameObject Trail;
        public GameObject Impact;
	    public Rigidbody2D Rigidbody;
        [SerializeField]
        private int attackDamageArrow = 10;
        public Vector2 knockback = new Vector2(0,0);

        public void Start()
        {
            Destroy(gameObject, 5);
        }

	    public void Update()
	    {
		    if (Rigidbody != null && Rigidbody.gravityScale > 0)
		    {
			    transform.right = Rigidbody.velocity.normalized;
		    }
	    }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Bang(other.gameObject);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Bang(other.gameObject);
        }

        private void Bang(GameObject other)
        {
            ReplaceImpactSound(other);
            Impact.SetActive(true);
            if (other.CompareTag("Enemy"))
            {
                Damageable enemy = other.GetComponent<Damageable>();
                if (enemy != null)
                {
                    Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                    // If it is an enemy, deal damage to it
                    enemy.Hit(attackDamageArrow, deliveredKnockback);
                }
            }
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 1);

            foreach (var ps in Trail.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }

	        foreach (var tr in Trail.GetComponentsInChildren<TrailRenderer>())
	        {
		        tr.enabled = false;
			}

		}

        private void ReplaceImpactSound(GameObject other)
        {
            var sound = other.GetComponent<AudioSource>();

            if (sound != null && sound.clip != null)
            {
                Impact.GetComponent<AudioSource>().clip = sound.clip;
            }
        }
    }
}