using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{

    public float MAXHP = 0;
    public float HP;

 
    public delegate void OnArrivedToDestination();
    public OnArrivedToDestination OnArrivedToDestinationHandler;

    public delegate void OnLeaderExited();
    public OnLeaderExited OnLeaderWantsExitHandler; // the pack follwos


    #region Public Field
    /// <summary>
    /// Set up the Agent State.
    /// </summary>
    public enum State
    {
        Wander,
        Idle,
        Walk,
        Decision,
        EnterHouse,
        ExitHouse

    }
    
    [Tooltip("All the visit positions")]
    public Transform[] visitingPos;
    [Tooltip("Get the reference of the agent's position")]
    public Transform agentTransform;
    [Tooltip("The wander range of the agent")]
    public float wanderScope = 15;
    [Tooltip("The current state of the agent")]
    public State currentState;// = State.EnterHouse;


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
    
    private float _stateTime;
    
    private float _waitingTime;

    private bool _isWalk = false;

    //private List<Transform> _visitingPos = new List<Transform>();

    private int _posIndex;
    #endregion

    #region Init

    private List<Utils.PointWrapper> mySnappointsBottom;
    private List<Utils.PointWrapper> mySnappointsUp;
    private List<Utils.PointWrapper> snapPointsCurrent;

    public void RegisterSnapPoints(Vector3[] snappointsBottom, Vector3[] snappointsUP)
    {
        this.mySnappointsBottom = new List<Utils.PointWrapper>(snappointsBottom.Length);//];
        for (int i = 0; i < snappointsBottom.Length; i++)
        {
            this.mySnappointsBottom.Add(new Utils.PointWrapper(snappointsBottom[i]));
        }

        this.mySnappointsUp = new List<Utils.PointWrapper>(snappointsUP.Length);// /;/];
        for (int i = 0; i < snappointsUP.Length; i++)
        {
            this.mySnappointsUp.Add(new Utils.PointWrapper(snappointsUP[i]));
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


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    //ADDED
    public void SetState(State s)
    {
        if (currentState == State.ExitHouse) // EXIT HOUSE STATES OVERRIDES EVERYTHING
            return;

        Vector3 result = Vector3.zero;

        NavMeshHit hit;
        currentState = s;

        switch (currentState)
        {
            case State.Idle:
                //_visitor.destination = visitorTransform.position;
                _agent.destination = agentTransform.position;
                break;


            case State.ExitHouse:
                _agent.destination = Core.Level.Instance.Exit.transform.position;
                if (OnLeaderWantsExitHandler != null) OnLeaderWantsExitHandler.Invoke();

                break;

            case State.Wander:
                
                //return true;
                // else were in trouble

                result = agentTransform.position;

                int count = 0;
                while (!RandomPoint(agentTransform.position, wanderScope, out result)){ if (count == 20) break; }
                                      

                 //Vector3 nextDestination = pos;
                 _agent.destination = result;

                break;


            case State.Decision:
                if (snapPointsCurrent == mySnappointsBottom)
                {
                    snapPointsCurrent = mySnappointsUp;
                    SetState(State.Walk);
                }
                else
                {
                    SetState(State.ExitHouse);
                }


                break;

            case State.Walk:
                //UpdateChaseState();
                //_visitor.destination = agentTransform.position;


                // THE POINT HAS TO BE ON THE NAVMESH

                result = snapPointsCurrent[_posIndex].pt; ; // NOT GOOD
                if (NavMesh.SamplePosition(snapPointsCurrent[_posIndex].pt, out hit, 2.0f, NavMesh.AllAreas))
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
            case State.ExitHouse:
                UpdateExitState();
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
        
        if (_stateTime > wanderTime)
        {
            _stateTime = 0;
            Debug.Log("Idle");
            //currentState = State.Idle;
            SetState(State.Idle);
        }
        
        if (_waitingTime > waitingTime)
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
        
        if (_stateTime > idleTime)
        {
            _stateTime = 0;
            Debug.Log("Wander");
            //currentState = State.Wander;
            SetState(State.Wander);
        }
        
        if (_waitingTime > waitingTime)
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

        int count = 0;
        do
        {
            count++;
            _posIndex = UnityEngine.Random.Range(0, snapPointsCurrent.Count);
            if (count >= snapPointsCurrent.Count)
            {
                SetState(State.Decision); // ALL POINTS VISITED
                return;
            }
        }
        while (snapPointsCurrent[_posIndex].visited);


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
            Debug.Log("Idle");
            //currentState = State.Idle;
            SetState(State.Idle);
            snapPointsCurrent[_posIndex].visited = true;

            if(OnArrivedToDestinationHandler!= null)OnArrivedToDestinationHandler.Invoke();


        }
        
    }

    private void UpdateExitState()
    {
        //_isWalk = true;
        //_agent.destination = _visitingPos[_posIndex].position;
        if (Vector3.Distance(agentTransform.position, Core.Level.Instance.Exit.transform.position) <
            2)
        {
            Destroy(gameObject);

        }

    }


    //-------------------------------------------------------------------------
    #endregion

    #region MyRegion
    //-------------------------------------------------------------------------






    //-------------------------------------------------------------------------
    #endregion
}
