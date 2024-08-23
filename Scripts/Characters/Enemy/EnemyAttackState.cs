using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
  private Vector3 targetPosition;

  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK);
    Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
    targetPosition = target.GlobalPosition;
    characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
  }

  protected override void ExitState()
  {
    characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
  }

  private void HandleAnimationFinished(StringName animName)
  {
    characterNode.ToggleHitbox(true);
    Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();
    if (target == null)
    {
      Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
      if (chaseTarget == null)
      {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
      }
      characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
      return;
    }
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK);
    targetPosition = target.GlobalPosition;
    Vector3 direciton = characterNode.GlobalPosition.DirectionTo(targetPosition);
    characterNode.SpriteNode.FlipH = direciton.X < 0;
  }

  private void PerformHit()
  {
    characterNode.ToggleHitbox(false);
    characterNode.HitBoxNode.GlobalPosition = targetPosition;
  }
}
