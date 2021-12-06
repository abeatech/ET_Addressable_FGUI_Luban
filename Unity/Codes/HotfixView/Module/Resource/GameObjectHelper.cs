using System;
using UnityEngine;

namespace ET
{
	public static class GameObjectHelper
	{
		public static GameObject Instantiate(GameObject prefab , Transform parent,bool worldPositionStays)
		{
			return UnityEngine.Object.Instantiate(prefab, parent, worldPositionStays);
		}
	}
}