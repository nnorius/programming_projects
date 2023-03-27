#include <iostream>
#include "GraphicsClient.h"
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <iomanip>

using namespace std;

//Constructor
GraphicsClient::GraphicsClient(string url, int portno){
    
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0)
    {
        fprintf( stderr, "Error creating socket\n");
        exit(-1);
    }

    const char* urlc = url.c_str();
    struct sockaddr_in serv_addr;
    memset(&serv_addr, '0', sizeof(serv_addr));

    serv_addr.sin_family = AF_INET;
    serv_addr.sin_port = htons(portno);

    if(inet_pton(AF_INET, urlc, &serv_addr.sin_addr)<=0)
    {
        fprintf(stderr, "Invalid address/ Address not supported \n");
        exit(-1);
    }

    if (connect(sockfd, (struct sockaddr *)&serv_addr, sizeof(serv_addr)) < 0)
    {
        fprintf(stderr, "Connection Failed \n");
        exit(-1);
    }
    setBackgroundColor(0,0,0);
    setDrawingColor(255,255,255);
    repaint();
 
    ip = url;
    port = portno;
    run = false;
    quit = false;
    setDisplyDim(0);
}

// Copy constructor
GraphicsClient::GraphicsClient(const GraphicsClient &origObject)
:GraphicsClient::GraphicsClient(origObject.ip, origObject.port)
{
   
}

//Destructor
GraphicsClient::~GraphicsClient(){
    close(sockfd);
}

const GraphicsClient & GraphicsClient::operator = (const GraphicsClient &rhs){
   
    if(this != &rhs ){
        close(sockfd);
        GraphicsClient *c = new GraphicsClient(rhs.ip, rhs.port);
        return *c;
    }
        
    return rhs;
}

//sets the background color of the associated display. The parameters are the red, green and then blue values
void GraphicsClient::setBackgroundColor(int red, int green, int blue){

    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x00;
    message[4] = 0x07;
    message[5] = 0x02;
    message[6] = (red>>4)&0xf;
    message[7] = (red)&0xf;
    message[8] = (green>>4)&0xf;
    message[9] = (green)&0xf;
    message[10] = (blue>>4)&0xf;
    message[11] = (blue)&0xf;

    send(sockfd, message, 12, 0);
    repaint();
    clear();
}

//set the color that objects will be drawn at until another setDrawingColor call is made. The parameters are red, green and blue.
void GraphicsClient::setDrawingColor(int red, int green, int blue){
    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x00;
    message[4] = 0x07;
    message[5] = 0x06;
    message[6] = (red>>4)&0xf;
    message[7] = (red)&0xf;
    message[8] = (green>>4)&0xf;
    message[9] = (green)&0xf;
    message[10] = (blue>>4)&0xf;
    message[11] = (blue)&0xf;

    send(sockfd, message, 12, 0);

}

//clears the display to the background color.
void GraphicsClient::clear(){
    char message[100];
    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x00;
    message[4] = 0x01;
    message[5] = 0x01;
    send(sockfd, message, 6, 0);
    repaint();
}

//sets the pixel at the location given by the first two
// parameters to the color given by the last three parameters. The first two
// parameter are the location given by the x and then y coordinate. The last three
// parameters are the color given by red, green, and blue in that order.
void GraphicsClient::setPixel(int x, int y, int red, int green, int blue){

    char message[100];
    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x00;
    message[4] = 0x0F;
    message[5] = 0x03;

    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x >>4)&0x0F;
    message[9] = (x)&0x0F ;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    message[14] = (red>>4)&0xf;
    message[15] = (red)&0xf;
    message[16] = (green>>4)&0xf;
    message[17] = (green)&0xf;
    message[18] = (blue>>4)&0xf;
    message[19] = (blue)&0xf;

    send(sockfd, message, 20, 0);
    repaint();
}

//draws a rectangle at the specified coordinates given by the first two parameters of the specified size given by the last two
//parameters. The first two parameters are the x and y coordinate in that order, and the last two parameters are the width 
//and height, also given in that order.
void GraphicsClient::drawRectangle(int x, int y, int width, int height){
    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x01;
    message[4] = 0x01;
    message[5] = 0x07;

    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x >>4)&0x0F;
    message[9] = (x)&0x0F ;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    message[14] = (width >> 12) & 0x0F;
    message[15] = (width >> 8) & 0x0F;
    message[16] = (width>>4)&0x0F;
    message[17] = (width)&0x0F;

    message[18] = (height >> 12) & 0x0F;
    message[19] = (height >> 8) & 0x0F;
    message[20] = (height>>4)&0x0F;
    message[21] = (height)&0x0F;

    send(sockfd, message, 22, 0);
    repaint();
}

