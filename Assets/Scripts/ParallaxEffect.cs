using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    
    //Starting position for the parallax game object
    Vector2 startingPosition;

    //Start Z value of the parallax game object
    float startingZ;

    Vector2 camMoveSinceStart =>(Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));


    //The further the object from the player , the faster the ParallaxEffect object will move.
    // It's will slower when we drag the z value closer to player
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget)/ clippingPlane;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
