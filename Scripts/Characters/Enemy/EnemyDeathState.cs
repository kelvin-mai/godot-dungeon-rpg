using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DEATH);
    characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
  }

  private void HandleAnimationFinished(StringName animName)
  {
    characterNode.PathNode.QueueFree();
  }
}
