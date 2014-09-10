using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidLibrary;
using System.Threading;

namespace FoodReceipt
{
    public class USBScale
    {
        public bool IsConnected
        {
            get
            {
                return scale == null ? false : scale.IsConnected;
            }
        }
        public decimal ScaleStatus
        {
            get
            {
                return inData.Data[1];
            }
        }
        public decimal ScaleWeightUnits
        {
            get
            {
                return inData.Data[2];
            }
        }
        private HidDevice scale;
        private HidDeviceData inData;

        public HidDevice GetDevice()
        {
            HidDevice hidDevice;

            hidDevice = HidDevices.Enumerate(0x1446, 0x6A73).FirstOrDefault();
            if (hidDevice != null)
                return hidDevice;

            // Metler Toledo
            hidDevice = HidDevices.Enumerate(0x0eb8).FirstOrDefault();
            if (hidDevice != null)
                return hidDevice;

            return null;
        }
        public bool Connect()
        {
            // Find a USBScale
            HidDevice device = GetDevice();
            if (device != null)
                return Connect(device);
            else
                return false;
        }
        public bool Connect(HidDevice device)
        {
            scale = device;
            int waitTries = 0;
            scale.OpenDevice();

            while (!scale.IsConnected && waitTries < 10)
            {
                Thread.Sleep(50);
                waitTries++;
            }
            return scale.IsConnected;
        }
        public void Disconnect()
        {
            if (scale.IsConnected)
            {
                scale.CloseDevice();
                scale.Dispose();
            }
        }

        public void GetWeight(out decimal? weight, out bool? isStable)
        {
            weight = null;
            isStable = false;

            if (scale.IsConnected)
            {
                inData = scale.Read(250);

                weight = (Convert.ToDecimal(inData.Data[4]) +
                            Convert.ToDecimal(inData.Data[5]) * 256) *
                            Convert.ToDecimal(Math.Pow(10, (sbyte)inData.Data[3]));
                        
                Convert.ToInt16(inData.Data[2]);

                isStable = inData.Data[1] == 0x4;
            }
        }
    }
}
