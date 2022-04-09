
using System;
using System.IO.Ports;
using System.Collections.Generic;




namespace serialBinary
{
    public enum StatesSerialCom { INIT, WAIT_CASE, ANSWER_POS, ANSWER_NEG };
    public class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            StatesSerialCom state = StatesSerialCom.INIT;
            switch (state)
            {
                case INIT:
                    SerialPort _serialPort = new SerialPort();
                    _serialPort.PortName = "/dev/ttyACM0";
                    _serialPort.BaudRate = 115200;
                    _serialPort.DtrEnable = true;
                    _serialPort.Open();

                    byte[] data = { 0x2A };
                    byte[] buffer = new byte[17];

                    state = StatesSerialCom.WAIT_CASE;
                    break;

                case WAIT_CASE:

                    if (Console.KeyAvailable == true)
                    {
                        ConsoleKeyInfo key;
                        key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.R)
                        {

                            state = StatesSerialCom.ANSWER_POS;
                        }
                        else if (key.Key == ConsoleKey.E)
                        {

                            state = StatesSerialCom.ANSWER_NEG;
                        }
                    }

                    break;
                case ANSWER_POS:
                    if (_serialPort.BytesToRead >= 17)
                    {
                        _serialPort.Read(buffer, 0, 17);
                        Serial.write(0x3E);
                        //ECRIBE EL PAQUETE?
                    }

                    state = StatesSerialCom.INIT;

                    break;
                case ANSWER_NEG:

                    while (counter != 3)
                    {


                        if (_serialPort.BytesToRead >= 17)
                        {
                            _serialPort.Read(buffer, 0, 17);
                            _serialPort.write(0xB0);
                            counter = counter + 1;
                        }
                    }
                    if (counter == 3)
                    {
                        state = StatesSerialCom.INIT;
                        //mensaje de error?

                    }

                    break;
            }
        }
    }
}