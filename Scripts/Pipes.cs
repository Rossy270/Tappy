using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Pipes : Node2D
{

	const float WiDTH_LIMIT_SCREEN_PADDING = 50f;

	[Export] float pipeSpeed = 120;

	List<Area2D> pipes = new List<Area2D>();
	Area2D laser;
	AudioStreamPlayer2D audio;

	Rect2 vpr;
	bool _isOnScreen = false;

    public override void _Ready()
    {
        laser = GetNode<Area2D>("Laser");
		audio = GetNode<AudioStreamPlayer2D>("ScoreEffect");

		foreach(Node child in GetChildren()) {
			if (child.IsInGroup("pipe")) {
				pipes.Add(child as Area2D);
			}
		}

		vpr = GetViewportRect();
    }

    public override void _Process(double delta)
    {
       MovePipe((float) delta);
    }

	void MovePipe(float delta) {
		Vector2 dir = Vector2.Zero;

		dir.X = -pipeSpeed * delta;

		Position += dir;

		if (!_isOnScreen && Position.X > vpr.End.X) {
			_isOnScreen = true;
		}
		else if (_isOnScreen && Position.X < vpr.Position.X - WiDTH_LIMIT_SCREEN_PADDING) {
			DestroyPipes();
		}
	}

	void DestroyPipes() {
		QueueFree();
	}
}