//draws a filled rectangle at the position and size given by the parameters. 
//The parameters are the same as the drawRectangle parameters.
void GraphicsClient::fillRectangle(int x, int y, int width, int height){
    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x01;
    message[4] = 0x01;
    message[5] = 0x08;

    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x >>4)&0x0F;
    message[9] = (x)&0x0F ;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    message[14] = (width >> 12) & 0x0F;
    message[15] = (width >> 8) & 0x0F;
    message[16] = (width>>4)&0x0F;
    message[17] = (width)&0x0F;

    message[18] = (height >> 12) & 0x0F;
    message[19] = (height >> 8) & 0x0F;
    message[20] = (height>>4)&0x0F;
    message[21] = (height)&0x0F;

    send(sockfd, message, 22, 0);
    repaint();
}

//Sets pixels to background color at location and size specified by parameters.
void GraphicsClient::clearRectangle(int x, int y, int width, int height){
    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x01;
    message[4] = 0x01;
    message[5] = 0x09;

    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x >>4)&0x0F;
    message[9] = (x)&0x0F ;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    message[14] = (width >> 12) & 0x0F;
    message[15] = (width >> 8) & 0x0F;
    message[16] = (width>>4)&0x0F;
    message[17] = (width)&0x0F;

    message[18] = (height >> 12) & 0x0F;
    message[19] = (height >> 8) & 0x0F;
    message[20] = (height>>4)&0x0F;
    message[21] = (height)&0x0F;

    send(sockfd, message, 22, 0);
    repaint();
}

//Draw oval at specified location in inscribed in a rectangle of the specified size. Params same as draw rectangle
void GraphicsClient::drawOval(int x, int y, int width, int height){
    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x01;
    message[4] = 0x01;
    message[5] = 0x0A;

    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x >>4)&0x0F;
    message[9] = (x)&0x0F ;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    message[14] = (width >> 12) & 0x0F;
    message[15] = (width >> 8) & 0x0F;
    message[16] = (width>>4)&0x0F;
    message[17] = (width)&0x0F;

    message[18] = (height >> 12) & 0x0F;
    message[19] = (height >> 8) & 0x0F;
    message[20] = (height>>4)&0x0F;
    message[21] = (height)&0x0F;

    send(sockfd, message, 22, 0);
    repaint();
}

//Draw filled oval at specified location in inscribed in a rectangle of the specified size.
void GraphicsClient::fillOval(int x, int y, int width, int height){
    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x01;
    message[4] = 0x01;
    message[5] = 0x0B;

    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x >>4)&0x0F;
    message[9] = (x)&0x0F ;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    message[14] = (width >> 12) & 0x0F;
    message[15] = (width >> 8) & 0x0F;
    message[16] = (width>>4)&0x0F;
    message[17] = (width)&0x0F;

    message[18] = (height >> 12) & 0x0F;
    message[19] = (height >> 8) & 0x0F;
    message[20] = (height>>4)&0x0F;
    message[21] = (height)&0x0F;

    send(sockfd, message, 22, 0);
    repaint();
}

//draws a line starting a point 1 and ending at point 2. Point 1 is given by the first two parameters,
//x and y, in that order, and point 2 is given by the last two parameters, x followed by y.
void GraphicsClient::drawLine(int x1, int y1, int x2, int y2){

    char message[100];

    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x01;
    message[4] = 0x01;
    message[5] = 0x0D;

    message[6] = (x1 >> 12) & 0x0F;
    message[7] = (x1 >> 8) & 0x0F;
    message[8] = (x1>>4)&0x0F;
    message[9] = (x1)&0x0F ;

    message[10] = (y1 >> 12) & 0x0F;
    message[11] = (y1 >> 8) & 0x0F;
    message[12] = (y1>>4)&0x0F;
    message[13] = (y1)&0x0F ;

    message[14] = (x2 >> 12) & 0x0F;
    message[15] = (x2 >> 8) & 0x0F;
    message[16] = (x2>>4)&0x0F;
    message[17] = (x2)&0x0F ;

    message[18] = (y2 >> 12) & 0x0F;
    message[19] = (y2 >> 8) & 0x0F;
    message[20] = (y2>>4)&0x0F;
    message[21] = (y2)&0x0F;

    send(sockfd, message, 22, 0);
    repaint();
}

