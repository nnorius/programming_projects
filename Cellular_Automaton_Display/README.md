CELLULAR AUTOMATON DISPLAY

DESCRIPTION

This project creates a 2 dimensional cellular automaton read from a file and displays it on the
graphics server. Black cells are of value 0 and are dead. White cells are value 1 and are alive. It implements Conway's Game of Life for each step.

The main program initializes a 2d automaton read from a file using a command line argument to specify the filepath. It creates a GUI on the graphics server and displays the automaton.

Button Guide:
STEP.  Exceutes one step of the CA and displays the result.
RUN. Continulously runs (steps) the CA at a rate of approximately 1 step every 100 ms.  
PAUSE.  If the CA is in the run mode, the CA will stop running.
QUIT. Terminates and exits the program. 
RESET. Sets the state of the CA back to the initial state when it was loaded
LOAD.  Uses a “file browser” to select a file to load.
CLEAR.  Sets all the cells to state 0.
RANDOMIZE.  Sets the cells in the CA to random initial states.

Clicking a square in the display will change it to from it's value of 1 or 0 to the opposite, 0 or 1.

COMPILE INSTRUCTIONS

Run the graphics server in Windows by navigating to the file in Command prompt and typing: java -jar GraphicsServerV2.jar

To compile the program type 'make' in your compiler. The program compiles to a file called gol. Make sure the graphics server is running and then enter the following arguments: ./gol filepath (you must have exactly this many arguments)

Two example files are included in repository that can be run, ggl.txt and glider.txt

<br>

https://user-images.githubusercontent.com/128853412/227744500-d858eef1-a3b0-417e-a562-55088528e631.mp4

<br>
The file used in the video is ggl.txt
