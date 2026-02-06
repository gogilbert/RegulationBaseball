using Godot;
using System;

public partial class Pitcher : Node3D
{
	[Export]
	public Node3D BaseballSpawnPoint { get; set; }

	[Export]
	public PackedScene BaseballScene { get; set; }

	private bool isAiming = false;
	private bool throwOnTick = false;
	private Vector2 mousevelocity;

    public override void _PhysicsProcess(double delta)
    {
		// Ensures physics can sync across clients
        if (throwOnTick) {
			GD.Print(mousevelocity);
			isAiming = false;
			throwOnTick = false;
			ThrowBall();
		}
    }

	// Mouse speed and pitch release queued up later on physics tick
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pitch")) {
			isAiming = true;
		}

		if (@event.IsActionReleased("Pitch")) {
			throwOnTick = true;
		}

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
