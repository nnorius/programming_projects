all:gol

gol: casimulator.o  CellularAutomaton.o GraphicsClient.o
	g++ -Wall -g -o gol casimulator.o CellularAutomaton.o GraphicsClient.o

casimulator.o: casimulator.cpp GraphicsClient.h CellularAutomaton.h
	g++ -Wall -g -c casimulator.cpp

CellularAutomaton.o: CellularAutomaton.cpp CellularAutomaton.h 
	g++ -Wall -g -c CellularAutomaton.cpp

GraphicsClient.o: GraphicsClient.cpp GraphicsClient.h
	g++ -Wall -g -c GraphicsClient.cpp
	
