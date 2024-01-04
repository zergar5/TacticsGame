using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TacticsGame
{
    public class Unit
    {
        public String Name { get; }
        public int HP { get; set; }
        public ImageSource Image { get; }

        public bool IsSelected {get; set;}

        public Unit(string path, int hp)
        {
            Name = Path.GetFileName(path);
            Image = BitmapFrame.Create(new Uri(path));
            HP = hp;
        }

        

    }
}
