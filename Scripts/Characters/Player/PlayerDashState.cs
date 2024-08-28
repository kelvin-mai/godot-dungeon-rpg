using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
  [Export]
  private Timer dashTimerNode;
  [Export]
  private PackedScene bombScene;
  [Export(PropertyHint.Range, "0,20,0.1")]
  private float speed = 10;

  public override void _Ready()
  {
    base._Ready();
    dashTimerNode.Timeout += HandleDashTimeout;
  }

  protected override void EnterState()
  {
    characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DASH);
    characterNode.Velocity = new(
      characterNode.direction.X, 0, characterNode.direction.Y
    );
    if (characterNode.Velocity == Vector3.Zero)
    {
      characterNode.Velocity = characterNode.SpriteNode.FlipH ?
      Vector3.Left :
      Vector3.Right;
    }
    characterNode.Velocity *= speed;
    dashTimerNode.Start();
    Node3D bomb = bombScene.Instantiate<Node3D>();
    GetTree().CurrentScene.AddChild(bomb);
    bomb.GlobalPosition = characterNode.GlobalPosition;
  }

  public override void _PhysicsProcess(double delta)
  {
    characterNode.MoveAndSlide();
    characterNode.Flip();
  }

  private void HandleDashTimeout()
  {
    characterNode.Velocity = Vector3.Zero;
    characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
  }
}
