#ifndef GRAPHICSCLIENT_H_
using namespace std;

class GraphicsClient{
      
    private:
       
       string ip;
       int port;
       int sockfd;
       bool run;//true if run clicked
       bool quit;//true if quit clicked
       int x; //x coordinate of button click
       int y; //y coordinate of button click
       int display; //dimensions for display

      
    public:
          

        //Constructor
        GraphicsClient(string url, int portno);

        //Copy constructor
        GraphicsClient(const GraphicsClient &rhs);

        //Destructor
        ~GraphicsClient();

        //assign=
        const GraphicsClient &operator = (const GraphicsClient &rhs);

        //sets the background color of the associated display. The parameters are the red, green and then blue values
        void setBackgroundColor(int red, int green, int blue);

        //set the color that objects will be drawn at until another setDrawingColor call is made. The parameters are red, green and blue.
        void setDrawingColor(int red, int green, int blue);

        //clears the display to the background color.
        void clear();

        //sets the pixel at the location given by the first two
        // parameters to the color given by the last three parameters. The first two
        // parameter are the location given by the x and then y coordinate. The last three
        // parameters are the color given by red, green, and blue in that order.
        void setPixel(int, int, int, int, int);

        //draws a rectangle at the specified coordinates given by the first two parameters of the specified size given by the last two
        //parameters. The first two parameters are the x and y coordinate in that order, and the last two parameters are the width 
        //and height, also given in that order.
        void drawRectangle(int x, int y, int width, int height);

        //draws a filled rectangle at the position and size given by the parameters. 
        //The parameters are the same as the drawRectangle parameters.
        void fillRectangle(int, int, int, int);

        //Sets pixels to background color at location and size specified by parameters.
        void clearRectangle(int x, int y, int width, int height);

        //Draw oval at specified location in inscribed in a rectangle of the specified size. Params same as draw rectangle
        void drawOval(int, int, int, int);

        //Draw filled oval at specified location in inscribed in a rectangle of the specified size.
        void fillOval(int, int, int, int);

        //draws a line starting a point 1 and ending at point 2. Point 1 is given by the first two parameters,
        //x and y, in that order, and point 2 is given by the last two parameters, x followed by y.
        void drawLine(int, int, int, int);

        //draws a string of characters on the display given by the last parameter at 
        //the position given by the first two parameters, x, y, in that order. 
        void drawstring(int, int, string);

        //send the redraw (repaint) signal to the attached graphics server.
        void repaint();

        //draw CA GUI
        void loadGUI();

        //request file from server
        void requestFile();

        //return sockfd
        int getSockfd();

        //interpret message from server
        int interpretMessage(char buf[], int count);

        //convert 4 hex bits to int
        int hexToInt(char val[]);

        //check if stepCA is running
        bool getRun();

        //set run variable
        void setRun(bool isrunning);

        //check if quit clicked
        bool getQuit();

        //set quit variable
        void setQuit(bool quitClicked);
        
        //get x click position
        int getX();

        //get y click position
        int getY();

        //set display to 600, 150, 40, or 0 if want default
        void setDisplyDim(int display);

        //return display variable
        int getDisplay();

        //convert hex to string and return filepath
        string interpretFile(char buf[], int count);

};

#define GRAPHICSCLIENT_H_
#endif