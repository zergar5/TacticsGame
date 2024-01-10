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
using TacticsGame.Core.Dto;
using System.Collections;
using TacticsGame.GameUI;
using SharpGL.SceneGraph.Primitives;
using TacticsGame.Cards;

namespace TacticsGame;

public partial class MainWindow : Window
{
    
    //private List<Unit> _units = new List<Unit>{
    //        new(_path + "\\hero1.jpg", 80),
    //        new(_path + "\\hero2.jpg", 70),
    //        new(_path + "\\hero1.jpg", 70),
    //        new(_path + "\\hero1.jpg", 70),
    //        new(_path + "\\hero1.jpg", 70),
    //        new(_path + "\\hero2.jpg", 1000)};

    private bool isResizing = false;
    private Game _game;
    private OpenGL _gl;
    private DispatcherTimer _timer;
    private MousePositionProvider _positionProvider;
    private StateProvider _stateProvider;
    private CurrentWeaponIdProvider _currentWeaponIdProvider;
    private ObservableCollection<int> _units = new();
    private DtoProvider _dtoProvider = new();

    public MainWindow()
    {
        InitializeComponent();
        var queuePanel = new UI(QueuePanel, _units, WeaponButton, PassButton, _dtoProvider);
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

     
    private void GlWindow_OnOpenGLDraw(object sender, OpenGLRoutedEventArgs args)
    {

    }

    private void Tick(object sender, EventArgs e)
    {
        _game.Update();
        _stateProvider.IsMadeTurn = false;
        _stateProvider.IsMoving = false;
        _stateProvider.IsShooting = false;
        _game.Render();
        GlWindow.DoRender();
    }

    private void GlWindow_OnOpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
    {
        _positionProvider = new MousePositionProvider();
        _stateProvider = new StateProvider();
        _currentWeaponIdProvider = new CurrentWeaponIdProvider();

        _game = new Game
        (
            new BasicGenerator(),
            _positionProvider,
            _stateProvider,
            _currentWeaponIdProvider,
            new CoordinatesConverter(GlWindow.OpenGL),
            _units,
            _dtoProvider
        );

        _gl = args.OpenGL;
        GlWindow.RenderTrigger = RenderTrigger.Manual;

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

        var point = new PointF((float)position.X, (float)(GlWindow.ActualHeight - position.Y));

        _positionProvider.TargetPosition = point;
        _stateProvider.IsMoving = true;
    }

    private void GlWindow_OnMouseMove(object sender, MouseEventArgs e)
    {
        var position = e.GetPosition(this);

        var point = new PointF((float)position.X, (float)(GlWindow.ActualHeight - position.Y));

        _positionProvider.Position = point;
    }

    private void PassButton_OnClick(object sender, RoutedEventArgs e)
    {
        _stateProvider.IsMadeTurn = true;
    }

    private void WeaponButton_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        if (button.IsPressed) _currentWeaponIdProvider.WeaponId = (int)button.Tag;
        else _currentWeaponIdProvider.WeaponId = -1;
    }
}