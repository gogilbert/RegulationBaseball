using Godot;
using System;

public partial class Pitcher : Node3D
{
	[Export]
	public Node3D BaseballSpawnPoint { get; set; }

	[Export]
	public PackedScene BaseballScene { get; set; }

	[Export]
	public Timer PitchTime { get; set; }

	private bool isAiming = false;
	private Vector2 accMouseVelocity;

	// Mouse speed and pitch release queued up later on physics tick
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pitch")) {
			isAiming = true;
		}

		if (@event.IsActionReleased("Pitch")) {
			isAiming = false;
			accMouseVelocity = Vector2.Zero;
			PitchTime.Stop();
		}

        if (isAiming && @event is InputEventMouseMotion mouseMotion){
			if (PitchTime.IsStopped()){
				PitchTime.Start();
			}
			accMouseVelocity += mouseMotion.Relative;
		}
    }
	
	// Throw pitch when 
	private void OnPitchTimeout() {
		GD.Print(accMouseVelocity);
		isAiming = false;
		PitchTime.Stop();
		accMouseVelocity = Vector2.Zero;
		ThrowBall();
	}

    private void ThrowBall(){
		Node3D newBaseball = (Node3D) BaseballScene.Instantiate();

		newBaseball.Position = BaseballSpawnPoint.Position;

		AddChild(newBaseball);
	}

}
