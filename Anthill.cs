using Godot;
using System;
using System.Collections;

public partial class Anthill : Node2D
{
	ArrayList ants;
	int numAnts = 30;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ants = new ArrayList();
		for (int i = 0; i < numAnts; i++) {
			Ant ant = new Ant();
			ant.Position = this.Position + Ant.RandomUnitCircle() * 10;
			AddChild(ant);
			ants.Add(ant);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
