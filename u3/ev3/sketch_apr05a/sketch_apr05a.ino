#define LED_WRONG 14

void taskComp();

void setup() {
  void taskComp();
}

void loop() {
  void taskComp();
}
void taskComp() {
  enum class StateTaskComp {INIT,SEND, WAITING_2A, WAITING_ANSWER, REPORT_ERROR};
  static StateTaskComp state =  StateTaskComp::INIT;
  static uint8_t bufferRx[3] = {0};
  static uint8_t dataCounter = 0;
  static uint8_t bufferTx[3];

  switch (state) {
    case StateTaskComp :: INIT: {
        Serial.begin(115200);
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
        //mandar los 17 bits
        state = StateTaskComp :: WAITING_ANSWER;
        break;
      }
    case StateTaskComp :: WAITING_ANSWER: {
        if (Serial.available()) {
          if (Serial.read() == 0x3E) {
            state = StateTaskComp :: WAITING_2A;
          }
          if ((Serial.read() == 0xB0) && (dataCounter =!3)) {
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
        digitalWrite(LED_WRONG, HIGH);
        delay(3000);
        state = StateTaskComp :: WAITING_2A;
        break;
      }
  }
}
