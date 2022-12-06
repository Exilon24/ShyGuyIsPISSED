namespace ShyGuyIsPISSED
{
    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public static float LightFlickerTime { get; set; } = 3f;
    }
}
