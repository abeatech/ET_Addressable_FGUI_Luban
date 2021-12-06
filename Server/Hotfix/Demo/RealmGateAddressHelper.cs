using System.Collections.Generic;


namespace ET
{
	public static class RealmGateAddressHelper
	{
		public static StartSceneData GetGate(int zone)
		{
			List<StartSceneData> zoneGates = StartServerComponent.Instance.Gates[zone];

			int n = RandomHelper.RandomNumber(0, zoneGates.Count);

			return zoneGates[n];
		}
	}
}
