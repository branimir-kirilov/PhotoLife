/*
 * This is arduino client (Wemos D1 based on ESP8266) that changes LED stripe colors according to percentage of code coverage.
 * For now the colors would change every 10% from 100% to 50%, and one for below 50%, so in total 6 different colors.
 * 
 * 90 - 100: 00FF00
 * 80 -  90: FFCC00
 * 70 -  80: FF9900
 * 60 -  70: FF6600
 * 50 -  60: FF3300
 *     < 50: FF0000
 *     
 *TODO: Blink when continious integration build not passing.
 */

#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>
#include<ArduinoJson.h>

/*Defining pins. Note that the are some mismatches on Wemos D1. (the retired one, not mini)*/
#define BLUEPIN 13 
#define GREENPIN 12
#define REDPIN 14 

const char* ssid = "NotYourWiFi"; //Replace with your WiFi ssid
const char* password = "bananana"; //Replace with your WiFi password

const char* host = "coveralls.io";
const int httpsPort = 443;

// SHA1 fingerprint of the certificate
const char* fingerprint = "6D A8 77 AB 48 93 2B 81 4B 93 6C 3E 53 54 8B 1A FC 61 1C 05";

//Simple function that changes the colors according to coverage
void changeLights(double coverage){
  if(coverage >= 90){
    analogWrite(REDPIN, 0);
    analogWrite(BLUEPIN, 0);
    analogWrite(GREENPIN, 255);  
  }
  else if(coverage >= 80 && coverage < 90){
    analogWrite(REDPIN, 255);
    analogWrite(BLUEPIN, 0);
    analogWrite(GREENPIN, 200);
  }
  else if(coverage >= 70 && coverage < 80){
    analogWrite(REDPIN, 255);
    analogWrite(BLUEPIN, 0);
    analogWrite(GREENPIN, 150);
  }
  else if(coverage >= 60 && coverage < 70 ){
    analogWrite(REDPIN, 255);
    analogWrite(BLUEPIN, 0);
    analogWrite(GREENPIN, 100);
  }
  else if(coverage >= 50 && coverage < 60){
    analogWrite(REDPIN, 255);
    analogWrite(BLUEPIN, 0);
    analogWrite(GREENPIN, 50);      
  }
  else{
    analogWrite(REDPIN, 255);
    analogWrite(BLUEPIN, 0);
    analogWrite(GREENPIN, 0);  
  }
}

void setup() {
  pinMode(BLUEPIN, OUTPUT);
  pinMode(GREENPIN, OUTPUT);
  pinMode(REDPIN, OUTPUT);

  analogWrite(REDPIN, 0);
  analogWrite(BLUEPIN, 0);
  analogWrite(GREENPIN, 0);  
  
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
}

void loop() {
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

  changeLights(0);
  
  Serial.println("==========");
  Serial.println("closing connection");

  //Do this every 5 minutes
  delay(300000);
}


