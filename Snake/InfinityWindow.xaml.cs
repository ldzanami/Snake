using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.EntityFrameworkCore;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для InfinityWindow.xaml
    /// </summary>
    public partial class InfinityWindow : Window
    {
        AppContext db = new();
        User authUser = null;

        //Тикрейт (скорость игры)
        private readonly System.Windows.Threading.DispatcherTimer gameTickTimer = new();

        //Рандомизация спавна еды
        private readonly Random _random = new();

        // Константы характеристик змеи
        const int SnakeEllipseSize = 40;
        const int SnakeStartLength = 3;
        const int _snakeStartSpeed = 300;

        //Внешка змеи и её части
        private SolidColorBrush _snakeBodyBrush = Brushes.Red;
        private SolidColorBrush _snakeHeadBrush = Brushes.DarkRed;
        private List<SnakePart> _snakeParts = new();

        //Направление движения, стартовое направление, длинна змеи, счёт
        private enum SnakeDirectoin { Left, Right, Top, Bottom };
        private SnakeDirectoin _snakeDirectoin = SnakeDirectoin.Right;
        private int _snakeLength;
        private int _score = 0;

        //Еда как элемент интерфейса
        private UIElement _snakeFood = null;

        private List<SnakeDirectoin> _snakeDirectoinList = new(1);

        //Инициализация всего
        public InfinityWindow()
        {
            InitializeComponent();
            authUser = db.Users.Where(b => b.Auth == 1).FirstOrDefault();
            if (authUser != null)
            {
                _snakeBodyBrush = new SolidColorBrush(HexToRgb(authUser.Tailbrush));
                _snakeHeadBrush = new SolidColorBrush(HexToRgb(authUser.Headbrush));
            }
            else
            {
                _snakeBodyBrush = Brushes.Red;
                _snakeHeadBrush = Brushes.DarkRed;
            }
            gameTickTimer.Tick += GameTickTimer_Tick;
        }

        //Старт игры, прорисовка змеи и еды
        private void StartNewGame()
        {
            bdrWelcomeMessage.Visibility = Visibility.Collapsed;
            bdrEndOfGame.Visibility = Visibility.Collapsed;
            foreach (SnakePart snakeBodyPart in _snakeParts)
            {
                if (snakeBodyPart.Uielement != null)
                    CanvasMap.Children.Remove(snakeBodyPart.Uielement);
            }
            _snakeParts.Clear();
            if (_snakeFood != null)
                CanvasMap.Children.Remove(_snakeFood);

            _score = 0;
            _snakeLength = SnakeStartLength;
            Score.Text = $"Score: 0";
            _snakeDirectoin = SnakeDirectoin.Right;
            _snakeParts.Add(new SnakePart() { Position = new Point(SnakeEllipseSize * 5, SnakeEllipseSize * 5) });
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(_snakeStartSpeed);

            DrawSnake();
            DrawFood();
            gameTickTimer.IsEnabled = true;
        }

        // Тикрейт
        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        //Создание элипса для поля и не только
        private static Ellipse CreateEllipse(Point point, Brush brush)
        {
            Ellipse ellipse = new()
            {
                Width = SnakeEllipseSize,
                Height = SnakeEllipseSize,
                Fill = brush
            };
            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);
            return ellipse;
        }

        //Инициализация компонентов поля
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
        }

        //Прорисовка игрового поля
        private void DrawGameArea()
        {
            bool done = false;
            int nextX = 0, nextY = 0, rowCounter = 0;
            while (!done)
            {
                CanvasMap.Children.Add(CreateEllipse(new Point(nextX, nextY), Brushes.LightGreen));
                nextX += SnakeEllipseSize;
                if (nextX > CanvasMap.ActualWidth)
                {
                    nextX = 0;
                    nextY += SnakeEllipseSize;
                    rowCounter++;
                }
                if (nextY >= CanvasMap.ActualHeight)
                {
                    done = true;
                }

            }
        }

        //Прорисовка змеи
        private void DrawSnake()
        {
            foreach (SnakePart snakePart in _snakeParts)
            {
                if (snakePart.Uielement == null)
                {
                    snakePart.Uielement = new Ellipse()
                    {
                        Width = SnakeEllipseSize,
                        Height = SnakeEllipseSize,
                        Fill = snakePart.IsHead ? _snakeHeadBrush : _snakeBodyBrush
                    };
                    CanvasMap.Children.Add(snakePart.Uielement);
                    Canvas.SetTop(snakePart.Uielement, snakePart.Position.Y);
                    Canvas.SetLeft(snakePart.Uielement, snakePart.Position.X);
                }
            }
        }

        //Мувмент змеи
        private void Move()
        {
            if (bdrEndOfGame.Visibility == Visibility.Visible)
                EndGame();
            while (_snakeParts.Count >= _snakeLength)
            {
                CanvasMap.Children.Remove(_snakeParts[0].Uielement);
                _snakeParts.RemoveAt(0);
            }
            foreach (SnakePart snakePart in _snakeParts)
            {
                (snakePart.Uielement as Ellipse).Fill = _snakeBodyBrush;
                snakePart.IsHead = false;
            }

            SnakePart snakeHead = _snakeParts[_snakeParts.Count - 1];
            double nextX = snakeHead.Position.X;
            double nextY = snakeHead.Position.Y;
            if (_snakeDirectoinList.Count > 0)
            { _snakeDirectoin = _snakeDirectoinList[0]; _snakeDirectoinList.RemoveAt(0); }
            switch (_snakeDirectoin)
            {
                case SnakeDirectoin.Left: nextX -= SnakeEllipseSize; break;
                case SnakeDirectoin.Right: nextX += SnakeEllipseSize; break;
                case SnakeDirectoin.Top: nextY -= SnakeEllipseSize; break;
                case SnakeDirectoin.Bottom: nextY += SnakeEllipseSize; break;
            }

            _snakeParts.Add(new SnakePart()
            {
                Position = new Point(nextX, nextY),
                IsHead = true
            });
            DoCollisionCheck();
            DrawSnake();
        }

        //Следующая позиция спавна еды
        private Point NextFoodPosition()
        {
            int maxX = (int)(CanvasMap.ActualWidth / SnakeEllipseSize);
            int maxY = (int)(CanvasMap.ActualHeight / SnakeEllipseSize);
            int foodX = _random.Next(0, maxX) * SnakeEllipseSize;
            int foodY = _random.Next(0, maxY) * SnakeEllipseSize;

            foreach (SnakePart snakePart in _snakeParts)
            {
                if (snakePart.Position.X == foodX && snakePart.Position.Y == foodY)
                    return NextFoodPosition();
            }
            return new Point(foodX, foodY);
        }

        //Прорисока еды
        private void DrawFood()
        {
            Point foodPosition = NextFoodPosition();
            _snakeFood = new Ellipse()
            {
                Width = SnakeEllipseSize,
                Height = SnakeEllipseSize,
                Fill = Brushes.Orange
            };
            CanvasMap.Children.Add(_snakeFood);
            Canvas.SetTop(_snakeFood, foodPosition.Y);
            Canvas.SetLeft(_snakeFood, foodPosition.X);
        }

        //Управление
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            SnakeDirectoin snakeDirectoin = _snakeDirectoin;
            if (gameTickTimer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Up: if (snakeDirectoin != SnakeDirectoin.Bottom && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Top); break;
                    case Key.Down: if (snakeDirectoin != SnakeDirectoin.Top && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Bottom); break;
                    case Key.Left: if (snakeDirectoin != SnakeDirectoin.Right && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Left); break;
                    case Key.Right: if (snakeDirectoin != SnakeDirectoin.Left && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Right); break;
                    case Key.W: if (snakeDirectoin != SnakeDirectoin.Bottom && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Top); break;
                    case Key.S: if (snakeDirectoin != SnakeDirectoin.Top && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Bottom); break;
                    case Key.A: if (snakeDirectoin != SnakeDirectoin.Right && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Left); break;
                    case Key.D: if (snakeDirectoin != SnakeDirectoin.Left && _snakeDirectoinList.Count < 1) _snakeDirectoinList.Add(SnakeDirectoin.Right); break;
                }
            }
            if (e.Key == Key.R)
                StartNewGame();
            if (e.Key == Key.Escape)
                Close();
        }

        //Проверка на столкновения
        private void DoCollisionCheck()
        {
            SnakePart snakeHead = _snakeParts[_snakeParts.Count - 1];
            if ((snakeHead.Position.X == Canvas.GetLeft(_snakeFood)) && (snakeHead.Position.Y == Canvas.GetTop(_snakeFood)))
            {
                EatFood();
                return;
            }
            if ((snakeHead.Position.Y < 0) || (snakeHead.Position.Y >= CanvasMap.ActualHeight) || (snakeHead.Position.X < 0) || (snakeHead.Position.X >= CanvasMap.ActualWidth))
            {
                EndGame();
            }
            foreach (SnakePart snakeBodyPart in _snakeParts.Take(_snakeParts.Count - 1))
            {
                if ((snakeHead.Position.X == snakeBodyPart.Position.X) && (snakeHead.Position.Y == snakeBodyPart.Position.Y))
                {
                    EndGame();
                }
            }
        }

        //Едим еду, обновляем счёт
        private void EatFood()
        {
            _snakeLength++; _score++;
            Score.Text = $"Score: {_score}";
            CanvasMap.Children.Remove(_snakeFood);
            DrawFood();
        }

        //Конец игры
        private void EndGame()
        {
            gameTickTimer.IsEnabled = false;
            tbFinalScore.Text = $"{_score}";
            bdrEndOfGame.Visibility = Visibility.Visible;
            authUser = null;
            authUser = db.Users.Where(b => b.Auth == 1).FirstOrDefault();
            if (authUser != null)
            {
                authUser.Infrecord = _score;
            }
            db.SaveChanges();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Color HexToRgb(string hexColor)
        {
            if (hexColor.StartsWith("#"))
            {
                hexColor = hexColor.Substring(1);
            }

            byte r = Convert.ToByte(hexColor.Substring(0, 2), 16);
            byte g = Convert.ToByte(hexColor.Substring(2, 2), 16);
            byte b = Convert.ToByte(hexColor.Substring(4, 2), 16);

            return Color.FromRgb(r, g, b);
        }
    }
}
