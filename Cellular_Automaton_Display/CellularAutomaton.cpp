#include "CellularAutomaton.h"
#include <iostream>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>

using namespace std;

CellularAutomaton::CellularAutomaton(string filename, int qstate){

    read2DCA(filename);
    quiescent = qstate;
    wrap = 1;
    size = 0;
    gap = 0;
    sx = 0;
    sy = 0;
}

//Copy Constructor
CellularAutomaton::CellularAutomaton(const CellularAutomaton &origObject){
    
    wrap = origObject.wrap;
    ca = new unsigned char[origObject.width * origObject.height];
    quiescent = origObject.quiescent;
    width = origObject.width;
    height = origObject.height;

    for(int i=0; i < height; i++) {
        for(int j=0; j < width; j++) {
            (*(ca + (i*(width)) + j)) = (*(origObject.ca + (i*(width)) + j));
               
        }
    }

}

unsigned char * CellularAutomaton::copyAutomaton(unsigned char* ca1){
    unsigned char *dca = new unsigned char[width * height];

    for(int i=0; i < height; i++) {
            for(int j=0; j < width; j++) {
               
                (*(dca + (i*(width)) + j)) = (*(ca1 + (i*(width)) + j));  
            }
        }
    return dca;
}

// Destructor
CellularAutomaton::~CellularAutomaton(){
    delete(ca);
}


//Assignment=
const CellularAutomaton & CellularAutomaton::operator = (const CellularAutomaton &origObject){
   
    if(this != &origObject){               //make sure it's not the same object (case a = a)
        
        wrap = origObject.wrap;
        ca = new unsigned char[origObject.width * origObject.height];
        quiescent = origObject.quiescent;
        width = origObject.width;
        height = origObject.height;
        unsigned char cur;
       
        for(int i=0; i < height; i++) {
            for(int j=0; j < width; j++) {
                cur = (*(origObject.ca + (i*(width)) + j));
                // (*(ca + (i*(width)) + j)) = (*(origObject.ca + (i*(width)) + j));
                (*(ca + (i*(width)) + j)) = cur;
                
            }
        }
    }
    return *this;
}

//A step function that takes a single argument that is the rule function and performs one step of the 2DCA.
void CellularAutomaton::step2DCA(unsigned char (*rule)(unsigned char*, int x, int y, int w, int h)){
    //temp arr to hold results
    unsigned char temp[width][height];

    for(int i = 0; i < width; i ++){ 
        for(int j = 0; j < height; j ++){
            temp[i][j]= gameoflife(ca, i, j, width, height);
        }
    }

    //copy temp to ca
    // unsigned char *cap = (unsigned char *) cadata;
    for(int i = 0; i < width; i ++){ 
        for(int j = 0; j < height; j ++){
            *(ca + (i*width)+j) = temp[i][j];
        }
    }
        

}



// display the 2DCA on the attached graphics window associated with the GraphicsClient object. 
// The size of the cell displayed on the window is dependent on the size of the 2DCA width and height
void CellularAutomaton::display(GraphicsClient & cg){

    if(cg.getDisplay() == 0){
       if(max(width, height) < 51 && max(width, height) > 0){
           
            size = 10;
            gap = 2;
        }else if(max(width, height) < 101){
            
            size = 4;
            gap = 1;
        
        }else if(max(width, height) < 201){
         
            size = 2;
            gap = 1;
        
        }else if(max(width, height) < 601){
        
            size = 4;
            gap = 1;
        
        }else{
            cout << "Invalid dimensions to display" << endl;
            return;
        }
    }else{
        
       if(cg.getDisplay() == 40){

            gap = 2;
            size = 40 / (max(width, height)+gap);
            
        }else if(cg.getDisplay() == 150){

            gap = 1;
            size = 150 / (max(width, height)+gap);
            
        }else if(cg.getDisplay() == 600){
            gap = 1;
            size = 600 / (max(width, height)+gap);
        
        }else{
            cout << "Invalid dimensions to display" << endl;
            return;
        }
   }
   
    sx = (600-(width*(size+gap)))/2;
    sy = (600-(height*(size+gap)))/2;

    for(int j = 0; j < height; j++) {

            for(int i=0; i < width; i++) {

                if(*(ca + (j*width) + i) == 1){
                    cg.setDrawingColor(255,255,255); //white
                    cg.fillRectangle(((gap+size)*i) + sx, ((gap+size)*j) + sy, size, size);
    
                }else{
                    cg.setDrawingColor(0,0,0); //black
                    cg.fillRectangle(((gap+size)*i) + sx, ((gap+size)*j) + sy, size, size);
                    
                }

            }
        }
  
}

