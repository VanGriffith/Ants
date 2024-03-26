using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class Ant : CharacterBody2D
{
	public float maxSpeed = 2;
	public float steerStrength = 2;
	public float wanderStrength = 0.1f;

	private ArrayList nearbyFood;

	Random rand;
	Vector2 velocity;
	Vector2 direction;
	Vector2 desiredDirection;
	Vector2 desiredPosition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rand = new Random();
		this.nearbyFood = new ArrayList();
		GD.Print("Ant");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		desiredDirection = (desiredDirection + RandomUnitCircle() * wanderStrength).Normalized();

		Vector2 desiredVelocity = desiredDirection * maxSpeed;
		Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
		Vector2 acceleration = ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

		velocity = ClampMagnitude(velocity + acceleration, maxSpeed);
		//this.Position += velocity;
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

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		var collision_info = MoveAndCollide(velocity);
		if (collision_info != null) {
			desiredDirection = desiredDirection.Bounce(collision_info.GetNormal());	
		}
	}
	private void OnSmellRadiusEntered(Area2D area)
	{
		this.desiredDirection = (area.GlobalPosition - this.GlobalPosition).Normalized();
	}

	private void OnSmellRadiusExited(Area2D area)
	{
		if (area.IsInGroup("Food")) {
			nearbyFood.Remove(area);
			GD.Print("Food Removed");
		}
	}
}
