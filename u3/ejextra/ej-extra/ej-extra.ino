void setup() {
  Serial.begin(115200);

}

void loop() {
  static float x = 3.1416;
  static float y = 2.7182;
 static int32_t z = -1;


  if (Serial.available()) {
    if (Serial.read() == 0x73) {
      Serial.write( (uint8_t *) &x, 4);
      Serial.write( (uint8_t *) &y, 4);
      Serial.write( (uint8_t *) &z, 4);
    }
  }
}
