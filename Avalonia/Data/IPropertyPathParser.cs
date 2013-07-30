namespace Avalonia.Data
{
    public interface IPropertyPathParser
    {
        PropertyPathToken[] Parse(object source, string path);
    }
}
