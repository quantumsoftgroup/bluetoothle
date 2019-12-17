using System;
using System.Reactive.Subjects;
using Android.Bluetooth;


namespace Plugin.BluetoothLE.Internals
{
    public class GattCallbacks : BluetoothGattCallback
    {
        private Device device;
        public GattCallbacks(Device d)
        {
            device = d;
        }

        public Subject<GattCharacteristicEventArgs> CharacteristicRead { get; } = new Subject<GattCharacteristicEventArgs>();

        public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.Read, new Guid(characteristic.Uuid.ToString()), (int) status);
            this.CharacteristicRead.OnNext(new GattCharacteristicEventArgs(gatt, characteristic, status));
        }


        public Subject<GattCharacteristicEventArgs> CharacteristicWrite { get; } = new Subject<GattCharacteristicEventArgs>();

        public override void OnCharacteristicWrite(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.Write, new Guid(characteristic.Uuid.ToString()), (int) status);
            this.CharacteristicWrite.OnNext(new GattCharacteristicEventArgs(gatt, characteristic, status));
        }


        public Subject<GattCharacteristicEventArgs> CharacteristicChanged { get; } = new Subject<GattCharacteristicEventArgs>();
        public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic)
            => this.CharacteristicChanged.OnNext(new GattCharacteristicEventArgs(gatt, characteristic));


        public Subject<GattDescriptorEventArgs> DescriptorRead { get; } = new Subject<GattDescriptorEventArgs>();

        public override void OnDescriptorRead(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.ReadDescriptor, new Guid(descriptor.Uuid.ToString()), (int) status);
            this.DescriptorRead.OnNext(new GattDescriptorEventArgs(gatt, descriptor, status));
        }


        public Subject<GattDescriptorEventArgs> DescriptorWrite { get; } = new Subject<GattDescriptorEventArgs>();

        public override void OnDescriptorWrite(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.WriteDescriptor, new Guid(descriptor.Uuid.ToString()), (int) status);
            this.DescriptorWrite.OnNext(new GattDescriptorEventArgs(gatt, descriptor, status));
        }


        public Subject<MtuChangedEventArgs> MtuChanged { get; } = new Subject<MtuChangedEventArgs>();

        public override void OnMtuChanged(BluetoothGatt gatt, int mtu, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.Mtu, device.Uuid, (int) status);
            this.MtuChanged.OnNext(new MtuChangedEventArgs(mtu, gatt, status));
        }

        public Subject<GattRssiEventArgs> ReadRemoteRssi  { get; } = new Subject<GattRssiEventArgs>();

        public override void OnReadRemoteRssi(BluetoothGatt gatt, int rssi, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.ReadRssi, device.Uuid, (int) status);
            this.ReadRemoteRssi.OnNext(new GattRssiEventArgs(rssi, gatt, status));
        }


        public Subject<GattEventArgs> ReliableWriteCompleted { get; } = new Subject<GattEventArgs>();

        public override void OnReliableWriteCompleted(BluetoothGatt gatt, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.ReliableWrite, device.Uuid, (int) status);
            this.ReliableWriteCompleted.OnNext(new GattEventArgs(gatt, status));
        }

        public Subject<GattEventArgs> ServicesDiscovered { get; } = new Subject<GattEventArgs>();

        public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status)
        {
            device?.InvokeErrorReceived(GattOperation.Discover, device.Uuid, (int) status);
            this.ServicesDiscovered.OnNext(new GattEventArgs(gatt, status));
        }

        public Subject<ConnectionStateEventArgs> ConnectionStateChanged { get; } = new Subject<ConnectionStateEventArgs>();

        public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState)
        {
            device?.InvokeErrorReceived(GattOperation.Connection, device.Uuid, (int) status);
            this.ConnectionStateChanged.OnNext(new ConnectionStateEventArgs(gatt, status, newState));
        }
    }
}