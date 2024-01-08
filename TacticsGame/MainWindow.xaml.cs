using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TacticsGame.Converters;
using static System.Net.Mime.MediaTypeNames;
using SharpGL;
using SharpGL.WPF;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TacticsGame.Core;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Converters;
using TacticsGame.Core.Mechanics.Queue;
using TacticsGame.Core.Providers;
using System.Collections.Specialized;

namespace TacticsGame;

public partial class MainWindow : Window
{
    private static string _path = @$"{Directory.GetCurrentDirectory()}\TacticsGame.Core\Assets\Icons\Units\";
    //private List<Unit> _units = new List<Unit>{
    //        new(_path + "\\hero1.jpg", 80),
    //        new(_path + "\\hero2.jpg", 70),
    //        new(_path + "\\hero1.jpg", 70),
    //        new(_path + "\\hero1.jpg", 70),
    //        new(_path + "\\hero1.jpg", 70),
    //        new(_path + "\\hero2.jpg", 1000)};
    private List<UnitCard> _unitsCards = new List<UnitCard>{
        new (1, $"{_path}u001.jpg", 100, 100),
        new (2, $"{_path}u013.jpg", 80, 80) };
        //new ("Unit 3", $"{_path}u013.jpg", 45, 45),
        //new ("Unit 4", $"{_path}u014.jpg", 14, 100) };
    private int _roundNumber = 1;
    private RoundCard _round = new(@$"{Directory.GetCurrentDirectory()}\TacticsGame.Core\Assets\Icons\UI\skull.png");
    private int _info = 0;
    private bool isResizing = false;
    private int _unitsNumber = 0; 

    private Game _game;
    private OpenGL _gl;
    private DispatcherTimer _timer;
    private MousePositionProvider _positionProvider;
    private UnitStateProvider _unitStateProvider;
    private ObservableCollection<int> _units = new();

