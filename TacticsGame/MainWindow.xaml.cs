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
        private MousePositionProvider _positionProvider;
        private UnitStateProvider _unitStateProvider;
        private ObservableCollection<int> _units = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        static void OnCollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var action = e.Action;
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
            _unitStateProvider = new UnitStateProvider();

            _game = new Game
                (
                    new BasicGenerator(),
                    _positionProvider,
                    _unitStateProvider,
                    new CoordinatesConverter(GlWindow.OpenGL),
                    new GameQueue(_units)
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
    }
}
