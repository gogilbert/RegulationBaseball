using Godot;
using System;

public partial class Pitcher : Node3D
{
	[Export]
	public Node3D BaseballSpawnPoint { get; set; }

	[Export]
	public PackedScene BaseballScene { get; set; }

	private bool isAiming = false;
	private Vector2 mousevelocity;

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("Pitch")) {
			isAiming = true;
		}

		if (Input.IsActionJustReleased("Pitch")) {
			GD.Print(mousevelocity);
			ThrowBall();
			isAiming = false;
		}
    }

    public override void _Input(InputEvent @event)
    {
        if (isAiming && @event is InputEventMouseMotion mouseMotion){
			mousevelocity = mouseMotion.Relative;
		}
    }
    public void ThrowBall(){
		Node3D newBaseball = (Node3D) BaseballScene.Instantiate();

		newBaseball.Position = BaseballSpawnPoint.Position;

		AddChild(newBaseball);
	}

}
