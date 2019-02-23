public class Utilities
{
    // Constant Scene name
    public const string menuScene = "Menu";
    public const string gameScene = "Game";

    // Constant gravity
    public const float localGravityForce = 9.81f;
}

// Different States of Game
public enum GameStates
{
    Playing,
    Pause,
    GameOver
}

// Different States of Player
public enum PlayerStates
{
    Jump,
    Gravitation
}