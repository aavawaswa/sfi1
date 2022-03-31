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
                byte[] buffer = new byte[4];

                while(true){
                    if(_serialPort.BytesToRead >= 4){
                        _serialPort.Read(buffer,0,4);

                        for(int i = 0;i < 4;i++){
                            Console.Write(buffer[i].ToString("X2") + " ");
                        }
                        Console.WriteLine();

                        Console.WriteLine(System.BitConverter.ToSingle(buffer,0));
                        byte [] bufferReverse = new byte[4];
                        for(int i = 3; i>= 0; i--) bufferReverse[3-i] = buffer[i];
                        Console.WriteLine(System.BitConverter.ToSingle(bufferReverse,0));

                        Console.ReadKey();
                        _serialPort.Write(data,0,1);
                    }
                }
            }
        }
}