using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lav array
            int arraySize = 9;
            string[] playBoardSpots = new string[arraySize];

            // variabler til at kunne holde øje med score
            CurrentScores newScores = new CurrentScores();
            newScores.scoreX = 0;
            newScores.scoreO = 0;

            // gameloop
            while (true)
            {
                //reset spilleplade
                for (int i = 0; i < arraySize; i++)
                {
                    playBoardSpots[i] = " ";
                }

                // variabler til ture
                int turnCounter = 0;
                string turn = "";

                // Bed bruger om at vælge hvilken spiller starter først
                Console.Clear();
                Console.WriteLine("Welcome to tic-tac-toe");
                Console.WriteLine("Wich player starts first?");
                Console.WriteLine("Write X or O for wich player should start first:");
                string firstTurn = Console.ReadLine().ToLower();

                // Set first turn
                while (true)
                {
                    if (firstTurn == "x")
                    {
                        turn = "X";
                        break; // break loop for at forsætte
                    }
                    else if (firstTurn == "o")
                    {
                        turn = "O";
                        break; // break loop for at forsætte
                    }
                    else
                    { // output en feil melling 
                        InvalidError();
                        firstTurn = Console.ReadLine().ToLower();
                    }
                }

                // turn loop
                while (true)
                {
                    if (turnCounter == 6) // check efter om spillerne har lavet 3 ture vær
                    {
                        // Clear console og print spilleplade og scores
                        Console.Clear();
                        Console.WriteLine("Player X round won: " + newScores.scoreX);
                        Console.WriteLine("Player O round won: " + newScores.scoreO);
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        StartTurnSwitch(turn); // udskriv text til tur når spillerne har placeret 3 markers vær

                        // Når spillerne har spillet 3 gange vær skal der flyttes på brikker
                        bool changedSpot = false;
                        int sellectSpot = ChangePlayBoard(GetInputString()); // få input
                        while (changedSpot == false)
                        {
                            if (sellectSpot == 9)
                            { // fejl melling
                                InvalidError();
                                sellectSpot = ChangePlayBoard(GetInputString());
                            }
                            else if (playBoardSpots[sellectSpot] != turn)
                            { // fejl melling
                                Console.WriteLine("You dont have a mark here, try again:");
                                sellectSpot = ChangePlayBoard(GetInputString());
                            }
                            else
                            { // nu der valgt en brik at flytte og nu skal der vælges hvor den skal flyttes til
                                playBoardSpots[sellectSpot] = " "; //fjern brik fra gamle felt
                                Console.WriteLine("Write where to move your mark to:");
                                int newSpot = ChangePlayBoard(GetInputString()); // få input
                                while (true)
                                {
                                    if (newSpot == 9)
                                    { // feil melling
                                        InvalidError();
                                        newSpot = ChangePlayBoard(GetInputString());
                                    }
                                    else if (playBoardSpots[newSpot] == "X" || playBoardSpots[newSpot] == "O")
                                    { // feil melling hvis felt allerede er taget
                                        TakenError();
                                        newSpot = ChangePlayBoard(GetInputString());
                                    }
                                    else
                                    { // sæt brik på ny felt
                                        playBoardSpots[newSpot] = turn;
                                        changedSpot = true;
                                        break;
                                        
                                    }
                                }
                            }
                        }
                    }
                    else
                    { // tur til at placere brik
                        Console.Clear();
                        Console.WriteLine("Player X round won: " + newScores.scoreX);
                        Console.WriteLine("Player O round won: " + newScores.scoreO);
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        StartTurn(turn);

                        // check efter om feltet er taget og så ændre spillepladen
                        int changeSpot = ChangePlayBoard(GetInputString());
                        while (true)
                        {
                            if (changeSpot == 9)
                            { // fejl melling
                                InvalidError();
                                changeSpot = ChangePlayBoard(GetInputString());
                            }
                            else if (playBoardSpots[changeSpot] == "X" || playBoardSpots[changeSpot] == "O")
                            { // fejl melling hvis felt er taget
                                TakenError();
                                changeSpot = ChangePlayBoard(GetInputString());
                            }
                            else
                            { // placer brik
                                playBoardSpots[changeSpot] = turn;
                                turnCounter += 1;
                                break;
                            }
                        }
                    }

                    // Tjek efter om der er 3 på stribe
                    if (playBoardSpots[0] == "X" && playBoardSpots[1] == "X" && playBoardSpots[2] == "X")
                    { // print spilleplade med vundet text
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[3] == "X" && playBoardSpots[4] == "X" && playBoardSpots[5] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[6] == "X" && playBoardSpots[7] == "X" && playBoardSpots[8] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[0] == "X" && playBoardSpots[3] == "X" && playBoardSpots[6] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[1] == "X" && playBoardSpots[4] == "X" && playBoardSpots[7] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[2] == "X" && playBoardSpots[5] == "X" && playBoardSpots[8] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[0] == "X" && playBoardSpots[4] == "X" && playBoardSpots[8] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[2] == "X" && playBoardSpots[4] == "X" && playBoardSpots[6] == "X")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        XWon();
                        newScores.scoreX += 1;
                        break;
                    }
                    else if (playBoardSpots[0] == "O" && playBoardSpots[1] == "O" && playBoardSpots[2] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[3] == "O" && playBoardSpots[4] == "O" && playBoardSpots[5] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[6] == "O" && playBoardSpots[7] == "O" && playBoardSpots[8] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[0] == "O" && playBoardSpots[3] == "O" && playBoardSpots[6] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[1] == "O" && playBoardSpots[4] == "O" && playBoardSpots[7] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[2] == "O" && playBoardSpots[5] == "O" && playBoardSpots[8] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[0] == "O" && playBoardSpots[4] == "O" && playBoardSpots[8] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }
                    else if (playBoardSpots[2] == "O" && playBoardSpots[4] == "O" && playBoardSpots[6] == "O")
                    {
                        Console.Clear();
                        PlayBoard(playBoardSpots[0], playBoardSpots[1], playBoardSpots[2], playBoardSpots[3], playBoardSpots[4], playBoardSpots[5], playBoardSpots[6], playBoardSpots[7], playBoardSpots[8]);
                        OWon();
                        newScores.scoreO += 1;
                        break;
                    }

                    // skift tur
                    if (turn == "X")
                    {
                        turn = "O";
                    }
                    else if (turn == "O")
                    {
                        turn = "X";
                    }
                }
            }
        }

        /// <summary>
        /// Udskriver spilleplade med givende inputs
        /// </summary>
        /// <param name="spot1">Felt 1</param>
        /// <param name="spot2">Felt 2</param>
        /// <param name="spot3">Felt 3</param>
        /// <param name="spot4">Felt 4</param>
        /// <param name="spot5">Felt 5</param>
        /// <param name="spot6">Felt 6</param>
        /// <param name="spot7">Felt 7</param>
        /// <param name="spot8">Felt 8</param>
        /// <param name="spot9">Felt 9</param>
        static void PlayBoard(string spot1, string spot2, string spot3, string spot4, string spot5, string spot6, string spot7, string spot8, string spot9)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Playboard:");
            Console.WriteLine("____________");
            Console.WriteLine(spot1 + "|" + spot2 + "|" + spot3);
            Console.WriteLine("-----");
            Console.WriteLine(spot4 + "|" + spot5 + "|" + spot6);
            Console.WriteLine("-----");
            Console.WriteLine(spot7 + "|" + spot8 + "|" + spot9);
            Console.WriteLine("____________");
        }

        /// <summary>
        /// Får og retunere input fra bruger
        /// </summary>
        /// <returns>Input i string</returns>
        static string GetInputString()
        {
            string userInput = Console.ReadLine();
            return userInput;
        }

        /// <summary>
        /// Udskriver text til start af ny tur
        /// </summary>
        /// <param name="turn">String med tur</param>
        static void StartTurn(string turn)
        {
            Console.WriteLine("It is "+turn+"'s turn");
            Console.WriteLine("The board spots numbers goes from left to right and from top to buttom");
            Console.WriteLine("Write the number on wich spot you would like to place your mark:");
        }

        /// <summary>
        /// Udskriver text til start af tur for når der skal flyttes på brikker
        /// </summary>
        /// <param name="turn">String med tur</param>
        static void StartTurnSwitch(string turn)
        {
            Console.WriteLine("It is "+turn+"'s turn");
            Console.WriteLine("The board spots numbers goes from left to right and from top to buttom");
            Console.WriteLine("Write the number on wich of your own marks you wish to move:");
        }

        /// <summary>
        /// Checker efter input og retunere en værdi til endring af spilleplade
        /// </summary>
        /// <param name="numbInput">string med input fra brugeren</param>
        /// <returns>Int med værdig til endring af spilleplade</returns>
        static int ChangePlayBoard(string numbInput)
        {
            int change = 0;
            if (numbInput == "1")
            {
                change = 0;
            }
            else if (numbInput == "2")
            {
                change = 1;
            }
            else if (numbInput == "3")
            {
                change = 2;
            }
            else if (numbInput == "4")
            {
                change = 3;
            }
            else if (numbInput == "5")
            {
                change = 4;
            }
            else if (numbInput == "6")
            {
                change = 5;
            }
            else if (numbInput == "7")
            {
                change = 6;
            }
            else if (numbInput == "8")
            {
                change = 7;
            }
            else if (numbInput == "9")
            {
                change = 8;
            }
            else
            {
                change = 9; //feijl reporterings værdi
            }
            return change;
        }

        /// <summary>
        /// Class til at holde styr på scores
        /// </summary>
        class CurrentScores
        {
            public int scoreX;
            public int scoreO;
        }

        /// <summary>
        /// Udskriver text til spiller X har vundet
        /// </summary>
        static void XWon()
        {
            Console.WriteLine("Player X has gotten 3 in a row, and has won the game!");
            Console.WriteLine("Press any key to start a new round:");
            Console.ReadKey();
        }

        /// <summary>
        /// Udskriver text til spiller O har vundet
        /// </summary>
        static void OWon()
        {
            Console.WriteLine("Player O has gotten 3 in a row, and has won the game!");
            Console.WriteLine("Press any key to start a new round:");
            Console.ReadKey();
        }

        /// <summary>
        /// Funktion til fejl melling af ugyldit input
        /// </summary>
        static void InvalidError()
        {
            Console.WriteLine("Invalid input, try again:");
        }

        /// <summary>
        /// Udsjriver Fejl melling hvis givende felt er taget
        /// </summary>
        static void TakenError()
        {
            Console.WriteLine("This spot is already taken, try again:");
        }
    }
}
