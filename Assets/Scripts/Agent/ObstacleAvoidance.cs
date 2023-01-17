using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
	public Transform raycastTransform;
	[Range(1, 40)] public float distance = 1;
	[Range(0, 180)] public float maxAngle = 45;
	[Range(2, 50)] public int numRaycast = 2;
	public LayerMask layerMask;

	public bool IsObstacleInFront()
	{
		// check if object is in front of agent
		Ray ray = new Ray(transform.position, transform.forward);

		Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
		return Physics.Raycast(ray, distance, layerMask);
	}

	public Vector3 GetOpenDirection()
	{
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
		foreach (var direction in directions)
		{
			// cast ray from transform position towards direction
			Ray ray = new Ray(raycastTransform.position, raycastTransform.rotation * direction);
			// if there is NO raycast hit then that is an open direction
			if (!Physics.Raycast(ray, distance, layerMask))
			{
				Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);
				// return open direction
				return ray.direction;
			}
			else 
			{
				Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
			}

		}

		return transform.forward;
	}
}
