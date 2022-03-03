﻿#if !DISABLESTEAMWORKS && STEAMWORKS_NET
using Steamworks;
using System;
using System.Net;

namespace HeathenEngineering.SteamworksIntegration.API
{
    /// <summary>
    /// Interface which provides access to a range of miscellaneous utility functions.
    /// </summary>
    /// <remarks>
    /// https://partner.steamgames.com/doc/api/ISteamUtils
    /// </remarks>
    public static class Utilities
    {
        /// <summary>
        /// Octet order from left to right as seen in string e.g. byte 24, byte 16, byte 8, byte 0
        /// </summary>
        /// <param name="address">And string which can be parsed by System.Net.IPAddress.Parse</param>
        /// <returns>Octet order from left to right as seen in string e.g. byte 24, byte 16, byte 8, byte 0</returns>
        public static uint IPStringToUint(string address)
        {
            var ipBytes = IPStringToBytes(address);
            var ip = (uint)ipBytes[0] << 24;
            ip += (uint)ipBytes[1] << 16;
            ip += (uint)ipBytes[2] << 8;
            ip += (uint)ipBytes[3];
            return ip;
        }

        /// <summary>
        /// Returns a human friendly string version of the uint address
        /// </summary>
        /// <param name="address">Octet order from left to right as seen in string e.g. byte 24, byte 16, byte 8, byte 0</param>
        /// <returns></returns>
        public static string IPUintToString(uint address)
        {
            var ipBytes = BitConverter.GetBytes(address);
            var ipBytesRevert = new byte[4];
            ipBytesRevert[0] = ipBytes[3];
            ipBytesRevert[1] = ipBytes[2];
            ipBytesRevert[2] = ipBytes[1];
            ipBytesRevert[3] = ipBytes[0];
            return new IPAddress(ipBytesRevert).ToString();
        }

        /// <summary>
        /// Octet order from left to right as seen in string is index 0, 1, 2, 3
        /// </summary>
        /// <param name="address">And string which can be parsed by System.Net.IPAddress.Parse</param>
        /// <returns>Octet order from left to right as seen in string is index 0, 1, 2, 3</returns>
        public static byte[] IPStringToBytes(string address)
        {
            var ipAddress = IPAddress.Parse(address);
            return ipAddress.GetAddressBytes();
        }

        /// <summary>
        /// Flips an image buffer
        /// This is used when loading images from Steamworks as they tend to be inverted for what Unity wants
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] FlipImageBufferVertical(int width, int height, byte[] buffer)
        {
            byte[] result = new byte[buffer.Length];

            int xWidth = width * 4;
            int yHeight = height;

            for (int y = 0; y < yHeight; y++)
            {
                for (int x = 0; x < xWidth; x++)
                {
                    result[x + ((yHeight - 1 - y) * xWidth)] = buffer[x + (xWidth * y)];
                }
            }

            return result;
        }

        public static class Client
        {
            public static string IpCountry => SteamUtils.GetIPCountry();
            public static uint SecondsSinceAppActive => SteamUtils.GetSecondsSinceAppActive();
            public static DateTime ServerRealTime => new DateTime(1970, 1, 1).AddSeconds(SteamUtils.GetServerRealTime());
            public static string SteamUILanguage => SteamUtils.GetSteamUILanguage();
            public static bool IsSteamInBigPictureMode => SteamUtils.IsSteamInBigPictureMode();
            public static bool IsSteamRunningInVR => SteamUtils.IsSteamRunningInVR();
            public static bool IsSteamRunningOnSteamDeck => SteamUtils.IsSteamRunningOnSteamDeck();
            public static bool IsVRHeadsetStreamingEnabled
            {
                get => SteamUtils.IsVRHeadsetStreamingEnabled();
                set => SteamUtils.SetVRHeadsetStreamingEnabled(value);
            }
            public static void SetGameLauncherMode(bool mode) => SteamUtils.SetGameLauncherMode(mode);
            public static void StartVRDashboard() => SteamUtils.StartVRDashboard();
        }
    }
}
#endif