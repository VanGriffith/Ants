using Godot;
using System;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class ant : Sprite2D
{
	public float maxSpeed = 5;
	public float acceleration = 0.5f;
	Random rand;
	Vector2 velocity;
	Vector2 direction;
	Vector2 desiredDirection;
	Vector2 desiredPosition;

	int timerIndex = 100;
	int timerDelay = 100;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rand = new Random();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (timerIndex == timerDelay) {
			desiredPosition = new Vector2(rand.Next(0,500), rand.Next(0, 500));
			timerIndex = 0;
		}
		timerIndex++;
		
		desiredDirection = (desiredPosition - this.Position).Normalized();
		Vector2 desiredVelocity = desiredDirection * maxSpeed;
		velocity += (desiredVelocity - velocity).Normalized() * acceleration;

		this.Position += velocity;
		this.Rotation = desiredDirection.Angle();

	}
}
