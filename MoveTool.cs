using Godot;
using System;

public class MoveTool : TextureButton
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private void _On_MoveTool_Pressed()
	{
		if (!Pressed)
			return;
		if (GetNode<TextureButton>("../DrawTool").Pressed)
			GetNode<TextureButton>("../DrawTool").Pressed = false;
		if (GetNode<TextureButton>("../EraseTool").Pressed)
			GetNode<TextureButton>("../EraseTool").Pressed = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
