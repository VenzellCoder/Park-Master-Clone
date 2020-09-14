using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAppearance : MonoBehaviour
{
	[SerializeField] private Material aliveStateMaterial;
	[SerializeField] private Material deadStateMaterial;
	[SerializeField] private MeshRenderer renderer;


	public void SetAliveAppearance()
	{
		renderer.material = aliveStateMaterial;
	}

	public void SetDeadAppearance()
	{
		renderer.material = deadStateMaterial;
	}
}
