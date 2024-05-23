using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.InventorySystem.Scripts.Data;
using Assets.HeroEditor.InventorySystem.Scripts.Enums;
using UnityEngine;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
    public class MeleeWeapon : MonoBehaviour
    {
        public AnimationEvents AnimationEvents;
        public Transform Edge;
        public LayerMask enemyLayers; // Set this in the inspector to the layers your enemies are on
        public float attackRange = 1f; // Set this to the range of your weapon

        private void Start()
        {
            AnimationEvents.OnCustomEvent += OnAnimationEvent;
        }

        private void OnDestroy()
        {
            AnimationEvents.OnCustomEvent -= OnAnimationEvent;
        }

        private void OnAnimationEvent(string eventName)
        {
            switch (eventName)
            {
                case "Hit":
                    // Detect enemies in range of the attack
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Edge.position, attackRange, enemyLayers);

                    // Damage each enemy hit
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        // Calculate damage based on the properties of the equipped item
                        enemy.GetComponent<Damageable>().Hit(15, new Vector2(0, 0));
                        Debug.Log("We hit " + enemy.name);
                    }
                    break;
                        default: return;
            }
        }

        // Draw the attack range in the editor
        private void OnDrawGizmosSelected()
        {
            if (Edge == null)
                return;

            Gizmos.DrawWireSphere(Edge.position, attackRange);
        }
    }
}