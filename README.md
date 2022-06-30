# Checkers
Checkers Windows Application

<img src="https://user-images.githubusercontent.com/72739568/175908237-c747c87d-de2f-472a-995d-a7ccd7cc69f3.png" alt="drawing" width="300"/>

## About :book:
This project is an english "Damka" game

- The software allows two players to play against each other, and allows to play against the computer
- The player can choose to play on diffrent board sizes

## Getting Started :confetti_ball:
Install Microsoft Visual Studio - https://visualstudio.microsoft.com/downloads/

Tutorial on how to open a project from a GitHub repo - https://docs.microsoft.com/en-us/visualstudio/get-started/tutorial-open-project-from-repo?view=vs-2022

Make sure you set CheckersGui as start up project, and press F5 to run the application

## AI Implementation :computer:
We implemented a class to choose a move for the computer, using the **MiniMax** algorithm

We chose to implement the evaluation function as followed: 

The function counts for the player who builds the tree the value of his pieces and subtracts from it the value of opponent’s pieces. We took into account that advanced pawns are more threatening than pawns that are on the back of the board, and that kings are considered more powerful than regular pawns. 

The function gives specific value of row to heuristic: 

- Pawn’s value: 5 + row number (As long as the piece close to the end of the board it worth more)
- King’s value = 5 + number of rows in the board + 2

## Tools used for development :wrench:
- .NET WinForms
- C#
- Microsoft Visual Studio


## Design :paintbrush:
- As the real physical game, we designed the game with a theme of Classic Wood
- The background of the setup form, the board, and even the player pieces are all with a wooden design
- We added a custom buttons design to the setting menu that will enhance the player's experience and will merge perfectly with the wooden design
- During a turn of a player his name design will be bolded & underlined so he can see it easily
- we created sounds to improve the user experience – each step the user can hear a sound of a piece sliding, and each capture the user can hear a capture sound. In the end of the game the user can hear a cool sound according to his result (winning or losing)

## Team members :man_office_worker::man_office_worker:
- Leead Arbetman
- Tomer Ahimeir

## Contributing
#### For contributing please follow the instructions below:
   1. **Fork** the repository on GitHub
   2. **Clone** the project to your own machine
   3. **Commit** changes to your own branch
   4. **push** your work back up to your forked repo
   5. Submit a Pull Request so that we can review your changes

 **NOTE:** Be sure to fetch and merge the latest changes from the "upstream" repository before making a pull request!
