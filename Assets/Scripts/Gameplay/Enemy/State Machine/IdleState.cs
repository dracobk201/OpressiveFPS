using UnityEngine;

public class IdleState : iStates
{
    private float _timeToChange;
    private float _initTime;
    private EnemyStateMachine _stateMachine;
    private Animator _enemyAnimator;

    public IdleState(EnemyStateMachine stateMachine, Animator enemyAnimator, float timeToChange)
    {
        _timeToChange = timeToChange;
        _enemyAnimator = enemyAnimator;
        _stateMachine = stateMachine;
    }

    public override void OnEnter()
    {
        _initTime = 0;
        //_enemyAnimator.SetBool(Global.MOVINGANIMATION, false);
    }

    public override void OnExit()
    {
        //Debug.Log("Exit");
    }

    public override void Update()
    {
        _initTime += Time.deltaTime;
        if (_initTime >= _timeToChange)
        {
            _stateMachine.ChangeState(Global.PatrolState);
        }
    }
}
