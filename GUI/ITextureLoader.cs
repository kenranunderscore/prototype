namespace prototype.GUI
{
    using System;

    internal interface ITextureLoader
    {
        ITexture LoadTexture(string path, IntPtr renderer);
    }
}
