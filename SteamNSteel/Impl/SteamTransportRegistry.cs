﻿using System.Collections.Concurrent;
using Steam.API;

namespace SteamNSteel.Impl
{
    // ReSharper disable SuggestVarOrType_Elsewhere
    // ReSharper disable SuggestVarOrType_SimpleTypes
    // ReSharper disable SuggestVarOrType_BuiltInTypes  
    public class SteamTransportRegistry : ISteamTransportRegistry
    {
        private readonly ConcurrentDictionary<SteamTransportLocation, SteamTransport> _steamTransports =
            new ConcurrentDictionary<SteamTransportLocation, SteamTransport>();

        public ISteamTransport registerSteamTransport(int x, int y, Direction[] initialAllowedDirections)
        {
			SteamTransportLocation steamTransportLocation = SteamTransportLocation.create(x, y);
            SteamTransport result = _steamTransports.GetOrAdd(steamTransportLocation, new SteamTransport(steamTransportLocation));

			bool[] allowedDirections = new bool[6];

			foreach (Direction initialAllowedDirection in initialAllowedDirections)
			{
				allowedDirections[(int)initialAllowedDirection] = true;
			}

	        foreach (Direction direction in Direction.VALID_DIRECTIONS)
	        {
		        bool canConnect = allowedDirections[(int) direction];
		        result.setCanConnect(direction, canConnect);
	        }

	        TheMod.SteamTransportStateMachine.addTransport(result);
			return result;
        }

        public void destroySteamTransport(int x, int y)
        {
            SteamTransport transport;
            var steamTransportLocation = SteamTransportLocation.create(x, y);

	        if (_steamTransports.TryRemove(steamTransportLocation, out transport))
	        {
		        TheMod.SteamTransportStateMachine.removeTransport(transport);
	        }
        }

		public ISteamTransport getSteamTransportAtLocation(SteamTransportLocation steamTransportLocation)
		{
			SteamTransport value;
			if (_steamTransports.TryGetValue(steamTransportLocation, out value))
			{
				return value;
			}
			return null;
		}
	}
}