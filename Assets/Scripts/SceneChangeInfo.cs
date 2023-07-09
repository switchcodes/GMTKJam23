public static class SceneChangeInfo
{
    public enum DifficultyEnum
    {
        Easy,
        Normal,
        Hard
    }
        
    public static DifficultyEnum Difficulty { get; set; }

    public static float Volume { get; set; } = 0.25f;
    
    public static int Score { get; set; } = 0;
    public static float Satisfaction { get; set; } = 100;
    public static float Moral { get; set; } = 100;
    
    public static GameOverConditions GameOverCondition { get; set; }
}