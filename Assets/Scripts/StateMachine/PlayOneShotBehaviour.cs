using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float sfxVolume;
    public AudioMixer mainMixer;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    //delay timer
    public float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playOnEnter)
       {
            PlaySound(animator.gameObject.transform.position);
       }
       timeSinceEntered = 0;
       hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playAfterDelay && !hasDelayedSoundPlayed)
       {
            timeSinceEntered += Time.deltaTime;
            if(timeSinceEntered > playDelay)
            {
                PlaySound(animator.gameObject.transform.position);
                hasDelayedSoundPlayed = true;
            }
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playOnExit)
       {
            PlaySound(animator.gameObject.transform.position);
       }
    }

    private void PlaySound(Vector3 position)
   {
    GameObject audioObject = new("SFX");
    audioObject.transform.position = position;
    AudioSource audioSource = audioObject.AddComponent<AudioSource>();
    audioSource.clip = soundToPlay;
    
    mainMixer.GetFloat("sfxVolume", out sfxVolume);
    audioSource.volume = Mathf.Pow(10, sfxVolume / 20);
    audioSource.Play();
    Destroy(audioObject, soundToPlay.length);
   }
}