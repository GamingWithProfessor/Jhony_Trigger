using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
    enum State { Idle, Run, Flip, Warzone}

    [SerializeField] PlayerAnimator PlayerAnimator;
    [SerializeField] float MoveSpeed = 5;
    private State state;
    private Warzone currentWarzone;
    [SerializeField] float WarzoneTimer;

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
        WarzoneTimer = 0;

        Debug.Log("Entered Warzone !");
    } 
    void ManageWarzoneState()
    {
        WarzoneTimer += Time.deltaTime;
        float splinePercentage = WarzoneTimer / 2;
        transform.position = currentWarzone.GetPlayerSpline().EvaluatePosition(splinePercentage);
    }
}
