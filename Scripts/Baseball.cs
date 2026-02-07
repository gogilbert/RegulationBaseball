using Godot;
using System;

public partial class Baseball : RigidBody3D
{
	[Export]
	public Vector2 MouseVelocity {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		Vector3 InitialImpulse = new Vector3(MouseVelocity.X/100,0,MouseVelocity.Y/100);
		ApplyCentralImpulse(InitialImpulse);
	}

}