    public MainWindow()
    {
        InitializeComponent();

        string[] imagesPath = Directory.GetFiles(_path);
        laserButton.SetBinding(Button.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.1 });
        laserButton.SetBinding(Button.HeightProperty, new Binding("ActualHeight") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
        gunButton.SetBinding(Button.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
        gunButton.SetBinding(Button.HeightProperty, new Binding("ActualHeight") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
        passButton.SetBinding(Button.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
        passButton.SetBinding(Button.HeightProperty, new Binding("ActualHeight") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
        unitsList.SetBinding(StackPanel.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.5 });


        //Loaded += FillInTheQueue;

        _units.CollectionChanged += Fill;

    }

    private void Fill(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (_unitsNumber < _units.Count)
        {
            unitsList.Children.Clear();
            _roundNumber = 1;           
            _unitsNumber = _units.Count;
        }
        while (unitsList.Children.Count < 10)
        {
            if (_roundNumber != 1 && _info == _units.Count)
            {
                var roundCard = _round.CreateRoundBorder(_roundNumber);
                unitsList.Children.Add(roundCard);
                _info = 0;
            }
            foreach (var unit in _units)
            {

                var unitCard = _unitsCards.Find(x => x.Id.Equals(unit)).CreateBorder();
                unitsList.Children.Add(unitCard);

                _info++;

            }
            if (_info == _units.Count)
            {
                _roundNumber++;
            }

        }

    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!isResizing)
        {
            isResizing = true;

            double aspectRatio = 16.0 / 9.0;
            double newWidth = e.NewSize.Height * aspectRatio;
            double newHeight = e.NewSize.Width / aspectRatio;

            if (newWidth > e.NewSize.Width)
            {
                this.Width = newWidth;
            }
            else
            {
                this.Height = newHeight;
            }

            isResizing = false;
        }
    }
    
    private void FillInTheQueue(object sender, RoutedEventArgs e)
    {

        while (unitsList.Children.Count < 10)
        {
            if (_roundNumber != 1 && _info == _units.Count)
            {
                var roundCard = _round.CreateRoundBorder(_roundNumber);
                roundCard.Unloaded += FillInTheQueue;
                unitsList.Children.Add(roundCard);
                _info = 0;
            }


            var unit = _unitsCards[0];

             
            var unitCard = unit.CreateBorder();
            unitCard.Unloaded += FillInTheQueue;
            unitsList.Children.Add(unitCard);
            
            //group.MouseLeftButtonUp += Remove_Card;

            _info++;

            _unitsCards.RemoveAt(0);
            _unitsCards.Add(unit);
            if (_info == _units.Count)
            {
                _roundNumber++;
            }
        }



    }

    //private void AddUnitImage(Unit unit, StackPanel group)
    //{
    //    Image img = new Image();
    //    img.Height = 60;
    //    img.Source = unit.Image;
    //    img.Unloaded += FillInTheQueue;

    //    group.Children.Add(img);
    //}
    //private void AddHealthBar(StackPanel group)
    //{
    //    ProgressBar healthBar = new ProgressBar();
    //    healthBar.Minimum = 0;
    //    healthBar.Maximum = 100;
    //    healthBar.Value = 100;
    //    healthBar.Height = 10;
    //    healthBar.Width = 45;
    //    healthBar.Background = new SolidColorBrush(Color.FromArgb(0x69, 0x69, 0x69, 0x69));
    //    healthBar.Foreground = new SolidColorBrush(Color.FromRgb(0x32, 0xCD, 0x32));
    //    healthBar.Loaded += HealthBar_Loaded; // подписываемся на событие Loaded
    //                                          // добавляем ProgressBar на контейнер
    //    group.Children.Add(healthBar);
    //}

    //private void HealthBar_Loaded(object sender, RoutedEventArgs e)
    //{
    //    if (sender is ProgressBar healthBar)
    //    {
    //        // выполнение инициализации ProgressBar
    //        int hp = 100; // пример значения HP
    //        UpdateHealthBar(healthBar, hp); // вызываем метод обновления HP
    //    }
    //}

    //private void UpdateHealthBar(ProgressBar healthBar, int hp)
    //{
    //    healthBar.Value = hp;
    //}
    private void Remove_Card(object sender, RoutedEventArgs e)
    {
        unitsList.Children.Remove((Border)sender); var firstCard = (Border)unitsList.Children[0];
        var secondCard = (Border)unitsList.Children[1];
        if (((Grid)firstCard.Child).Children[1] is TextBlock && ((Grid)secondCard.Child).Children[1] is TextBlock)
        {
            unitsList.Children.Remove(firstCard);
        }
    }

    //static void OnCollectionChanged(object sender,
    //    System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    //{
    //    var action = e.Action;
    //}

    private void GlWindow_OnOpenGLDraw(object sender, OpenGLRoutedEventArgs args)
    {

    }

    private void Tick(object sender, EventArgs e)
    {
        _game.Update();
        _unitStateProvider.IsMoving = false;
        _game.Render();
    }

    private void GlWindow_OnOpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
    {
        _positionProvider = new MousePositionProvider();
        _unitStateProvider = new UnitStateProvider();

        _game = new Game
        (
            new BasicGenerator(),
            _positionProvider,
            _unitStateProvider,
            new CoordinatesConverter(GlWindow.OpenGL),
            _units
        );

        _gl = args.OpenGL;

        _game.InitRenderSystems(args.OpenGL);

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000f/60)
        };

        _timer.Tick += Tick;

        _timer.Start();
    }

    private void GlWindow_OnResized(object sender, OpenGLRoutedEventArgs args)
    {
        _gl.Viewport(0, 0, (int)GlWindow.ActualWidth, (int)GlWindow.ActualHeight);
        _gl.LoadIdentity();
        _gl.Ortho(-8, 8, -4.5, 4.5, -1, 1);
    }

    private void GlWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        var position = e.GetPosition(this);

        //Возможно, стоит определить заранее
        var point = new PointF((float)position.X, (float)(GlWindow.ActualHeight - position.Y));

        _positionProvider.TargetPosition = point;
        _unitStateProvider.IsMoving = true;
    }

    private void GlWindow_OnMouseMove(object sender, MouseEventArgs e)
    {
        var position = e.GetPosition(this);

        //Возможно, стоит определить заранее
        var point = new PointF((float)position.X, (float)(GlWindow.ActualHeight - position.Y));

        _positionProvider.Position = point;
    }

    private void PassButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
}