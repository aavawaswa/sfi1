using System;
using System.IO.Ports;

namespace serialRFID
{
    class Program{
            static void Main(string[] args)
            {
                SerialPort _serialPort = new SerialPort();
                // Allow the user to set the appropriate properties.
                _serialPort.PortName = "/dev/ttyACM0";
                _serialPort.BaudRate = 115200;
                _serialPort.DtrEnable = true;
                _serialPort.Open();
                byte[] data = {0x73};
                _serialPort.Write(data,0,1);
                byte[] buffer = new byte[12];

                while(true){
                    if(_serialPort.BytesToRead >= 12){
                        _serialPort.Read(buffer,0,12);

                        float x = System.BitConverter.ToSingle(buffer,0);
                        Console.WriteLine(x);

                        float y = System.BitConverter.ToSingle(buffer,4);
                        Console.WriteLine(y);

                        int z = System.BitConverter.ToInt32(buffer,8);
                        Console.WriteLine(z);
                        
                    }
                }
            }
        }
}