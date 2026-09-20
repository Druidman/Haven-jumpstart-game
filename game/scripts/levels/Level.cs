
using System.Collections.Generic;
using System;
using Godot;
public partial class Level : Godot.Node3D
{
  // eg. level1
  private Random rng = new Random();
  
  static int max_num_of_targets = 10;

  private List<ShootingTarget> targets = new List<ShootingTarget>();


  [Export] public float world_width = 100f;
  [Export] public float level_name = 100f;
  [Export] public int num_of_targets = 20;

  [Export] public Godot.PackedScene ShootingTargetScene;
  private int targets_killed_count = 0;
  

  public override void _Ready()
  {
	GenerateShootingTargets();
  }

  public void GenerateShootingTargets()
  {
	for (int i = 0; i < Math.Min(max_num_of_targets, num_of_targets - targets_killed_count); i++)
	{
	  GenerateShootingTarget();
	}
  }

  public void onTargetKilled(ShootingTarget target)
  {
	this.targets.Remove(target);
  this.targets_killed_count += 1;
  }
  public void GenerateShootingTarget()
  {
	Godot.Vector3 position = new Godot.Vector3(
	  ((float)rng.NextDouble() - 0.5f) * (float)world_width,
	  (float)rng.NextDouble() + 0.5f,
	  ((float)rng.NextDouble() - 0.5f) * (float)world_width
	);

	var instance = ShootingTargetScene.Instantiate<ShootingTarget>();
	targets.Add(instance);

	instance.Position = position;
	instance.OnDeath += onTargetKilled;
  
	AddChild(instance);

	// find best position for it. Random one
	// add position to list
	
  }
}
