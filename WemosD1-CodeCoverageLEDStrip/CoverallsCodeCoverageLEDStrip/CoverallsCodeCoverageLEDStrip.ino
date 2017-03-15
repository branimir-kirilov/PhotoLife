/*
 * This is arduino client that changes LED stripe colors according to percentage of code coverage.
 * For now the colors would change every 10%, so in total 10 different colors.
 * 
 */

#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>
#include<ArduinoJson.h>

#define BLUEPIN 13
#define GREENPIN 12
#define REDPIN 14 

const char* ssid = "NotYourWiFi";
const char* password = "bananana";

const char* host = "coveralls.io";
const int httpsPort = 443;

// SHA1 fingerprint of the certificate
const char* fingerprint = "6D A8 77 AB 48 93 2B 81 4B 93 6C 3E 53 54 8B 1A FC 61 1C 05";

void setup() {
  Serial.begin(115200);
  Serial.println();
  Serial.print("connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  // Use WiFiClientSecure class to create TLS connection
  WiFiClientSecure client;
  Serial.print("connecting to ");
  Serial.println(host);
  if (!client.connect(host, httpsPort)) {
    Serial.println("connection failed");
    return;
  }

  if (client.verify(fingerprint, host)) {
    Serial.println("certificate matches");
  } else {
    Serial.println("certificate doesn't match");
  }

  String url = "/github/Branimir123/PhotoLife.json";
  Serial.print("requesting URL: ");
  Serial.println(url);

  client.print(String("GET ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "User-Agent: BuildFailureDetectorESP8266\r\n" +
               "Connection: close\r\n\r\n");

  Serial.println("request sent");
  while (client.connected()) {
    String line = client.readStringUntil('\n');
    if (line == "\r") {
      Serial.println("headers received");
      break;
    }
  }
  String line = client.readStringUntil('\n');
  Serial.println("reply was:");
  Serial.println("==========");
  Serial.println(line);

  char jsonArray[500];
  strncpy(jsonArray, line.c_str(), sizeof(jsonArray));
  jsonArray[sizeof(jsonArray) - 1] = 0;
  
  StaticJsonBuffer<500> jsonBuffer;
  
  JsonObject& root = jsonBuffer.parseObject(jsonArray);

   if (!root.success()) {
    Serial.println("parseObject() failed");
    return;
  }
  
  double percentage = root["covered_percent"];
  
  Serial.println("==========");
  Serial.println(percentage);
  Serial.println("==========");
  Serial.println("closing connection");
}

void loop() {
}
