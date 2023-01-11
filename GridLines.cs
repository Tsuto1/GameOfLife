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
		
		for (int y = 0; y < tilemapRect.Size.y; y++) {
			DrawLine(
				new Vector2( 0, y * tilemapCellSize.y ),
				new Vector2( tilemapRect.Size.x * tilemapCellSize.x, y * tilemapCellSize.y ),
				color );
		}
		
		for (int x = 0; x < tilemapRect.Size.x; x++) {
			DrawLine(
				new Vector2( x * tilemapCellSize.x, 0 ),
				new Vector2( x * tilemapCellSize.x, tilemapRect.Size.y * tilemapCellSize.y ),
				color );
		}
	}
	
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
