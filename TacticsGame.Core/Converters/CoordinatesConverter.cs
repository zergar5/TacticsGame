using SharpGL;
using System.Drawing;

namespace TacticsGame.Core.Converters;

public class CoordinatesConverter
{
    private readonly OpenGL _gl;

    public CoordinatesConverter(OpenGL gl)
    {
        _gl = gl;
    }

    public PointF ConvertScreenToWorld(PointF point)
    {
        var coordinates = _gl.UnProject(point.X, point.Y, 0);

        point.X = (float)coordinates[0];
        point.Y = (float)coordinates[1];

        return point;
    }
}