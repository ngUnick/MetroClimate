#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <DHT.h>

#define DHT_SENSOR_PIN  D7
#define DHT_SENSOR_TYPE DHT11

// WiFi credentials
const char* ssid = "thankarezosP";
const char* password = "thanasiskaa";

DHT dht_sensor(DHT_SENSOR_PIN, DHT_SENSOR_TYPE);
WiFiClient wifiClient;

void setup() {
  Serial.begin(115200);
  dht_sensor.begin();
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
}

void loop() {
  float humidity = dht_sensor.readHumidity();
  float temperatureC = dht_sensor.readTemperature();
  float temperatureF = dht_sensor.readTemperature(true);

  if (isnan(temperatureC) || isnan(temperatureF) || isnan(humidity)) {
    Serial.println("Failed to read from DHT sensor!");
  } else {
    if (WiFi.status() == WL_CONNECTED) {
      HTTPClient http;
      String postData = "{\"StationId\": 1, \"SensorId\": 1, \"Value\": " + String((double)temperatureC) + "}";

      http.begin(wifiClient, "http://192.168.132.106:5205/Station"); // Replace "your-server-ip" with the actual server IP
      http.addHeader("Content-Type", "application/json");

      int httpCode = http.POST(postData);
      String payload = http.getString();

      Serial.println(httpCode); // Print HTTP return code
      Serial.println(payload); // Print request response payload

      http.end(); // Close connection
    } else {
      Serial.println("Error in WiFi connection");
    }
  }

  delay(10000); // Corrected to actually wait for 10 seconds
}
