using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
  }
}
