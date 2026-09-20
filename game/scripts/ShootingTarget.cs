using Godot;
using System;

public partial class ShootingTarget : Area3D
{
	public Action<ShootingTarget> OnDeath;

	public void _OnBodyEntered(Node3D Body)
	{
		GD.Print("siema");
		if (Body is Player)
		{
			
			OnDeath?.Invoke(this);
			QueueFree();
		}
	}
}
