
using System;
using System.IO.Ports;

public enum StatesSerialCom { INIT, WAIT_CASE, ANSWER_POS, ANSWER_NEG };
namespace serialBinary
{
    class Program
    {
        static void Main(string[] args)
        {

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
                    }
                    //ECRIBE EL PAQUETE
                    state = StatesSerialCom.INIT;

                    break;
                case ANSWER_NEG:
                int counter = 0;
                while (counter != 3){
                    Serial.write(0xB0);
                    
                    if (_serialPort.BytesToRead >= 17)
                    {
                        _serialPort.Read(buffer, 0, 17);
                        counter = counter + 1;

                     
                    }
                }
                   if (counter == 3)
                        {
                            state = StatesSerialCom.INIT;

                        }

                    break;
            }
        }
    }
}













/*   while(true){
                    if(_serialPort.BytesToRead >= 17){
                        _serialPort.Read(buffer,0,17);

                        if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key;
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.R)
                    {
                        _serialPort.WriteLine("read");
                        string message = _serialPort.ReadLine();
                        Console.WriteLine(message);

                    }else if (key.Key == ConsoleKey.E)
                    {
                        _serialPort.WriteLine("outON");
                        string message = _serialPort.ReadLine();
                        Console.WriteLine(message);

                    }
                }
            }
        }
*/
