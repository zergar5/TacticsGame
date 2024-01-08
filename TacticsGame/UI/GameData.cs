using System.ComponentModel;
using System.Windows.Media;

public class GameData : INotifyPropertyChanged
{
    private string hero1Image;
    public string Hero1Image
    {
        get { return hero1Image; }
        set { hero1Image = value; OnPropertyChanged(nameof(Hero1Image)); }
    }

    private int hero1HP;
    public int Hero1HP
    {
        get { return hero1HP; }
        set { hero1HP = value; OnPropertyChanged(nameof(Hero1HP)); }
    }

    private string hero2Image;
    public string Hero2Image
    {
        get { return hero2Image; }
        set { hero2Image = value; OnPropertyChanged(nameof(Hero2Image)); }
    }

    private int hero2HP;
    public int Hero2HP
    {
        get { return hero2HP; }
        set { hero2HP = value; OnPropertyChanged(nameof(Hero2HP)); }
    }

    private string hero3Image;
    public string Hero3Image
    {
        get { return hero3Image; }
        set { hero3Image = value; OnPropertyChanged(nameof(Hero3Image)); }
    }

    private int hero3HP;
    public int Hero3HP
    {
        get { return hero3HP; }
        set { hero3HP = value; OnPropertyChanged(nameof(Hero3HP)); }
    }

    private double hpWidth;
    public double HPWidth
    {
        get { return hpWidth; }
        set { hpWidth = value; OnPropertyChanged(nameof(HPWidth)); }
    }

    private SolidColorBrush hpColor;
    public SolidColorBrush HPColor
    {
        get { return hpColor; }
        set { hpColor = value; OnPropertyChanged(nameof(HPColor)); }
    }

    private string hpText;
    public string HPText
    {
        get { return hpText; }
        set { hpText = value; OnPropertyChanged(nameof(HPText)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}