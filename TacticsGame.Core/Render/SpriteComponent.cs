namespace TacticsGame.Core.Render;

public struct SpriteComponent
{
    public string Sprite { get; set; }

    public SpriteComponent(string sprite)
    {
        Sprite = sprite;
    }
}