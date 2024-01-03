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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SharpGL;
using SharpGL.WPF;
using TacticsGame.Core;
using TacticsGame.Core.BattlefieldGenerator;

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
        private readonly BattlefieldGenerator _battlefieldGenerator = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            //_game.Render();
        }

        private void OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        { 
            _game = new Game();
            _gl = args.OpenGL;
            _game.InitRenderSystems(args.OpenGL);

            _timer = new DispatcherTimer();

            _timer.Interval = TimeSpan.FromMilliseconds(1000f/60);

            _timer.Tick += Tick;

            _timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            _game.Render();
        }

        private void OpenGLControlResized(object sender, OpenGLRoutedEventArgs args)
        {
            args.OpenGL.LoadIdentity();
            args.OpenGL.Ortho(-1, 1, -1, 1, -1, 1);
        }
    }
}
