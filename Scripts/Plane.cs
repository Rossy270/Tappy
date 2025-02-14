using Godot;
using System;

public partial class Plane : CharacterBody2D
{
	const float GRAVITY = 800.0f;
	const float POWER = -450.0f;

	AnimatedSprite2D sprite;
	bool _isFallingAnimation = true;
	Tween _rotationTween;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}


    public override void _Process(double delta)
    {
       if (_isFallingAnimation) {
		StartRotationTween(90f, 0.8f, Tween.TransitionType.Quad, Tween.EaseType.Out);
	   }
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.Y += GRAVITY * (float) delta;

		if (Input.IsActionJustPressed("fly")) {
			velocity.Y = POWER;
			OnJumpAnimation();
		}

		Velocity = velocity;

		MoveAndSlide();
	}

	void StartRotationTween(float targetDegrees, float duration, Tween.TransitionType transitionType, Tween.EaseType ease) {
		//Interrompe o Tween anterior se existir
		_rotationTween?.Kill();

		//Criando novo Tween
		_rotationTween = CreateTween();

		_rotationTween.SetTrans(transitionType)
					  .SetEase(ease)
					  .TweenProperty(sprite, "rotation_degrees", targetDegrees, duration);
		
	}

	void OnJumpAnimation() {
		_isFallingAnimation = false;

	StartRotationTween(-30f, 0.15f, Tween.TransitionType.Quad, Tween.EaseType.Out);

		GetTree().CreateTimer(0.3f).Timeout += () => _isFallingAnimation = true;
	}
}
