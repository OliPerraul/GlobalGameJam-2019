using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VisitorAI : MonoBehaviour
{
    public float MAXHP = 0;
    public float HP = 0;


    #region Public Field
    /// <summary>
    /// Set up the Agent State.
    /// </summary>
    public enum State
    {
        Wander,
        Idle,
        Chase,
        ExitHouse

    }

    [SerializeField]
    private Animator anitor; public float walkanimthereshold = 0.01f;


    [Tooltip("Get the reference of the visitor's position")]
    public Transform visitorTransform;
    [Tooltip("Get the reference of the agent's position")]
    public Transform agentTransform;

    public float arrivalScope = 15;

    [Tooltip("The wander range of the agent")]
    public float wanderScope = 15;
    [Tooltip("The current state of the agent")]
    public State currentState;

    public float wanderTime = 3;
    public float idleTime = 3;
    #endregion

    #region Private Field
    /// <summary>
    /// The agent for Nav mesh.
    /// </summary>
    private UnityEngine.AI.NavMeshAgent _visitor;
    
    private float _stateTime;
    #endregion

    #region Init
    //-------------------------------------------------------------------------
    /// <summary>
    /// Get component of everything.
    /// </summary>
    private void OnEnable()
    {
        _visitor = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _stateTime = 0;
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

        if (_visitor.velocity.magnitude > walkanimthereshold)
        {
            anitor.SetBool("IsWalking", true);
        }
        else
        {
            anitor.SetBool("IsWalking", false);
        }
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

        currentState = s;
        Vector3 result = Vector3.zero;

        switch (currentState)
        {
            //case State.ExitHouse:
            case State.ExitHouse:
                _visitor.destination = Core.Level.Instance.Exit.transform.position;
                break;


            case State.Idle:
                _visitor.destination = visitorTransform.position;
                break;

            case State.Wander:
                //UpdateWanderState();
                result = agentTransform.position;

                int count = 0;
                while (!RandomPoint(agentTransform.position, wanderScope, out result)) { count++;  if (count == 20) break; }


                //Vector3 nextDestination = pos;
                _visitor.destination = result;
                break;


            case State.Chase:
                //UpdateChaseState();
                //_visitor.destination = agentTransform.position;

                result = agentTransform.position;// NOT GOOD
                count = 0;
                while (!RandomPoint(result, arrivalScope, out result)) { count++; if (count == 20) break; }
                _visitor.destination = result;
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
            case State.Chase:
                UpdateChaseState();
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
        _stateTime += Time.deltaTime;
        
        if (_stateTime > wanderTime)
        {
            _stateTime = 0;
            //currentState = State.Idle;
            SetState(State.Idle);
        }
        
        if (agentTransform.GetComponent<AgentAI>().isWalk)
        {
            _stateTime = 0;
            SetState(State.Chase);
            //currentState = State.Chase;
        }
    }
    
    /// <summary>
    /// Updates the state of the idle.
    /// </summary>
    void UpdateIdleState()
    {

        _stateTime += Time.deltaTime;
        
        if (_stateTime > idleTime)
        {
            _stateTime = 0;
            //currentState = State.Wander;
            SetState(State.Wander);
        }

    }
    
    /// <summary>
    /// Updates the state of the chase.
    /// </summary>
    private void UpdateChaseState()
    {
        //_visitor.destination = agentTransform.position;
        
        if (!agentTransform.GetComponent<AgentAI>().isWalk)
        {
            //currentState = State.Idle;
            SetState(State.Wander);
        }
    }


    private void UpdateExitState()
    {
        //_isWalk = true;
        //_agent.destination = _visitingPos[_posIndex].position;
        if (Vector3.Distance(visitorTransform.position, Core.Level.Instance.Exit.transform.position) <
            2)
        {

            Destroy(transform.parent.gameObject);
            Core.Game.Instance.AICount--;
            if (Core.Game.Instance.AICount <= 0)
            {
                Core.Game.Instance.AICount = 0;
                Core.Game.Instance.OnWaveCleared();
            }

        }

    }


    public void OnREAgentArrivedToDest()
    {
        SetState(State.Chase);
    }

    public void OnLeaderWantsQuit()
    {
        SetState(State.ExitHouse);
    }

    //-------------------------------------------------------------------------
    #endregion

    #region MyRegion
    //-------------------------------------------------------------------------

    //-------------------------------------------------------------------------
    #endregion
}
