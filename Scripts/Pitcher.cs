using Godot;
using System;

public partial class Pitcher : Node3D
{
	[Export]
	public Node3D BaseballSpawnPoint { get; set; }

	[Export]
	public PackedScene BaseballScene { get; set; }

	private bool isAiming = false;

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Pitch")) {
			isAiming = true;
		}

		if (Input.IsActionJustReleased("Pitch")) {
			Vector2 mouseVelocity = Input.GetLastMouseVelocity();
			GD.Print(mouseVelocity);
			ThrowBall();
			isAiming = false;
		}
    }

	public void ThrowBall(){
		Node3D newBaseball = (Node3D) BaseballScene.Instantiate();

		newBaseball.Position = BaseballSpawnPoint.Position;

		AddChild(newBaseball);
	}

}
