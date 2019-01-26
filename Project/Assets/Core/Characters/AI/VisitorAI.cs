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
        Idle
    }
    
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
    private UnityEngine.AI.NavMeshAgent _agent;
    
    private float _stateTime;
    #endregion

    #region Init
    //-------------------------------------------------------------------------
    /// <summary>
    /// Get component of everything.
    /// </summary>
    private void OnEnable()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        }
    }
    
    /// <summary>
    /// Updates the state of the wander.
    /// </summary>
    void UpdateWanderState()
    {
        Vector3 randomRange = new Vector3 ( 
        (Random.value - 0.5f) * 2 * wanderScope, 0, 
        (Random.value - 0.5f) * 2 * wanderScope);
        
        Vector3 nextDestination = agentTransform.position + randomRange;
        _agent.destination = nextDestination;

        _stateTime += Time.deltaTime;
        
        if (_stateTime > wanderTime)
        {
            _stateTime = 0;
            currentState = State.Idle;
        }

        Debug.Log("Wander Time: " + _stateTime);
    }
    
    /// <summary>
    /// Updates the state of the idle.
    /// </summary>
    void UpdateIdleState()
    {
        _agent.destination = agentTransform.position;
        _stateTime += Time.deltaTime;
        
        if (_stateTime > idleTime)
        {
            _stateTime = 0;
            currentState = State.Wander;
        }

        Debug.Log("Idle Time: " + _stateTime);
    }
    //-------------------------------------------------------------------------
    #endregion

    #region MyRegion
    //-------------------------------------------------------------------------

    //-------------------------------------------------------------------------
    #endregion
}
