using Godot;
using System;

public partial class SpawnPipesControll : Node2D
{
	const int RANDOM_SPACE = 150;
	const double TIMEOUT_FOR_TIMER = 1.5;

	PackedScene pipeScene = GD.Load<PackedScene>("res://Scenes/Pipes/Pipes.tscn");

	Timer timer;
	Rect2 vpr;

	public override void _Ready()
	{
		vpr = GetViewportRect();
		timer = GetNode<Timer>("Timer");
		timer.Timeout += SpawnPipe;
	}

	void SpawnPipe() {
		Node2D pipes = pipeScene.Instantiate() as Node2D;
		AddChild(pipes);
		pipes.Position = GetRandomPos();

		GetTree().CreateTimer(TIMEOUT_FOR_TIMER).Timeout += () => timer.Start();
	}

	Vector2 GetRandomPos() {
		Vector2 newPos = Vector2.Zero;

		float halfHeightScreen = vpr.Size.Y / 2;

		newPos.Y = halfHeightScreen + GD.RandRange(-RANDOM_SPACE, RANDOM_SPACE);

		newPos.X = vpr.End.X;

		return newPos;
	}
}
