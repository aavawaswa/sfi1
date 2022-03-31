using System;
using System.IO.Ports;

namespace serialRFID
{
    class Program{
            static void Main(string[] args)
            {
                SerialPort _serialPort = new SerialPort();
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
                        Console.ReadKey();
                        _serialPort.Write(data,0,1);
                    }
                }
            }
        }
}