using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine {
    public int id;
    public string characterName;
    public string emotion;

    public Dictionary<string, string> texts = new Dictionary<string, string>();
}

public static class CSVImporter {

    public static List<DialogueLine> LoadCSV(TextAsset file) {
        List<DialogueLine> lines = new List<DialogueLine>();
        string[] data = file.text.Split('\n');
        string[] headers = data[0].Trim().Split(';');

        for (int i = 1; i < data.Length; i++) {
            if (string.IsNullOrWhiteSpace(data[i])) continue;
            string[] row = data[i].Split(';');

            DialogueLine line = new DialogueLine();
            line.id = int.Parse(row[0]);
            line.characterName = row[1];
            line.emotion = row[2];
            for (int j = 3; j < headers.Length && j < row.Length; j++) {
                string langCode = headers[j]; // ES, EN, FR
                line.texts[langCode] = row[j];
            }
            lines.Add(line);
        }

        return lines;
    }
}
