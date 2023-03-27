#include <iostream>
#include "GraphicsClient.h"
#include "CellularAutomaton.h"
#include <stdio.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <string.h>
#include <sys/ioctl.h>

using namespace std;

int main(int argc, char *argv[]){  

// expects 2 arguments: ./gol filepath/
  if(argc != 2){
        printf("Invalid number of arguments\n");
        return 0;
    }

    char *filepath = argv[1];
    
    CellularAutomaton* c = new CellularAutomaton(filepath, 0);
    GraphicsClient* x = new GraphicsClient("127.0.0.1", 7777);
    x->loadGUI();
  
    c->display(*x);

    int sockfd = x->getSockfd();
    
    struct timespec time;
    time.tv_sec = 0;
    time.tv_nsec = 100000000;

    while(!x->getQuit()){

     int click = 0;
     char buf[300]; 
     int count;
    
      ioctl(sockfd, FIONREAD, &count);
      if(count > 0){

        read(sockfd, buf, count);
        click = x->interpretMessage(buf, count);
        if(click == 3){

          detectButton(x->getX(), x->getY(), *x, *c);

        }else if(click==10){

          c->read2DCA(x->interpretFile(buf, count));
          x->clear();
          x->loadGUI();
          c->display(*x);
        }
        
      }
      //run loop
      while(x->getRun()){
        c->step2DCA(gameoflife);
        c->display(*x);
        nanosleep(&time, &time);

        ioctl(sockfd, FIONREAD, &count);
        
        if(count > 0){
          read(sockfd, buf, count);
          click = x->interpretMessage(buf, count);

          if(click == 10){

            c->read2DCA(x->interpretFile(buf, count));
            x->clear();
            x->loadGUI();
            c->display(*x);

          }else{
            detectButton(x->getX(), x->getY(), *x, *c);
          }
        }


      }
      
    }
   
    delete(c);
    delete(x);
   
    return 0;
}




//implements game of life rule
unsigned char gameoflife(unsigned char* ca, int x, int y, int width, int height){

    // filepath will be automatically loaded as the default ca displayed
    unsigned char cur;
    int live = 0;
    int newX, newY;
    
    //current cell is [x][y]. Loop through surrounding 8 cells
    for(int i = x-1; i < x+2; i ++ ){
        for(int j = y-1; j < y+2; j ++){
          if(!(i == x && j ==y)){
            newX = (i+width)%(width);
            newY = (j+height)%(height);
            cur = *(ca+(newX*width)+newY);
            if(cur == 1) live ++;
          }
        }
    }
    if(*(ca+(x*width)+y) == 0){ //if 3 live neighbors becomes alive
        if(live == 3) return 1;
    }else if (*(ca+(x*width)+y) == 1){ //if 2 or 3 live neighbors stays alive. 
        if(live == 2 || live == 3) return 1;
    }
    return 0; //else dies or stays dead
}


void detectButton(int x, int y, GraphicsClient & gc, CellularAutomaton & c){
    if(x>608 && x<708 && y>8 && y<34){//LOAD
        gc.requestFile();
    }
    else if(x>608 && x<708 && y>42 && y<68){//RUN
        gc.setRun(true);
    }
     else if(x>608 && x<708 && y>76 && y<102){//STEP
        c.step2DCA(gameoflife);
        c.display(gc);
    }
     else if(x>608 && x<708 && y>110 && y<136){//PAUSE
        gc.setRun(false);
    }
     else if(x>608 && x<708 && y>144 && y<170){//CLEAR
        c.initCA(0);
        c.display(gc);
    }
     else if(x>608 && x<708 && y>178 && y<204){//RESET
        c.resetCA();
        c.display(gc);
    }
     else if(x>608 && x<708 && y>212 && y<238){//RANDOM
        c.initCA(-1);
        c.display(gc);
    }
     else if(x>608 && x<708 && y>246 && y<272){//QUIT
        gc.clear();
        gc.setQuit(true);
    }
    //size buttons
    else if(x>624 && x<654 && y>550 && y<580){//1
        gc.clear();
        gc.loadGUI();
        gc.setDisplyDim(600);
        c.display(gc);
    }
     else if(x>678 && x<708 && y>550 && y<580){//2
        gc.clear();
        gc.loadGUI();
        gc.setDisplyDim(150);
        c.display(gc);
    }
     else if(x>732 && x<762 && y>550 && y<580){//3
        gc.clear();
        gc.loadGUI();
        gc.setDisplyDim(40);
        c.display(gc);
    }else if(x > c.getsx() && x <(600-c.getsx()) && y > c.getsy() && y < (600-c.getsy())){
      
        int cellsize = c.getSize() + c.getGap();
        int x0 = c.getwidth(); //row index
        int y0 = c.getheight(); //column
      
       
        int i = 600-c.getsx();
        while(i > gc.getX()){
          x0--;
          i -= cellsize;
      
        }
        
        i = 600-c.getsy();
        while(i > gc.getY()){
          y0--;
          i -= cellsize;
          
        }
        
        unsigned char cellval = c.getCell(x0, y0);
       
        if(cellval == 0){
         
          c.set2DCACell(x0, y0, 1);
          }
        else if(cellval == 1){
          
          c.set2DCACell(x0, y0, 0);
          }
        c.display(gc);


      }


}