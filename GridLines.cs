using Godot;
using System;

public class GridLines : Node2D
{
	// Toggle the grid's visibility
	public bool showGrid = true;
	private TileMap parent; // used for calculating grid shape, as well as other functions
	private Rect2 tilemapRect; // used for calculating grid shape
	private Vector2 tilemapCellSize; // used for calculating grid shape
	private Color color;

	public override void _Ready()
	{
		parent = GetParent<TileMap>(); // used for calculating grid shape, as well as other functions
		tilemapRect = parent.GetUsedRect(); // used for calculating grid shape
		tilemapCellSize = parent.CellSize; // used for calculating grid shape
		color = new Color(0.9f, 0.9f, 0.9f);
	}
	
	public override void _Draw() {
		if (!showGrid) return;
		
		// 
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
	
	public override void _Input(InputEvent @event)
	{
		// Show/hide grid. Key: G
		if (Input.GetActionStrength("ui_toggle_grid") == 1) {
			showGrid = !showGrid;
			Update();
		}
	}
}