//draws a string of characters on the display given by the last parameter at 
//the position given by the first two parameters, x, y, in that order. 
void GraphicsClient::drawstring(int x, int y, string text){

    char message[200];
    int length = text.length()*2;
    length += 9;


    message[0] = 0xFF;
    message[1] = (length >> 12) & 0x0F;
    message[2] = (length >> 8) & 0x0F;
    message[3] = (length>>4)&0x0F;
    message[4] = (length)&0x0F;//length

    message[5] = 0x05;//command
    
    message[6] = (x >> 12) & 0x0F;
    message[7] = (x >> 8) & 0x0F;
    message[8] = (x>>4)&0x0F;
    message[9] = (x)&0x0F;

    message[10] = (y >> 12) & 0x0F;
    message[11] = (y >> 8) & 0x0F;
    message[12] = (y>>4)&0x0F;
    message[13] = (y)&0x0F;

    int count = 14;
    char c;
    for(int i = 0; i < text.length(); i++){
        c=text.at(i);
       
        message[count] = (c>>4)&0x0F;
        count ++;
        message[count] = (c)&0x0F;
        count++;
     
    }

    send(sockfd, message, length+5, 0);
    repaint();
}

//send the redraw (repaint) signal to the attached graphics server.
void GraphicsClient::repaint(){
    char message[100];
    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x00;
    message[4] = 0x01;
    message[5] = 0x0C;
    send(sockfd, message, 6, 0);
}

void GraphicsClient::loadGUI(){
    setBackgroundColor(0,0,0);
    setDrawingColor(120,134,107);
    fillRectangle(600,0, 200, 600);

    setDrawingColor(255,255,255);
   
    int w = 608;
    int h = 8; 
    for(int i = 0; i < 8; i++){
      fillRectangle(w, h, 100, 26);
      h += 34;
    }
    //size buttons
    fillRectangle(624, 550, 30, 30);
    fillRectangle(624+54, 550, 30, 30);
    fillRectangle(624+108, 550, 30, 30);

    setDrawingColor(0,0,0);
    drawstring(624, 26, "LOAD");
    drawstring(624, 60, "RUN");
    drawstring(624, 94, "STEP");
    drawstring(624, 128, "PAUSE");
    drawstring(624, 162, "CLEAR");
    drawstring(624, 196, "RESET");
    drawstring(624, 230, "RANDOM");
    drawstring(624, 264, "QUIT");

    drawstring(650, 525, "SELECT SIZE");
    drawstring(636, 570, "1");
    drawstring(636+54, 570, "2");
    drawstring(636+108, 570, "3");
}

void GraphicsClient::requestFile(){
    char message[100];
    message[0] = 0xFF;
    message[1] = 0x00;
    message[2] = 0x00;
    message[3] = 0x00;
    message[4] = 0x01;
    message[5] = 0x0E;
    send(sockfd, message, 6, 0);
}

int GraphicsClient::getSockfd(){
    return sockfd;
}

bool GraphicsClient::getRun(){
    return run;
}

void GraphicsClient::setRun(bool isrunning){
    run = isrunning;
}

bool GraphicsClient::getQuit(){
    return quit;
}

void GraphicsClient::setQuit(bool quitClicked){
    quit = quitClicked;
}

int GraphicsClient::getX(){
    return x;
}

int GraphicsClient::getY(){
    return y;
}

//interpret file path
string GraphicsClient::interpretFile(char buf[], int count){
        char c;
        string hex = "";
        for(int i=6 ; i<count; i += 2){ 
            c = (buf[i] << 4) | buf[i+1];
            hex += c;
            if(c == '/' || c == '\\' || c ==':'){
                hex = "";
            }
        } 
        return hex;
}

//return 10 if file, return type (1, 2, 3) if button click
int GraphicsClient::interpretMessage(char buf[], int count){
  
    char co[4] = {0};
    co[3] = buf[5];
    int messagetype = hexToInt(co);

    if(messagetype == 10){ //is filepath

        return 10;

    }else{  //is button click
        
        cout << endl;
        co[3] = buf[5];
        int buttonType = hexToInt(co);
        co[3] = buf[6];
        
        co[0]=buf[7];
        co[1]=buf[8];
        co[2]=buf[9];
        co[3]=buf[10];
        x = hexToInt(co);

        co[0]=buf[11];
        co[1]=buf[12];
        co[2]=buf[13];
        co[3]=buf[14];
        y = hexToInt(co);
        
        return buttonType;
    }
      
}

int GraphicsClient::getDisplay(){
    return display;

}

//set display dimensions
void GraphicsClient::setDisplyDim(int dim){
    display = dim;
}

int GraphicsClient::hexToInt(char val[]){

    int number = (val[3] & 0xFF)
           | ((val[2] & 0xFF) << 4) 
           | ((val[1] & 0xFF) << 8)
           | ((val[0] & 0xFF) << 12);
    return number;
}

