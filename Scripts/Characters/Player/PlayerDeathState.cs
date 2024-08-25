using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DEATH);
    characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
  }

  private void HandleAnimationFinished(StringName animName)
  {
    GameEvents.RaiseEndGame();
    characterNode.QueueFree();
  }
}
