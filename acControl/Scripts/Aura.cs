﻿using HidLibrary;
using System.Drawing;

public static class Aura
{
    private static readonly byte[] MESSAGE_SET = { 0x5d, 0xb5 };
    private static readonly byte[] MESSAGE_APPLY = { 0x5d, 0xb4 };

    public const int Static = 0;
    public const int Breathe = 1;
    public const int Strobe = 2;
    public const int Rainbow = 3;
    public const int Dingding = 10;

    public const int SpeedSlow = 0;
    public const int SpeedMedium = 1;
    public const int SpeedHigh = 2;

    public static int Mode { get; set; } = Static;
    public static Color Color1 { get; set; } = Color.White;
    public static Color Color2 { get; set; } = Color.Black;
    public static int Speed { get; set; } = SpeedSlow;

    public static void ApplyAura()
    {
        int[] deviceIds = { 0x1854, 0x1869, 0x1866, 0x19b6 };

        foreach (HidDevice device in HidDevices.Enumerate(0x0b05, deviceIds))
        {
            if (device.IsConnected && device.Description.Contains("HID"))
            {
                device.OpenDevice();
                byte[] msg = {
                    0x5d, 0xb3, 0x00, (byte)Mode, (byte)Color1.R,
                    (byte)Color1.G, (byte)Color1.B, (byte)Speed, 0, 0, (byte)Color2.R,
                    (byte)Color2.G, (byte)Color2.B
                };
                device.Write(msg);
                device.Write(MESSAGE_SET);
                device.Write(MESSAGE_APPLY);
                device.CloseDevice();
            }
        }
    }
}
