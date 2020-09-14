using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Layers
{
	public const string FLOOR_FOR_PATH = "Floor";
	public const string START_POINT = "StartPoint";
	public const string FINISH_POINT = "FinishPoint";
	public const string UNIT = "Unit";
	public const string OBSTACLE = "Obstacle";


	public static LayerMask GetLayerByName(string layerName)
	{
		int layer = 0;
		layer |= (1 << LayerMask.NameToLayer(layerName));
		layer = ~layer;
		return layer;
	}
}
