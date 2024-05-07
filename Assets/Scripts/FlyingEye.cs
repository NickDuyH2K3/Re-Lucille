using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    public Collider2D deathCollider;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    Transform nextWaypoint;
    int waypointNumber = 0;
    public bool _hasTarget = false;
    public float waypointReachedDistance = 0.1f;

    public bool HasTarget { 
        get{ return _hasTarget; }
        
        private set{
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } 
    }
    public bool CanMove { 
        get{ 
            return animator.GetBool(AnimationStrings.canMove); 
           } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    void Start()
    {
        nextWaypoint = waypoints[waypointNumber];
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            } else{
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        //fly to new waypoint
        Vector2 dirtectionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        //check if we reached the waypoint
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = dirtectionToWaypoint * flightSpeed;
        UpdateDirection();
        //if we need to switch waypoints
        if(distance <= waypointReachedDistance)
        {
            waypointNumber++;

            if(waypointNumber >= waypoints.Count){
                //back to original waypoint
                waypointNumber = 0;
            }
            nextWaypoint = waypoints[waypointNumber];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else {
            if(rb.velocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
    public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
