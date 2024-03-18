using Godot;
using System;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class Ant : Sprite2D
{
	public float maxSpeed = 2;
	public float steerStrength = 2;
	public float wanderStrength = 0.1f;

	Random rand;
	Vector2 velocity;
	Vector2 direction;
	Vector2 desiredDirection;
	Vector2 desiredPosition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Texture = (Texture2D)GD.Load("res://sprites/ant.png");
		rand = new Random();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		desiredDirection = (desiredDirection + RandomUnitCircle() * wanderStrength).Normalized();

		Vector2 desiredVelocity = desiredDirection * maxSpeed;
		Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
		Vector2 acceleration = ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

		velocity = ClampMagnitude(velocity + acceleration, maxSpeed);
		this.Position += velocity;
		this.Rotation = desiredDirection.Angle();

	}

	public static Vector2 ClampMagnitude(Vector2 vector, float maxLength) {
		if (vector.Length() > maxLength) {
			return vector.Normalized() * maxLength;
		}
		else {
			return new Vector2(vector.X, vector.Y);
		}
	}

	public static Vector2 RandomUnitCircle() {
		Random randy = new Random();
		return new Vector2(0,1).Rotated(Mathf.DegToRad(randy.Next(0,360)));
	}
}
