using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 10.0f;
	public const float SPRINT_FACTOR = 2.0f;
	public const float JumpVelocity = 4.5f;
	
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("rotate_left"))
		{
			Rotation = new Vector3(Rotation.X, Rotation.Y + 0.1f, Rotation.Z);
		}
		else if (Input.IsActionPressed("rotate_right"))
		{
			Rotation = new Vector3(Rotation.X, Rotation.Y - 0.1f, Rotation.Z);
		}

		Vector3 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// JUMMP
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		if (Input.IsActionPressed("sprint"))
		{
			direction *= SPRINT_FACTOR;
		}

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
