using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
    enum State { Idle, Run, Flip, Warzone}

    [SerializeField] PlayerAnimator PlayerAnimator;
    [SerializeField] PlayerIk playerIk;

    [SerializeField] float MoveSpeed;    
    [SerializeField] float slowMoScale;
    [SerializeField] float WarzoneTimer;
    private State state;
    private Warzone currentWarzone;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        state = State.Idle;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRunning();
        }
        ManageState();
    }

    void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Run:
                Move();
                break;
            case State.Flip:
                break;
            case State.Warzone:
                ManageWarzoneState();
                break;
        }
    }
    void StartRunning()
    {
        state = State.Run;
        PlayerAnimator.PlayRunAnimation();
    }
    void Move()
    {
        transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
    }
    public void EnteredWarzoneCallback(Warzone warzone)
    {
        if (currentWarzone != null)
        {
            return;
        }
        state = State.Warzone;
        currentWarzone = warzone;

        currentWarzone.StartAnimatingIKTarget();

        WarzoneTimer = 0;

        PlayerAnimator.Play(currentWarzone.GetAnimationToPlay(), currentWarzone.GetAnimatorSpeed());

        Time.timeScale = slowMoScale;

        playerIk.ConfigureIK(currentWarzone.GetIKTarget());

        PlayerAnimator.Play(currentWarzone.GetAnimationToPlay());

        Debug.Log("Entered Warzone !");
    } 
    void ManageWarzoneState()
    {
        WarzoneTimer += Time.deltaTime;
        float splinePercentage = WarzoneTimer / currentWarzone.GetDuration();
        transform.position = currentWarzone.GetPlayerSpline().EvaluatePosition(splinePercentage);

        if (splinePercentage >= 1)
        {
            ExitWarzone();
        }
    }
    void ExitWarzone()
    {
        state = State.Run;
        currentWarzone = null;
        PlayerAnimator.Play("Run", 1);
        Time.timeScale = 1;
        playerIk.DisableIK();
    }
}
