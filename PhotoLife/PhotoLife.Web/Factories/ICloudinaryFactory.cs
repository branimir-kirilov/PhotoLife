using CloudinaryDotNet;

namespace PhotoLife.Factories
{
    public interface ICloudinaryFactory
    {
        Cloudinary GetCloudinary();
    }
}