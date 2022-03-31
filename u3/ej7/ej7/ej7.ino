void setup() {
  Serial.begin(115200);

}

void loop() {
  //vamos a transmitir el 16205
  static int32_t x = 0x01023F4D;

  if (Serial.available()) {
    if (Serial.read() == 0x73) {
//little end
      /*
        Serial.write((uint8_t)( x & 0x000000FF));
        Serial.write((uint8_t)((x >> 8) & 0x000000FF));
        Serial.write((uint8_t)((x >> 16) & 0x000000FF));
        Serial.write((uint8_t)((x >> 24)));
      */

      // Serial.write( (uint8_t *)( &x , 4 );
// big end
      /*
        Serial.write((uint8_t)((x >> 24)));
        Serial.write((uint8_t)((x >> 16) & 0x000000FF));
        Serial.write((uint8_t)((x >> 8) & 0x000000FF));
        Serial.write((uint8_t)( x & 0x000000FF));
      */

        for(int i=3 ; i >= 0 ; i++){
          Serial.write( ((uint8_t *) &x) + i, 1 );
        }
    }
  }
}
