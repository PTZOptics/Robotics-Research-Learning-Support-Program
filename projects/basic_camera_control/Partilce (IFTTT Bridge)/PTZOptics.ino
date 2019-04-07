// This #include statement was automatically added by the Particle IDE.
// Currently this code is not tested. Wanted to have something for NAB!
#include <HttpClient.h>
unsigned int nextTime = 0;    // Next time to contact the server
HttpClient http;

http_header_t headers[] = {
    { "Accept" , "*/*"},
    { NULL, NULL } // NOTE: Always terminate headers will NULL
};

http_request_t request;
http_response_t response;

void setup() {
Particle.function("control", ptzoptics);
}

void loop() {

}

int ptzoptics(String command){
     if(command == "zoomin"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&zoomin&4");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&zoomstop&4");
     }
     if(command == "zoomout"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&zoomout&4");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&zoomstop&4");
     }
     if(command == "up"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&up&10&10");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&ptzstop&4");
     }
     if(command == "down"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&down&10&10");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&ptzstop&4");
     }
     if(command == "left"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&left&10&10");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&ptzstop&4");
     }
     if(command == "right"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&right&10&10");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&ptzstop&4");
     }
     if(command == "focusin"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&focusin&4");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&focusstop&4");
     }
     if(command == "focusout"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&focusout&4");
        delay(200);
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&focusstop&4");
     }
     if(command == "focuslock"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&lock_mfocus");
     }  
     if(command == "focusunlock"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&unlock_mfocus");
     }
     if(command == "home"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&home");
     }
     if(command == "preset1"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&1");
     }
     if(command == "preset2"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&2");
     }
     if(command == "preset3"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&3");
     }
     if(command == "preset4"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&4");
     }
     if(command == "preset5"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&5");
     }
     if(command == "preset6"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&6");
     }
     if(command == "preset7"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&7");
     }
     if(command == "preset8"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&8");
     }
     if(command == "preset9"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&9");
     }
     if(command == "preset10"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&poscall&10");
     }
     if(command == "setpreset1"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&1");
     }
     if(command == "setpreset2"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&2");
     }
     if(command == "setpreset3"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&3");
     }
     if(command == "setpreset4"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&4");
     }
     if(command == "setpreset5"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&5");
     }
     if(command == "setpreset6"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&6");
     }
     if(command == "setpreset7"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&7");
     }
     if(command == "setpreset8"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&8");
     }
     if(command == "setpreset9"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&9");
     }
     if(command == "setpreset10"){
        httpcmd("/cgi-bin/ptzctrl.cgi?ptzcmd&posset&10");
     }
}



int httpcmd(String path){
        request.hostname = "172.16.206.56";
    request.port = 80;
    request.path = path;

    http.get(request, response, headers);
    Serial.print("Application>\tResponse status: ");
    Serial.println(response.status);

    Serial.print("Application>\tHTTP Response Body: ");
    Serial.println(response.body);
    return 1;
}
