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
	
	// Throw pitch when timer expires
	private void OnPitchTimeout() {
		GD.Print(accMouseVelocity);
		isAiming = false;
		PitchTime.Stop();
		ThrowBall();
		accMouseVelocity = Vector2.Zero;
	}

    private void ThrowBall(){
		Baseball newBaseball = BaseballScene.Instantiate<Baseball>();

		newBaseball.Position = BaseballSpawnPoint.Position;
		newBaseball.MouseVelocity = accMouseVelocity;

		AddChild(newBaseball);
	}

}
