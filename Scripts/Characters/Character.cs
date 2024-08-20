using Godot;

public abstract partial class Character : CharacterBody3D
{
  [ExportGroup("Required Nodes")]
  [Export]
  public AnimationPlayer AnimationPlayerNode { get; private set; }
  [Export]
  public Sprite3D SpriteNode { get; private set; }
  [Export]
  public StateMachine StateMachineNode { get; private set; }
  public Vector2 direction = new();

  public void Flip()
  {
    if (Velocity.X == 0)
    {
      return;
    }
    SpriteNode.FlipH = Velocity.X < 0;
  }
}