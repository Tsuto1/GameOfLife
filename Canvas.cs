using Godot;
using Godot.Collections;
using System;

public class Canvas : TileMap
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	[Export]
	public bool running = false;
	
	private float totalDelta = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public override void _PhysicsProcess(float delta) {
		totalDelta += delta;
		if (totalDelta >= 0.2f){
			Step(delta);
			totalDelta = 0;
		}
	}
	
	private int CountNeighbors(Vector2 cell) {
		int count = 0;
			
		for (float y = cell.y - 1; y < cell.y + 2; y++)
			for (float x = cell.x - 1; x < cell.x + 2; x++) {
				if (new Vector2(x, y) == cell)
					continue;
				if (GetCellv(new Vector2(x, y)) == 0)
					count++;
			}
		return count;
	}
	
	private void Step(float delta) {
		Dictionary<Vector2, int> changes = new Dictionary<Vector2, int>();
		foreach (Vector2 cell in GetUsedCells()) {
			var neighborCount = CountNeighbors(cell);
			
			if (GetCellv(cell) == 0) {
				if (neighborCount < 2 || neighborCount > 3) {
					changes.Add(cell, 2);
				}
			} else {
				if (neighborCount == 3) {
					changes.Add(cell, 0);
				}
			}
		}
		
		foreach (var change in changes) {
			SetCellv(change.Key, change.Value);
		}
	}
	
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
