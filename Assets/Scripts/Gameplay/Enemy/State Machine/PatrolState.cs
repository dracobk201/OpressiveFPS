using UnityEngine;
using UnityEngine.AI;

public class PatrolState : iStates
{
    private bool _patrolForward;
    private int _currentPatrolIndex; 
    private float _timeToChange;
    private float _switchProbability;
    private float _initTime;
    private float _sightRadius;
    private EnemyStateMachine _stateMachine;
    private NavMeshAgent _agent;
    private Rigidbody _enemyRigidbody;

    public PatrolState(EnemyStateMachine stateMachine, Rigidbody enemyRigidbody, NavMeshAgent agent, float timeToChange, float switchProbability, float sightRadius)
    {
        _stateMachine = stateMachine;
        _enemyRigidbody = enemyRigidbody;
        _agent = agent;
        _timeToChange = timeToChange;
        _switchProbability = switchProbability;
        _sightRadius = sightRadius;
    }

    public override void OnEnter()
    {
        _initTime = 0;
        ChangePatrolPoint();
        SetNewDestination();
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        if (SearchPlayer())
            _stateMachine.ChangeState(Global.AttackState);

        _initTime += Time.deltaTime;
        if (_initTime >= _timeToChange)
            _stateMachine.ChangeState(Global.IdleState);
    }

    private void SetNewDestination()
    {
        Vector3 randomDirection;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        randomDirection = Random.insideUnitSphere * _sightRadius;
        randomDirection += _enemyRigidbody.position;
        NavMesh.SamplePosition(randomDirection, out hit, _sightRadius, NavMesh.AllAreas);
        finalPosition = hit.position;

        if (Mathf.Abs(finalPosition.x) == Mathf.Infinity || Mathf.Abs(finalPosition.y) == Mathf.Infinity || Mathf.Abs(finalPosition.z) == Mathf.Infinity)
        {
            Debug.Log(finalPosition);
            _agent.isStopped = true;
            return;
        }

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(_enemyRigidbody.position, finalPosition, NavMesh.AllAreas, path))
        {
            bool isvalid = (path.status != NavMeshPathStatus.PathComplete) ? false : true;
            if (isvalid)
            {
                _agent.SetDestination(finalPosition);
                _agent.isStopped = false;
            }
            else
            {
                _agent.isStopped = true;
            }
        }
    }

    private void ChangePatrolPoint()
    {
        if (Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }

        if (_patrolForward)
        {
           // _currentPatrolIndex = (_currentPatrolIndex + 1) % _waypoints.Items.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                //_currentPatrolIndex = _waypoints.Items.Count - 1;
            }
        }
    }

    private bool SearchPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_enemyRigidbody.position, _sightRadius);

        foreach (Collider item in hitColliders)
        {
            if (item.tag.Equals(Global.PlayerTag))
                return true;
        }
        return false;
    }
}
