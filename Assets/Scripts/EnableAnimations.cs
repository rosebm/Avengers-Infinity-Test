using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

//Starts the Hulk Animation once the Image Target is found, not as soon the app starts
public class EnableAnimations : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour mTrackableBehaviour;

    public Animator animationClips;
    private float translateSpeed = 0.01f;

    void Start(){
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour) 
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus) {

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            // Play  when target is found
            animationClips.enabled = true;


        }

        else {
            // Stop  when target is lost
            animationClips.enabled = false;
        }

    }

    protected virtual void OnTrackingFound() {
        animationClips.enabled = true;
    }

    protected virtual void OnTrackingLost() {
        animationClips.enabled = false;
    }

}
