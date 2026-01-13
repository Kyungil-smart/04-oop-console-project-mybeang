using System.Text;

// csv Data Loading 을 위한 클래스
public static class csvToData
{
    private static string OpenFile(string filename)
    {
        string csvString = "";
        string path = Path.Combine(Environment.CurrentDirectory, $"./Data/{filename}");
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            byte[] csvBytes = new byte[fs.Length];
            UTF8Encoding encoding = new UTF8Encoding();
            while (fs.Read(csvBytes, 0, csvBytes.Length) > 0)
            {
                csvString += encoding.GetString(csvBytes);
            }
        }
        return csvString.Replace("\r", "");
    }
    
    // 맵 데이터 획득
    public static string[,] GetMap(int stage)
    {
        string[,] map = new string[40, 40];
        string csvString = OpenFile($"stage{stage}.csv");
        string[] splitedString = csvString.Split('\n');
        for (int i = 0; i < splitedString.Length; i++)
        {
            string[] csvLine = splitedString[i].Split(',');
            for (int j = 0; j < csvLine.Length; j++)
            {
                map[i, j] = csvLine[j];
            }
        }
        return map;
    }

    // 적 데이터 획득
    public static List<int> GetEnemy(int stage)
    {
        List<int> enemies = new();
        string csvString = OpenFile($"enemies.csv");
        string[] splitedString = csvString.Split('\n');
        foreach (var line in splitedString[stage - 1].Split(','))
        {
            enemies.Add(int.Parse(line));
        }
        return enemies;
    }
    
    // 스테이지별 버프 박스 개수 데이터 획득
    public static int GetNumOfBuffBox(int stage)
    {
        string csvString = OpenFile($"numOfTreasureBox.csv");
        string[] splitedString = csvString.Split(',');
        return int.Parse(splitedString[stage - 1]);
    }
}