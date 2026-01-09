using System.Text;

public static class csvToData
{
    private static string _OpenFile(string filename)
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
    public static string[,] GetMap(int stage)
    {
        string[,] map = new string[40, 40];
        string csvString = _OpenFile($"stage{stage}.csv");
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

    public static List<int> GetEnermy(int stage)
    {
        List<int> enermies = new();
        string csvString = _OpenFile($"enermies.csv");
        string[] splitedString = csvString.Split('\n');
        foreach (var line in splitedString[stage - 1].Split(','))
        {
            enermies.Add(int.Parse(line));
        }
        return enermies;
    }
}