public static class SceneChangeInfo
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
        
    public static Difficulty Information { get; set; }

    public static float Volume { get; set; } = 0.25f;
}