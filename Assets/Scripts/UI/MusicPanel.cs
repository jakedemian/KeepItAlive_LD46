using UnityEngine;

public class MusicPanel : MonoBehaviour {
    public static MusicPanel Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
