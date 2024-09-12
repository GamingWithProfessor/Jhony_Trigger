using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Warzone : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] SplineContainer playerSpline;
    [SerializeField] Transform ikTarget;
    [SerializeField] SplineAnimate ikSplineAnimate;

    [Header("Settings")]
    [SerializeField] float duration;
    [SerializeField] float animatorSpeed;
    [SerializeField] string animationToPlay;

    // Start is called before the first frame update
    void Start()
    {
        ikSplineAnimate.Duration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimatingIKTarget()
    {
        ikSplineAnimate.Play();
    }

    public Spline GetPlayerSpline()
    {
        return playerSpline.Spline;
    }

    public float GetDuration()
    {
        return duration;
    }

    public float GetAnimatorSpeed()
    {
        return animatorSpeed;
    }

    public string GetAnimationToPlay() 
    { 
        return animationToPlay; 
    }

    public Transform GetIKTarget()
    {
        return ikTarget;
    }
}
