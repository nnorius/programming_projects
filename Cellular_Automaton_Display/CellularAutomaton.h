#ifndef CELLULARAUTOMATON_H_
using namespace std;
#include <string>
#include "GraphicsClient.h"

class CellularAutomaton{

    private:
        int wrap;//always 1
        unsigned char *ca;
        unsigned char *origca;//copy of original ca loaded
        unsigned char quiescent;
        int size;//cell pixel size
        int gap;//size gap between cells
        int sx;//The number of pixels before ca screen is printed x
        int sy; //the number of pixels before ca screen is printed y
        int width;//num rows ca
        int height;//num columns ca
        

    public:
        
        //Constructor
        CellularAutomaton(string filename, int quiescent);

        //Copy Constructor
        CellularAutomaton(const CellularAutomaton &rhs);

        // Destructor
        ~CellularAutomaton();

        //Assignment=
        const CellularAutomaton &operator = (const CellularAutomaton &rhs);

        //A step function that takes a single argument that is the rule function and performs one step of the 2DCA.
        void step2DCA( unsigned char (*rule)(unsigned char*, int x, int y, int w, int h));

        // display the 2DCA on the attached graphics window associated with the GraphicsClient object. 
        // The size of the cell displayed on the window is dependent on the size of the 2DCA width 
        void display(GraphicsClient & cg);

        //initilizes 2dca and sets each cell to the state read from the inputted file.
        void read2DCA(string filepath);

        
        //dynamically creates a 2DCA
        void create2DCA( int w, int h);

        //set state of single cell of 2dca
        void set2DCACell(unsigned int x, unsigned int y, unsigned char state);

        //prints 2dca to stdout for testing
        void displayCA(unsigned char *dca);

        //reset ca to original state
        void resetCA();

        //return deep copy of parameter
        unsigned char * copyAutomaton(unsigned char *);

        //initialize ca states to random 0,1 if -1, or indicated state otherwise
        void initCA(int state);

        //get state of cell at coordinates i,j
        unsigned char getCell(int i, int j);

        int getSize();
        int getGap();
        int getsx();
        int getsy(); 
        int getwidth();
        int getheight();

};

//implements game of life rule, located in casimulator.cpp
unsigned char gameoflife(unsigned char* ca, int x, int y, int width, int height);

//decides what to do after button is clicked. located in casimulator.cpp
void detectButton(int x, int y, GraphicsClient & gc, CellularAutomaton & c);

#define CELLULARAUTOMATON_H_
#endif