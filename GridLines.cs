using Godot;
using System;

public class GridLines : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	[Export]
	private bool showGrid = true;
	private TileMap parent;
	private Rect2 tilemapRect;
	private Vector2 tilemapCellSize;
	private Color color;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<TileMap>();
		tilemapRect = parent.GetUsedRect();
		tilemapCellSize = parent.CellSize;
		color = new Color(0.9f, 0.9f, 0.9f);
	}
	
	public override void _Draw() {
		if (!showGrid) return;
		
		for (float y = tilemapRect.Position.y * tilemapCellSize.y;
			y < (tilemapRect.Size.y + tilemapRect.Position.y) * tilemapCellSize.y;
			y += tilemapCellSize.y) {
			
			DrawLine(
				new Vector2(tilemapRect.Position.x * tilemapCellSize.x, y),
				new Vector2((tilemapRect.Position.x + tilemapRect.Size.x) * tilemapCellSize.x, y),
				color
			);
		}
		
		for (float x = tilemapRect.Position.x * tilemapCellSize.x;
			x < (tilemapRect.Size.x + tilemapRect.Position.x) * tilemapCellSize.x;
			x += tilemapCellSize.x) {
			
			DrawLine(
				new Vector2(x, tilemapRect.Position.y * tilemapCellSize.y),
				new Vector2(x, (tilemapRect.Position.y + tilemapRect.Size.y) * tilemapCellSize.y),
				color
			);
		}
	}
	
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
