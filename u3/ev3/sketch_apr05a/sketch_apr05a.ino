#define LED_WRONG 14

void taskComp();

void setup() {
  taskComp();
}

void loop() {
  taskComp();
}
void taskComp() {
  enum class StateTaskComp {INIT,  WAITING_2A, SEND, WAITING_ANSWER, REPORT_ERROR};
  static StateTaskComp state =  StateTaskComp::INIT;
  static uint8_t bufferRx[3] = {0};
  static uint8_t dataCounter = 0;
  static uint8_t checkCounter = 17;
  static uint8_t bufferTx[3];

  switch (state) {
    case StateTaskComp :: INIT: {
        Serial.begin(115200);
        pinMode(LED_WRONG, OUTPUT);

        state = StateTaskComp :: WAITING_2A;
        break;
      }
    case StateTaskComp :: WAITING_2A: {
        if (Serial.available()) {

          if (Serial.read() == 0x2A) {

            state = StateTaskComp :: SEND;
          }
        }
        break;
      }
    case StateTaskComp :: SEND: {
        int32_t ente = 86574;
        float floaty[3] = {22.34,23.22,69.42};
        for (int j = 0; j < 3; j++) {
          Serial.write ( (uint8_t *) &floaty[j], 4);
          Serial.write((uint32_t)( ente & 0x00FF));
          Serial.println (floaty[j]);
        }
        Serial.println (ent);
            uint8_t calcuChecksum = 0;
            bufferRx[0] = {( (uint8_t *) &floaty[0], 4)};
            bufferRx[4] = {( (uint8_t *) &floaty[1], 4)};
            bufferRx[8] = {( (uint8_t *) &floaty[2], 4)};
            bufferRx[12] = ente;
            for (uint8_t i = 1; i <= checkCounter - 1; i++) {
              calcuChecksum = calcuChecksum ^ bufferRx[i - 1];
            }
            Serial.write(calcuChecksum);
        state = StateTaskComp :: WAITING_ANSWER;
        break;
      }
    case StateTaskComp :: WAITING_ANSWER: {
        if (Serial.available()) {

          if (Serial.read() == 0x3E) {
            Serial.print("hola");
            state = StateTaskComp :: WAITING_2A;
          }
          else if ((Serial.read() == 0xB0) ) {
            Serial.print("hola");
            dataCounter = dataCounter + 1 ;
            state = StateTaskComp :: SEND;

          }
          if (dataCounter == 3) {
            state = StateTaskComp :: REPORT_ERROR;
          }
        }

        break;
      }
    case StateTaskComp :: REPORT_ERROR: {
        dataCounter = 0;
        //        const uint32_t interval = 500;
        //        const uint32_t interval2 = 3000;
        //        static uint32_t previousMillis = 0;
        //        static uint32_t previousMillis2 = 0;
        //        static uint8_t ledState = LOW;
        //
        //        uint32_t currentMillis = millis();
        //        uint32_t currentMillis2 = millis();
        //
        //
        //        if (currentMillis - previousMillis >= interval) {
        //          previousMillis = currentMillis;
        //          if (ledState == LOW) {
        //            ledState = HIGH;
        //          } else {
        //            ledState = LOW;
        //          }
        //          digitalWrite(LED_WRONG, ledState);
        //          //Serial.print("holaaaa");
        //        }

        digitalWrite(LED_WRONG, HIGH);
        delay(500);
        digitalWrite(LED_WRONG, LOW);
        delay(500);
        digitalWrite(LED_WRONG, HIGH);
        delay(500);
        digitalWrite(LED_WRONG, LOW);
        delay(500);
        digitalWrite(LED_WRONG, HIGH);
        delay(500);
        digitalWrite(LED_WRONG, LOW);
        state = StateTaskComp :: WAITING_2A;

        break;
      }
  }
}
