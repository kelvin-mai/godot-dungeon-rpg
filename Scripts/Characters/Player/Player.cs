using Godot;
using System;

public partial class Player : CharacterBody3D
{
  [ExportGroup("Required Nodes")]
  [Export]
  public AnimationPlayer animationPlayerNode;
  [Export]
  public Sprite3D spriteNode;
  [Export]
  public StateMachine stateMachineNode;
  public Vector2 direction = new();

  public override void _PhysicsProcess(double delta)
  {
    Velocity = new(direction.X, 0, direction.Y);
    Velocity *= 5;

    Flip();
    MoveAndSlide();
  }

  public override void _Input(InputEvent @event)
  {
    direction = Input.GetVector(
      GameConstants.INPUT_MOVE_LEFT,
      GameConstants.INPUT_MOVE_RIGHT,
      GameConstants.INPUT_MOVE_FORWARD,
      GameConstants.INPUT_MOVE_BACKWARD
    );
  }

  private void Flip()
  {
    if (Velocity.X == 0)
    {
      return;
    }
    spriteNode.FlipH = Velocity.X < 0;
  }
}
