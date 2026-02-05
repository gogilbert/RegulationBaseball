using Godot;
using System;

public partial class Main : Node3D
{
	private PackedScene BASEBALL = (PackedScene) ResourceLoader.Load("res://Scenes/Baseball.tscn");
	
	[Export]
	public Vector3 BASEBALL_POSITION { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseButton mouseButton){
			if(mouseButton.Pressed) {
				GD.Print("pressed");
				Node3D newBaseball = (Node3D) BASEBALL.Instantiate();

				newBaseball.Position = BASEBALL_POSITION;

				AddChild(newBaseball);
			}
		}

    }
}
