using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
    characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
  }

  protected override void ExitState()
  {
    characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
  }

  public override void _PhysicsProcess(double delta)
  {
    characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
  }
}
