﻿namespace Steam.API
{
    public class SteamNSteelInitializedEvent
    {
        private readonly ISteamTransportRegistry _steamTransportRegistry;

        public SteamNSteelInitializedEvent(ISteamTransportRegistry steamTransportRegistry)
        {
            _steamTransportRegistry = steamTransportRegistry;
        }

        public ISteamTransportRegistry getSteamTransportRegistry()
        {
            return _steamTransportRegistry;
        }
    }
}
