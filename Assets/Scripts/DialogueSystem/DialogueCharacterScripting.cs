using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Scriptable Object")]
public class DialogueCharacterScripting : ScriptableObject {
    [SerializeField] private string characterName;
    [SerializeField] private Sprite happyPhoto;
    [SerializeField] private Sprite angryPhoto;
    [SerializeField] private Sprite sadPhoto;
    [SerializeField] private Sprite seriousPhoto;

    public string Name => characterName;
    public Sprite HappyPhoto => happyPhoto;
    public Sprite AngryPhoto => angryPhoto;
    public Sprite SadPhoto => sadPhoto;
    public Sprite SeriousPhoto => seriousPhoto;

    public Sprite GetEmotionSprite(string emotion) {
        switch (emotion.ToLower()) {
            case "happy":   return happyPhoto;
            case "angry":   return angryPhoto;
            case "sad":     return sadPhoto;
            case "serious":   return seriousPhoto;
            default: return null;
        }
    }

}
