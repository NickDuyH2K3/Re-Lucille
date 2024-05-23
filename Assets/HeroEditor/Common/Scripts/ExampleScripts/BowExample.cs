using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
    /// <summary>
    /// Bow shooting behaviour (charge/release bow, create arrow). It's just an example!
    /// </summary>
    public class BowExample : MonoBehaviour
    {
        public Character Character;
        public AnimationClip ClipCharge;
	    public Transform FireTransform;
	    public GameObject ArrowPrefab;
        public bool CreateArrows;

        /// <summary>
        /// Should be set outside (by input manager or AI).
        /// </summary>
        [HideInInspector] public bool ChargeButtonDown;
        [HideInInspector] public bool ChargeButtonUp;

        private float _chargeTime;

        public void Update()
        {
            if (ChargeButtonDown)
            {
                _chargeTime = Time.time;
                Character.Animator.SetInteger("Charge", 1);
            }

            if (ChargeButtonUp)
            {
                var charged = Time.time - _chargeTime > ClipCharge.length;

                Character.Animator.SetInteger("Charge", charged ? 2 : 3);

                if (charged && CreateArrows)
                {
	                CreateArrow();
                }
            }
        }

		private void CreateArrow()
        {
            var arrow = Instantiate(ArrowPrefab, FireTransform);
            var sr = arrow.GetComponent<SpriteRenderer>();
            var rb = arrow.GetComponent<Rigidbody2D>();
            const float speed = 18.75f; // TODO: Change this!
            
            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.identity;
            arrow.transform.SetParent(null);
            sr.sprite = Character.Bow.Single(j => j.name == "Arrow");

            // Determine the direction of the shot
            float direction = Mathf.Sign(Character.transform.lossyScale.x);

            // Flip the arrow's sprite if shooting to the left
            sr.flipX = direction < 0;

            rb.velocity = speed * FireTransform.right * direction * Random.Range(0.85f, 1.15f);

            var characterCollider = Character.GetComponent<Collider2D>();

            if (characterCollider != null)
            {
                Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), characterCollider);
            }

            arrow.gameObject.layer = 9; // TODO: Create layer in your project and disable collision for it (in physics settings)
            Physics2D.IgnoreLayerCollision(9, 9, true); // Disable collision with other projectiles.
        }
	}
}