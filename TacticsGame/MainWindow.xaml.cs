using SharpGL;
using SharpGL.WPF;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TacticsGame.Core;
using TacticsGame.Core.Battlefield.Generators;
using TacticsGame.Core.Converters;
using TacticsGame.Core.Providers;

namespace TacticsGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;
        private OpenGL _gl;
        private DispatcherTimer _timer;
        private readonly BasicGenerator _battlefieldGenerator = new();
        private MousePositionProvider _positionProvider;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GlWindow_OnOpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {

        }

        private void Tick(object sender, EventArgs e)
        {
            _game.Update();
            _game.Render();
        }

        private void GlWindow_OnOpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            _positionProvider = new MousePositionProvider();
            _game = new Game(new BasicGenerator(), _positionProvider, new CoordinatesConverter(GlWindow.OpenGL));
            _gl = args.OpenGL;

            _game.InitRenderSystems(args.OpenGL);

            _timer = new DispatcherTimer();

            _timer.Interval = TimeSpan.FromMilliseconds(1000f/60);

            _timer.Tick += Tick;

            _timer.Start();
        }

        private void GlWindow_OnResized(object sender, OpenGLRoutedEventArgs args)
        {
            args.OpenGL.Viewport(0, 0, (int)GlWindow.ActualWidth, (int)GlWindow.ActualHeight);
            args.OpenGL.LoadIdentity();
            args.OpenGL.Ortho(-8, 8, -4.5, 4.5, -1, 1);
        }

        private void GlWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);

            var point = new PointF((float)position.X, (float)(GlWindow.ActualHeight - position.Y));

            _positionProvider.TargetPosition = point;
        }

        private void GlWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            var point = new PointF((float)position.X, (float)(GlWindow.ActualHeight - position.Y));

            _positionProvider.Position = point;
        }
    }
}
