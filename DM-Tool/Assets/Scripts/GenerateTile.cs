using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTile : MonoBehaviour
{
    public TileCell tilePrefab;
	public Camera MainCamera;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			HandleInput();
		}
	}

	void Awake()
    {
		Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);

    }

	void HandleInput()
	{
		RaycastHit hit;
		Ray inputRay = MainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(inputRay, out hit))
		{
			TouchGround(hit.point);
			Debug.DrawLine(MainCamera.transform.position, hit.point, Color.red);
		}
	}

	void TouchGround(Vector3 position)
	{
		//position = transform.InverseTransformPoint(position);
		Debug.Log("touched at " + position);
		Instantiate(tilePrefab, position, Quaternion.identity);
	}
}
