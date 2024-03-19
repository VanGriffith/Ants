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
		PackedScene antScene = GD.Load<PackedScene>("res://Ant.tscn");
		ants = new ArrayList();
		for (int i = 0; i < numAnts; i++) {
			CharacterBody2D ant = (CharacterBody2D)antScene.Instantiate();
			ant.Position = Ant.RandomUnitCircle()*10;
			ants.Add(ant);
			AddChild(ant);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
