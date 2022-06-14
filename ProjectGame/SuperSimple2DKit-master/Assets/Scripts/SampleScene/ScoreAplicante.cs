[System.Serializable]

// public static class ScoreAplicante
// {
//     public int idAplicante;
//     public int score = 0; 
// }

public class ScoreAplicante : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public int idAplicante;
    public int score = 0; 

    public static ScoreAplicante Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
