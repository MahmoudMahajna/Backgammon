using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Backgammon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgammonGame backgammon;
        private readonly Point[] _moveToPoints = new Point[3] { null, null, null };


        public MainWindow()
        {
            InitializeComponent();
        }
        private StackPanel GetAppropriatePoint(IPoint point)
        {
            if (point == null)
            {
                throw new ArgumentNullException();
            }
            switch (point.Number)
            {
                case 1: return stackPane1;
                case 2: return stackPane2; 
                case 3: return stackPane3; 
                case 4: return stackPane4; 
                case 5: return stackPane5; 
                case 6: return stackPane6;
                case 7: return stackPane7; 
                case 8: return stackPane8;
                case 9: return stackPane9 ;
                case 10: return stackPane10; 
                case 11: return stackPane11; 
                case 12: return stackPane12; 
                case 13: return stackPane13; 
                case 14: return stackPane14; 
                case 15: return stackPane15; 
                case 16: return stackPane16; 
                case 17: return stackPane17; 
                case 18: return stackPane18; 
                case 19: return stackPane19;
                case 20: return stackPane20; 
                case 21: return stackPane21; 
                case 22: return stackPane22; 
                case 23: return stackPane23; 
                case 24: return stackPane24;
                case 25:return stackMiddlePoint;
                case 26: return stackPlayerOneFinalPoint;
                case 27:return stackPlayerTwoFinalPoint;
                default:return null; 
            }
        }

        private void FillCheckersInStackPanel(StackPanel stackPanel, Point point)
        {
            foreach(var checker in point)
            {
                Canvas b = new Canvas();
                b.Height = 30;
                b.Width = 30;
                var img = new ImageBrush();
                var str = Directory.GetCurrentDirectory().ToString() + @"\..\..\";
                string url = checker.PlayerNumber == 1 ?str+ @"Backgrounds\FirstChecker.ico" : str+ @"Backgrounds\SecondChecker.ico";
                img.ImageSource=new BitmapImage(new Uri(url));
                b.Background = img;
                stackPanel.Children.Add(b);
            }
        }


        private void FillCheckers(BackgammonGame backgammon)
        { 
            foreach (var point in backgammon)
            {
                var stackPanel = GetAppropriatePoint(point);
                stackPanel.Children.Clear();
                FillCheckersInStackPanel(stackPanel, point);
            }
        }
        
        private void ChangeColorOfPointsToMoveTo(int pointNum)
        {   
            var _point = backgammon.ElementAt(pointNum-1);
            if(backgammon.PointMoveFrom!=_point && backgammon.PointsToMoveTo[0]!=_point && backgammon.PointsToMoveTo[1] != _point)
            {
                return;
            }
            if (_point.Count() != 0 && _point.GetCheckerType()==backgammon.Turn )
            {
                var _points = backgammon.GetPointsToMoveTo(_point, backgammon.FirstDice, backgammon.SecondDice, _point.GetCheckerType());
                foreach (var point in _points)
                {
                    if (point != null)
                    {
                        var stackPanel = GetAppropriatePoint(point);
                        stackPanel.Background = Brushes.Green;
                    }
                }
            }
            else
            {
                if (_point.Number == backgammon.GetMiddlePointNumber())
                {
                    var _points = backgammon.GetPointsToMoveTo(_point, backgammon.FirstDice, backgammon.SecondDice, backgammon.Turn);
                    foreach (var point in _points)
                    {
                        if (point != null)
                        {
                            var stackPanel = GetAppropriatePoint(point);
                            stackPanel.Background = Brushes.Green;
                        }
                    }
                } 
            }
        }

        private void stackPane23_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(23);
        }
        private void stackPane21_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(21);
        }

        private void stackPane19_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(19);
        }

        private void stackPane17_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(17);
        }

        private void stackPane15_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(15);
        }

        private void stackPane13_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(13);
        }

        private void stackPane14_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(14);
        }

        private void stackPane16_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(16);
        }

        private void stackPane18_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(18);
        }

        private void stackPane20_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(20);
        }

        private void stackPane22_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(22);
        }

        private void stackPane24_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(24);
        }

        private void stackPane11_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(11);
        }

        private void stackPane9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(9);
        }

        private void stackPane7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(7);
        }

        private void stackPane5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(5);
        }

        private void stackPane3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(3);
        }

        private void stackPane1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(1);
        }

        private void stackPane2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(2);
        }

        private void stackPane4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(4);
        }

        private void stackPane6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(6);
        }

        private void stackPane8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(8);
        }

        private void stackPane10_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(10);
        }
        

        private void SetMoveToPoints()
        {
            _moveToPoints[0] = backgammon.PointsToMoveTo[0];
            _moveToPoints[1] = backgammon.PointsToMoveTo[1];
        }
        private void stackPane12_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(12);
        }
        private void StackPanelChosen(int stackPanelNum)
        {
            try
            {
                SetMoveToPoints();
                bool canMove;
                backgammon.PointChosen(stackPanelNum, out canMove);
                if (!canMove)
                {
                    var playerNum = backgammon.Turn == 1 ? 2 : 1;
                    MessageBox.Show($"{(playerNum == 1 ? backgammon.FirstPlayer : backgammon.SecondPlayer)} Cant Move!!");
                    MessageBox.Show((backgammon.Turn == 1 ? backgammon.FirstPlayer.ToString() : backgammon.SecondPlayer.ToString()) + " Roll The Dice Please!!");
                }
                else
                {
                    var _point = backgammon.ElementAt(stackPanelNum - 1);
                    if (!(_moveToPoints[0] == _point || _moveToPoints[1] == _point))
                    {
                        ChangeColorOfPointsToMoveTo(stackPanelNum);
                    }
                    else
                    {
                        ResetColorOfPointsToMoveTo();
                        FillCheckers(backgammon);
                        SetMoveToPoints();
                    }
                    if (backgammon.IsEnded())
                    {
                        MessageBox.Show($"Player {backgammon.TheWinner} Win!");
                        return;
                    }
                    if (backgammon.IsRoundEnded())
                        MessageBox.Show((backgammon.Turn == 1 ? backgammon.FirstPlayer.ToString() : backgammon.SecondPlayer.ToString()) + " Roll The Dice Please!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ResetColorOfPointsToMoveTo()
        {
            foreach(var point in _moveToPoints)
            {
                if (point == null)
                    continue;

                var stackPanel = GetAppropriatePoint(point);
                stackPanel.Background = Brushes.Transparent;
            }
        }

        private void RollDiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!backgammon.IsDiceRolled)
            {
                try
                {
                    backgammon.RollDice();
                    var img1 = new ImageBrush();
                    var str = Directory.GetCurrentDirectory().ToString() + @"\..\..\";
                    string url1 = str + $@"Backgrounds\b{backgammon.FirstDice}.png";
                    img1.ImageSource = new BitmapImage(new Uri(url1));
                    imgFirstDice.Background = img1;
                    string url2 = str + $@"Backgrounds\r{backgammon.SecondDice}.png";
                    var img2 = new ImageBrush(new BitmapImage(new Uri(url2)));
                    imgSecondDice.Background = img2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                
            }
        }

        private void stackMiddlePoint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(backgammon.GetMiddlePointNumber());
        }

        private void stackPlayerOneFinalPoint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(backgammon.GetFirstFinishPointNumber());
        }

        private void stackPlayerTwoFinalPoint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanelChosen(backgammon.GetSecondFinishPointNumber());

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                backgammon = new BackgammonGame(txtFirstPlayerName.Text, txtSecondPlayerName.Text);
                startButton.IsEnabled = false;
                rollDiceButton.IsEnabled = true;
                backgammon.FillCheckers();
                FillCheckers(backgammon);
                backgammon.SetTurn(1);
                MessageBox.Show((backgammon.Turn == 1 ? backgammon.FirstPlayer.ToString() : backgammon.SecondPlayer.ToString()) + " Roll The Dice Please!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
