using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{

    #region Public Field
    /// <summary>
    /// Set up the Agent State.
    /// </summary>
    public enum State
    {
        Wander,
        Idle,
        Walk
    }
    
    [Tooltip("All the visit positions")]
    public Transform[] visitingPos;
    [Tooltip("Get the reference of the agent's position")]
    public Transform agentTransform;
    [Tooltip("The wander range of the agent")]
    public float wanderScope = 15;
    [Tooltip("The current state of the agent")]
    public State currentState;

    public float wanderTime = 3;
    public float idleTime = 3;
    public float waitingTime = 7;
    
    public bool isWalk
    {
        get { return _isWalk; }
    }
    
    #endregion

    #region Private Field
    /// <summary>
    /// The agent for Nav mesh.
    /// </summary>
    private UnityEngine.AI.NavMeshAgent _agent;
    
    [SerializeField]
    private float _stateTime;
    
    private float _waitingTime;

    private bool _isWalk = false;

    //private List<Transform> _visitingPos = new List<Transform>();

    private int _posIndex;
    #endregion

    #region Init

    private List<Vector3> mySnappointsBottom;
    private List<Vector3> mySnappointsUp;
    private List<Vector3> snapPointsCurrent;

    public void RegisterSnapPoints(Vector3[] snappointsBottom, Vector3[] snappointsUP)
    {
        this.mySnappointsBottom = new List<Vector3>(snappointsBottom.Length);//];
        for (int i = 0; i < snappointsBottom.Length; i++)
        {
            this.mySnappointsBottom.Add(snappointsBottom[i]);
        }

        this.mySnappointsUp = new List<Vector3>(snappointsUP.Length);// /;/];
        for (int i = 0; i < snappointsUP.Length; i++)
        {
            this.mySnappointsUp.Add(snappointsUP[i]);
        }

        // Start with bottom snap point
        snapPointsCurrent = mySnappointsBottom;

    }

    //-------------------------------------------------------------------------
    /// <summary>
    /// Get component of everything.
    /// </summary>
    private void OnEnable()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _stateTime = 0;
        //_visitingPos.AddRange(visitingPos);
        
    }
    //-------------------------------------------------------------------------
    #endregion

    #region State Management
    //-------------------------------------------------------------------------
    /// <summary>
    /// Update the statement of the agent.
    /// </summary>
    private void FixedUpdate()
    {
        StateSwitch();
    }

    //ADDED
    void SetState(State s)
    {
        currentState = s;

        switch (currentState)
        {
            case State.Idle:
                //_visitor.destination = visitorTransform.position;
                _agent.destination = agentTransform.position;
                break;


            case State.Wander:
                Vector3 randomRange = new Vector3(
                (UnityEngine.Random.value - 0.5f) * 2 * wanderScope, 0,
                (UnityEngine.Random.value - 0.5f) * 2 * wanderScope);

                Vector3 nextDestination = agentTransform.position + randomRange;
                _agent.destination = nextDestination;
                break;


            case State.Walk:
                //UpdateChaseState();
                //_visitor.destination = agentTransform.position;


                // THE POINT HAS TO BE ON THE NAVMESH

                Vector3 result = snapPointsCurrent[_posIndex]; // NOT GOOD
                NavMeshHit hit;
                if (NavMesh.SamplePosition(snapPointsCurrent[_posIndex], out hit, 2.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    //return true;
                }// else were in trouble

                _agent.destination = result;


                break;
        }
    }

    //-------------------------------------------------------------------------

    /// <summary>
    /// State swith.
    /// </summary>
    void StateSwitch()
    {
        switch (currentState)
        {
            case State.Idle:
                UpdateIdleState();
                break;
            case State.Wander:
                UpdateWanderState();
                break;
            case State.Walk:
                UpdateWalkState();
                break;
        }
    }

    /// <summary>
    /// Updates the state of the wander.
    /// </summary>
    void UpdateWanderState()
    {
        //Vector3 randomRange = new Vector3 ( 
        //(UnityEngine.Random.value - 0.5f) * 2 * wanderScope, 0, 
        //(UnityEngine.Random.value - 0.5f) * 2 * wanderScope);
        
        //Vector3 nextDestination = agentTransform.position + randomRange;
        //_agent.destination = nextDestination;

        _stateTime += Time.deltaTime;
        _waitingTime += Time.deltaTime;
        
        if (_stateTime >= wanderTime)
        {
            _stateTime = 0;
            Debug.Log("Idle");
            //currentState = State.Idle;
            SetState(State.Idle);
            return;
        }
        
        if (_waitingTime >= waitingTime)
        {
            _stateTime = 0;
            _waitingTime = 0;
            SelectNextPos();
        }
    }

    /// <summary>
    /// Updates the state of the idle.
    /// </summary>
    void UpdateIdleState()
    {
        //_agent.destination = agentTransform.position;
        _stateTime += Time.deltaTime;
        _waitingTime += Time.deltaTime;
        
        if (_stateTime >= idleTime)
        {
            _stateTime = 0;
            Debug.Log("Wander");
            //currentState = State.Wander;
            SetState(State.Wander);
            return;
        }
        
        if (_waitingTime >= waitingTime)
        {
            _stateTime = 0;
            _waitingTime = 0;
            SelectNextPos();
        }
    }
    
    /// <summary>
    /// Selects the next position.
    /// </summary>
    private void SelectNextPos()
    {
        if (snapPointsCurrent.Count == 0) // no more snapoints
        {
            Debug.Log("Finish the visiting");
            return;
        }
        // Have to consider the data type conversion.
        _posIndex = UnityEngine.Random.Range(0, snapPointsCurrent.Count);
        //Debug.Log(_visitingPos[_posIndex].localPosition); // NO LOCAL POS PLZ
        //currentState = State.Walk;
        SetState(State.Walk);
    }
    
    /// <summary>
    /// Updates the state of the walk.
    /// </summary>
    private void UpdateWalkState()
    {
        _isWalk = true;
        //_agent.destination = _visitingPos[_posIndex].position;
        if (Vector3.Distance(agentTransform.position, _agent.destination) < 
            2)
        {
            //Debug.Log("Idle");
            //currentState = State.Idle;
            snapPointsCurrent.Remove(snapPointsCurrent[_posIndex]);
            SetState(State.Idle);
        }
        
    }


    //-------------------------------------------------------------------------
    #endregion

    #region MyRegion
    //-------------------------------------------------------------------------






    //-------------------------------------------------------------------------
    #endregion
}
