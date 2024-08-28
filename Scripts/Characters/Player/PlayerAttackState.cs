using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
  [Export]
  private Timer comboTimerNode;
  [Export]
  private PackedScene lightningScene;
  private int comboCounter = 1;
  private int maxComboCount = 2;

  public override void _Ready()
  {
    base._Ready();
    comboTimerNode.Timeout += () => comboCounter = 1;
  }

  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK + comboCounter, -1, 1.5f);
    characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;
  }

  protected override void ExitState()
  {
    characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    characterNode.HitBoxNode.BodyEntered -= HandleBodyEntered;
    comboTimerNode.Start();
  }

  private void HandleBodyEntered(Node3D body)
  {
    if (comboCounter != maxComboCount)
    {
      return;
    }
    Node3D lightning = lightningScene.Instantiate<Node3D>();
    GetTree().CurrentScene.AddChild(lightning);
    lightning.GlobalPosition = body.GlobalPosition;
  }

  private void HandleAnimationFinished(StringName animName)
  {
    comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
    characterNode.ToggleHitbox(true);
    characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
  }

  private void PerformHit()
  {
    Vector3 newPosition = characterNode.SpriteNode.FlipH ?
      Vector3.Left :
      Vector3.Right;
    float distanceMultiplier = 0.75f;
    characterNode.HitBoxNode.Position = newPosition * distanceMultiplier;
    characterNode.ToggleHitbox(false);
  }
}
