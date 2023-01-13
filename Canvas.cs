using Godot;
using Godot.Collections;
using System;

public class Canvas : TileMap
{
	// Nodes and controls
	private TextureButton PlayPauseTool;
	private TextureButton EraseTool;
	private TextureButton DrawTool;
	private TextureButton MoveTool;
	private HSlider SpeedSlider;
	private Camera2D _Camera2D;
	
	// Variables
	// Helps control playback/stepping speed
	private float timer = 0;
	// Records mouse left click being held down for motion activity
	private bool mouseDown = false;
	// Blocks out actions while mouse is on the control board
	private bool mouseOut = false;
	
	public override void _Ready()
	{
		// Assign nodes and controls to paths
		PlayPauseTool = GetNode<TextureButton>("/root/Root/Camera2D/CanvasLayer/PlayPauseTool");
		EraseTool = GetNode<TextureButton>("/root/Root/Camera2D/CanvasLayer/EraseTool");
		DrawTool = GetNode<TextureButton>("/root/Root/Camera2D/CanvasLayer/DrawTool");
		MoveTool = GetNode<TextureButton>("/root/Root/Camera2D/CanvasLayer/MoveTool");
		SpeedSlider = GetNode<HSlider>("/root/Root/Camera2D/CanvasLayer/SpeedSlider");
		_Camera2D = GetNode<Camera2D>("/root/Root/Camera2D");
	}
	
	public override void _Input(InputEvent @event)
	{
		//
		if (@event is InputEventMouseButton eMouseButton) {
			if (eMouseButton.Pressed) {
				if (eMouseButton.ButtonIndex == (int)ButtonList.Left) {
					mouseDown = true;
					if (DrawTool.Pressed) {
						SetCellv(WorldToMap((eMouseButton.Position*_Camera2D.Zoom) + _Camera2D.Position), 0);
					} else if (EraseTool.Pressed) {
						SetCellv(WorldToMap((eMouseButton.Position*_Camera2D.Zoom) + _Camera2D.Position), 1);
					}
					
				} else if (eMouseButton.ButtonIndex == (int)ButtonList.WheelUp) {
					_Camera2D.Zoom += new Vector2(-0.1f, -0.1f);
				} else if (eMouseButton.ButtonIndex == (int)ButtonList.WheelDown) {
					_Camera2D.Zoom += new Vector2(0.1f, 0.1f);
				}
			} else if (eMouseButton.ButtonIndex == (int)ButtonList.Left) {
				mouseDown = false;
			}
		}
		
		if (@event is InputEventMouseMotion eMouseMotion) {
			if (mouseDown && !mouseOut) {
				if (DrawTool.Pressed) {
					SetCellv(WorldToMap((eMouseMotion.Position*_Camera2D.Zoom) + _Camera2D.Position), 0);
				} else if (EraseTool.Pressed) {
					SetCellv(WorldToMap((eMouseMotion.Position*_Camera2D.Zoom) + _Camera2D.Position), 1);
				} else if (MoveTool.Pressed) {
					_Camera2D.Position -= eMouseMotion.Relative * _Camera2D.Zoom;
				}
			}
		}
		
		if (Input.GetActionStrength("ui_reset_zoom") == 1) {
			_Camera2D.Zoom = new Vector2(1, 1);
			_Camera2D.Position = new Vector2(0, 0);
		}
		if (Input.GetActionStrength("ui_clear_map") == 1) {
			foreach (Vector2 cell in GetUsedCells())
				SetCellv(cell, 1);
		}
		if (Input.GetActionStrength("ui_play_pause") == 1) {
			PlayPauseTool.Pressed = !PlayPauseTool.Pressed;
		}
	}
	
	public override void _PhysicsProcess(float delta) {
		if (!PlayPauseTool.Pressed) return;
		
		timer += delta;
		if (timer >= (3 - SpeedSlider.Value)){
			Step(delta);
			timer = 0;
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
	
	/** mouseOut events
		each control has its own separate mouse_entered and mouse_exited events, 
		thus each control on the board ought to be checked**/
	private void _on_Board_mouse_entered() {
		mouseOut = true;
	}
	private void _on_Board_mouse_exited() {
		mouseOut = false;
	}
	private void _on_PlayPauseTool_mouse_entered() {
		mouseOut = true;
	}
	private void _on_PlayPauseTool_mouse_exited() {
		mouseOut = false;
	}
	private void _on_EraseTool_mouse_entered() {
		mouseOut = true;
	}
	private void _on_EraseTool_mouse_exited() {
		mouseOut = false;
	}
	private void _on_DrawTool_mouse_entered() {
		mouseOut = true;
	}
	private void _on_DrawTool_mouse_exited() {
		mouseOut = false;
	}
	private void _on_MoveTool_mouse_entered() {
		mouseOut = true;
	}
	private void _on_MoveTool_mouse_exited() {
		mouseOut = false;
	}
	private void _on_SpeedSlider_mouse_entered() {
		mouseOut = true;
	}
	private void _on_SpeedSlider_mouse_exited() {
		mouseOut = false;
	}
	private void _on_SpeedSliderLabel_mouse_entered() {
		mouseOut = true;
	}
	private void _on_SpeedSliderLabel_mouse_exited() {
		mouseOut = false;
	}
}
