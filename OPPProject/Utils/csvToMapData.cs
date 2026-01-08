using System.Text;

public class csvToMapData
{
    public static string[,] ToArray()
    {
        string[,] map =  new string[40, 40];
        string csvString = "";
        string path = Path.Combine(Environment.CurrentDirectory, "./Data/stage1.csv");
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            byte[] csvBytes = new byte[fs.Length];
            UTF8Encoding encoding = new UTF8Encoding();
            while (fs.Read(csvBytes, 0, csvBytes.Length) > 0)
            {
                csvString += encoding.GetString(csvBytes);
            }
        }
        csvString = csvString.Replace("\r", "");
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
}