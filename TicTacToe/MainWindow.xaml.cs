using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the current game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// true if it's player 1's turn (x)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// true if the game has ended
        /// </summary>
        private bool mGameEnded;
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            //create a new array of free cells
            mResults = new MarkType[9];

            for(int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.free;
            }

            //it's player 1's turn!
            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                //change background, foreground and content to default
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });

            //make sure the game hasn't finished
            mGameEnded = false;

    }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start a new game on the next click if we have finished
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            //cast the sender to a button
            var button = (Button)sender;

            //find the buttons in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //don't do anything if the cell already has a value
            if (mResults[index] != MarkType.free)
                return;

            //set the cell value based on which player's turn it is
            if (mPlayer1Turn)
            {
                mResults[index] = MarkType.Cross;
                button.Content = "X";
                mPlayer1Turn = false;
                button.Foreground = Brushes.Blue;
            }
                
            else
            {
                mResults[index] = MarkType.Nought;
                button.Content = "O";
                mPlayer1Turn = true;
                button.Foreground = Brushes.Red;
            }

            CheckForWinner();
            
        }

        /// <summary>
        /// Checks if a player has got 3 in a line
        /// </summary>
        private void CheckForWinner()
        {
            //check for horizontal wins
            if(mResults[0] != MarkType.free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            else if (mResults[3] != MarkType.free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            else if (mResults[6] != MarkType.free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //check for vertical wins
            else if (mResults[0] != MarkType.free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            else if (mResults[1] != MarkType.free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            else if (mResults[2] != MarkType.free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //check for diagonal wins 
            else if (mResults[0] != MarkType.free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            else if (mResults[2] != MarkType.free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            //check for full board and no winner
            if (!mResults.Any(f => f == MarkType.free))
            {
                mGameEnded = true;

                //turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {             
                    Button.Background = Brushes.Orange;
                });
            }
        }
    }
}
