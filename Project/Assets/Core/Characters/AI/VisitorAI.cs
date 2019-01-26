using System;
using UnityEngine;

public class VisitorAI : MonoBehaviour
{

    #region Public Field
    /// <summary>
    /// Set up the Agent State.
    /// </summary>
    public enum State
    {
        Wander,
        Idle,
        Chase
    }
    
    [Tooltip("Get the reference of the visitor's position")]
    public Transform visitorTransform;
    [Tooltip("Get the reference of the agent's position")]
    public Transform agentTransform;
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
        }
    }

    /// <summary>
    /// Updates the state of the wander.
    /// </summary>
    void UpdateWanderState()
    {
        Vector3 randomRange = new Vector3 ( 
        (UnityEngine.Random.value - 0.5f) * 2 * wanderScope, 0, 
        (UnityEngine.Random.value - 0.5f) * 2 * wanderScope);
        
        Vector3 nextDestination = visitorTransform.position + randomRange;
        _visitor.destination = nextDestination;

        _stateTime += Time.deltaTime;
        
        if (_stateTime > wanderTime)
        {
            _stateTime = 0;
            currentState = State.Idle;
        }
        
        if (agentTransform.GetComponent<AgentAI>().isWalk)
        {
            _stateTime = 0;
            currentState = State.Chase;
        }

        Debug.Log("Wander Time: " + _stateTime);
    }
    
    /// <summary>
    /// Updates the state of the idle.
    /// </summary>
    void UpdateIdleState()
    {
        _visitor.destination = visitorTransform.position;
        _stateTime += Time.deltaTime;
        
        if (_stateTime > idleTime)
        {
            _stateTime = 0;
            currentState = State.Wander;
        }

        Debug.Log("Idle Time: " + _stateTime);
    }
    
    /// <summary>
    /// Updates the state of the chase.
    /// </summary>
    private void UpdateChaseState()
    {
        _visitor.destination = agentTransform.position;
        
        if (!agentTransform.GetComponent<AgentAI>().isWalk)
        {
            currentState = State.Idle;
        }
    }
    //-------------------------------------------------------------------------
    #endregion

    #region MyRegion
    //-------------------------------------------------------------------------

    //-------------------------------------------------------------------------
    #endregion
}
