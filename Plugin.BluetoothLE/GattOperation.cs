using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.BluetoothLE
{
    public enum GattOperation
    {
        Connection,
        Read,
        Write,
        ReliableWrite,
        ReadRssi,
        ReadDescriptor,
        WriteDescriptor,
        Mtu,
        Discover
    }
}
