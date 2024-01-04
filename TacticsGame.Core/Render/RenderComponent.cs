namespace TacticsGame.Core.Render;

public struct RenderComponent
{
    public RenderingType Type { get; set; }

    public RenderComponent(RenderingType renderingType)
    {
        Type = renderingType;
    }
}