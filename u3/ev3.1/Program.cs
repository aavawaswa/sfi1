using System;
using System.IO.Ports;

public enum StateSerialCom { INIT, WAIT_CASE, CASE_SEND_GOOD, CASE_SEND_BAD };


namespace SerialBinary
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables iniciale
            StateSerialCom state = StateSerialCom.INIT;

            int checkSum = 0;
            int b0Counter = 0;

            byte[] dataReqst = { 0x2a, 0x3e, 0xb0 };

            byte[] listCatchPack = new byte[17]; //byte[] bufferAi = new byte[17];

            SerialPort _serialPort = new SerialPort();

            while (true)
            {
                switch (state)
                {
                    case StateSerialCom.INIT:

                        _serialPort.PortName = "/dev/ttyACM0"; //Cambiar Direccion//
                        _serialPort.BaudRate = 115200;
                        _serialPort.DtrEnable = true;
                        _serialPort.Open();

                        state = StateSerialCom.WAIT_CASE;
                        break;
                    case StateSerialCom.WAIT_CASE:
                        if (Console.KeyAvailable == true)
                        {
                            ConsoleKeyInfo key;
                            key = Console.ReadKey(true);

                            if (key.Key == ConsoleKey.R)
                            {
                                _serialPort.Write(dataReqst, 0, 1);
                                state = StateSerialCom.CASE_SEND_GOOD;
                            }
                            else if (key.Key == ConsoleKey.E)
                            {
                                _serialPort.Write(dataReqst, 0, 1);
                                state = StateSerialCom.CASE_SEND_BAD;
                            }
                        }
                        break;
                    case StateSerialCom.CASE_SEND_GOOD:

                        if (_serialPort.BytesToRead >= 17)
                        {
                            _serialPort.Read(listCatchPack, 0, 17);

                            for (int i = 0; i < 16; i++)
                            {
                                Console.Write(listCatchPack[i].ToString("X2") + " ");
                            }
                            Console.WriteLine();

                            //el paquete llego bien? calcular checksum para saber
                            for (int i = 0; i <= 15; i++)
                            {
                                checkSum = checkSum ^ listCatchPack[i];
                            }
                            if (checkSum == listCatchPack[16])
                            {
                                Console.WriteLine("Correcto: " + checkSum + "=" + listCatchPack[16]);
                                checkSum = 0;
                                _serialPort.Write(dataReqst, 1, 1);
                                for (int i = 0; i < 16; i++)
                                {
                                    listCatchPack[i] = 0;
                                }
                                state = StateSerialCom.WAIT_CASE;
                            }
                            //else si es != entonces envio b0
                        }
                        break;
                    case StateSerialCom.CASE_SEND_BAD:

                        if (_serialPort.BytesToRead >= 17)
                        {
                            _serialPort.Read(listCatchPack, 0, 17);
                            for (int i = 0; i < 16; i++)
                            {
                                Console.Write(listCatchPack[i].ToString("X2") + " ");
                            }
                            Console.WriteLine();
                            if (checkSum != listCatchPack[16])
                            {
                                Console.WriteLine("Incorrecto: " + checkSum + "!=" + listCatchPack[16]);
                                _serialPort.Write(dataReqst, 2, 1);
                                b0Counter++;
                            }
                        }
                        if (b0Counter == 3)
                        {
                            b0Counter = 0;
                            for (int i = 0; i < 16; i++)
                            {
                                listCatchPack[i] = 0;
                            }
                            state = StateSerialCom.WAIT_CASE;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}