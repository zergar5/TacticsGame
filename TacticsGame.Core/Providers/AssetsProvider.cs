using System.Drawing;
using System.Drawing.Imaging;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace TacticsGame.Core.Providers;

public class AssetsProvider
{
    private readonly Dictionary<string, string> _assetsPaths = new();
    private readonly Dictionary<string, uint> _loadedAssets = new();
    private readonly List<Texture> _textures = new();
    private readonly string _corePath = @$"{Directory.GetCurrentDirectory()}\TacticsGame\Assets\";

    private OpenGL _gl;

    public AssetsProvider()
    {
        _assetsPaths.Add("BaseBattlefield", _corePath + @"Textures\Map\BaseBattlefield.jpg");
        _assetsPaths.Add("NecronWarrior", _corePath + @"Icons\Units\NecronWarrior.jpg");
        _assetsPaths.Add("Termagant", _corePath + @"Icons\Units\Termagant.jpg");
        _assetsPaths.Add("GaussFlayer", _corePath + @"Icons\Weapons\GaussFlayer.jpg");
        //Не забыть поменять
        _assetsPaths.Add("Fleshborner", _corePath + @"Icons\Weapons\GaussFlayer.jpg");
    }

    public void InitGl(OpenGL gl)
    {
        _gl = gl;
    }

    public uint GetTexture(string id)
    {
        if (_loadedAssets.TryGetValue(id, out var texture))
        {
            return texture;
        }

        texture = LoadTexture(_assetsPaths[id]);

        _loadedAssets.Add(id, texture);

        return texture;
    }

    public string GetPath(string id)
    {
        if (_assetsPaths.TryGetValue(id, out var texture))
        {
            return texture;
        }

        throw new Exception();
    }

    private uint LoadTexture(string path)
    {
        var textureBitmap = new Bitmap(path);

        var texture = new Texture();

        texture.Create(_gl, textureBitmap);

        _textures.Add(texture);

        textureBitmap.Dispose();

        return texture.TextureName;
    }
}