//dynamically allocate a 2dca with states set to 0 and initialize struct memebers 
void CellularAutomaton::create2DCA( int w, int h){
    ca = new unsigned char [w*h];
    
    if(!ca){
        fprintf(stderr,"ca initialization failed\n");
        exit(1);
    } 
  
    height = h;
    width = w;

    for(int i=0; i < height; i++) {
        for(int j=0; j < width; j++) {
            (*(ca + (i*(width)) + j)) = 0;
               
        }
    }

}

void CellularAutomaton::set2DCACell(unsigned int x, unsigned int y, unsigned char state){
    
    *(ca + (y*width) + x) = (unsigned char)state;
    
}

//initilizes 2dca and sets each cell to the state read from the inputted file.
void CellularAutomaton::read2DCA(string filepath){
    const char* filep = filepath.c_str();
    FILE *fp;

    if ((fp = fopen(filep, "r")) == NULL) {
        printf("File cannot be opened.");
        exit(1);
    }

    int rows; //# rows = height
    int cols; //# cols = width
    fscanf(fp, "%d %d", &rows, &cols);
    
    create2DCA(cols, rows);//intialize dca
    int i = 0;
    int j = 0;
    while (1){

    int c;
    if (fscanf(fp, "%d", &c) != 1){
        break;        
    }
     
        if(i != rows){

            set2DCACell(j, i, (unsigned char)c); //set cells to file states
        }
        
        j++;
        if(j == cols){
            j = 0;
            i ++;
        }
         
    }
    origca = copyAutomaton(ca);

    fclose(fp);
   
}


//prints 2 dimension DCA in matrix format with a space between each cell (used for testing)
void CellularAutomaton::displayCA(unsigned char *dca){
  
        for(int i=0; i < height; i++) {
            printf("\n");
            for(int j=0; j < width; j++) {
                printf("%d ", *(dca + (i*(width)) + j));
            }
        }
        printf("\n");
    
}

void CellularAutomaton::resetCA(){
    ca = copyAutomaton(origca);
}

//initializes cellular automaton to random 0, 1 states if state = -1 (quiescent state = 0), 
// or quiescent state if not
void CellularAutomaton::initCA(int state){
    if(state == -1){ 
      srand(time(0));
      quiescent = 0;
      // rand() % (max_number + 1 - minimum_number) + minimum_number 
      for(int i=0; i < height; i++) {
        for(int j=0; j < width; j++) {
            (*(ca + (i*(width)) + j)) = (unsigned char)rand()%2;
        }
      }
    }else{
      
       for(int i=0; i < height; i++) {
            for(int j=0; j < width; j++) {
                (*(ca + (i*(width)) + j)) = (unsigned char)quiescent;
                  
            }
        }
      }
    

} 

unsigned char CellularAutomaton::getCell(int i, int j){
    return *(ca + (j*(width)) + i);
}
int CellularAutomaton::getSize(){
    return size;
}
int CellularAutomaton::getGap(){
    return gap;
}
int CellularAutomaton::getsx(){
    return sx;
}
int CellularAutomaton::getsy(){
    return sy;
}
int CellularAutomaton::getwidth(){
    return width;
}
int CellularAutomaton::getheight(){
    return height;
}


