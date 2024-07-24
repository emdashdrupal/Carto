namespace Carto.Utils
{
    using Colossal.Json;
    using Colossal.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class MiscUtils
    {
        private static readonly ILog m_Log = Instance.Log;
        
        /// <summary>
        /// Combine paths based on current OS platform.
        /// （根據目前的OS平臺結合路徑。）
        /// </summary>
        /// <param name="paths">An array of paths.（一個路徑陣列。）</param>
        public static string CombinePath(params string[] paths)
        {
            switch (GetOSPlatform())
            {
                case "WINDOWS":
                    return Path.Combine(paths).Replace("/", "\\");

                default:
                    return Path.Combine(paths).Replace("\\", "/");
            }
        }

        /// <summary>
        /// Deserialize the option settings into the dictionary.
        /// （將選項的設定反序列化為字典。）
        /// </summary>
        /// <param name="input">The JSON string of field settings.（欄位設定的JSON字串。）</param>
        public static Dictionary<ExportUtils.FeatureType, Dictionary<string, bool>> Deserialize(string input)
        {
            return JSON.MakeInto<Dictionary<ExportUtils.FeatureType, Dictionary<string, bool>>>(JSON.Load(input));
        }

        /// <summary>
        /// Get the deep copy of the dictionary.
        /// （獲得字典的深層複製。）
        /// </summary>
        /// <param name="original">The original dictionary.（原始的字典。）</param>
        public static Dictionary<ExportUtils.FeatureType, Dictionary<string, bool>> DeepCopy(Dictionary<ExportUtils.FeatureType, Dictionary<string, bool>> original)
        {
            return original.ToDictionary(kvp => kvp.Key, kvp => new Dictionary<string, bool>(kvp.Value));
        }

        /// <summary>
        /// Get all file names in a given directory.
        /// （獲得指定目錄下所有檔案的名稱。）
        /// </summary>
        /// <param name="path">The path to the directory.（指向目錄的路徑。）</param>
        public static IEnumerable<string> GetFiles(string path)
        {
            /*
                # Source: （資料來源：）

                * Marc Gravell. (2009). How to recursively list all the files in a directory in C#?
                    https://stackoverflow.com/a/929418
            */

            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    m_Log.Error(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    m_Log.Error(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }

        /// <summary>
        /// Get index of maximum integer from an enumerable class.
        /// （獲得可遍歷的類別中，最大整數的索引值。）
        /// </summary>
        public static int GetIndexOfMaximum(IEnumerable<int> source)
        {
            /*
                # Source: （資料來源：）

                * Jon Skeet. (2009). Obtain the index of the maximum element
                    https://stackoverflow.com/a/1136335
            */

            IComparer<int> comparer = Comparer<int>.Default;
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Empty sequence");
                }
                int maxIndex = 0;
                int maxElement = iterator.Current;
                int index = 0;
                while (iterator.MoveNext())
                {
                    index++;
                    int element = iterator.Current;
                    if (comparer.Compare(element, maxElement) > 0)
                    {
                        maxElement = element;
                        maxIndex = index;
                    }
                }
                return maxIndex;
            }
        }

        /// <summary>
        /// Get index of minimum integer from an enumerable class.
        /// （獲得可遍歷的類別中，最小整數的索引值。）
        /// </summary>
        public static int GetIndexOfMinimum(IEnumerable<int> source)
        {
            /*
                # Source: （資料來源：）

                * Jon Skeet. (2009). Obtain the index of the maximum element
                    https://stackoverflow.com/a/1136335
            */

            IComparer<int> comparer = Comparer<int>.Default;
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Empty sequence");
                }
                int minIndex = 0;
                int minElement = iterator.Current;
                int index = 0;
                while (iterator.MoveNext())
                {
                    index++;
                    int element = iterator.Current;
                    if (comparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        minIndex = index;
                    }
                }
                return minIndex;
            }
        }

        /// <summary>
        /// Get the OS platform the game is running on.
        /// （獲得遊戲運行的OS平臺。）
        /// </summary>
        /// <returns>The current OS ("LINUX", "OSX", or "WINDOWS").（目前的作業系統（"LINUX"、"OSX"或"WINDOWS"）。）</returns>
        public static string GetOSPlatform()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return "LINUX";
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "OSX";
                }

                return "WINDOWS";
            }
            catch (Exception)
            {
                m_Log.Warn($"An error occured at GetOSPlatform(), returning default \"WINDOWS\". 於 GetOSPlatform() 發生一個錯誤，回傳預設值 \"WINDOWS\"。");
                return "WINDOWS";
            }
        }

        /// <summary>
        /// Read all lines of the text resource into a string array.
        /// （讀取文本資源為字串陣列。）
        /// </summary>
        /// <param name="resource">The complete resource name.（資源的完整名稱。）</param>
        public static string[] ReadAllLines(string resource)
        {
            /*
                # Source: （資料來源：）

                * Peter Duniho. (2015). Using File.ReadAllLines from embedded text file
                    https://stackoverflow.com/a/29912240
            */

            IEnumerable<string> EnumerateLines(TextReader reader)
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }

            using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)))
            {
                return EnumerateLines(reader).ToArray();
            }
        }

        /// <summary>
        /// Remove the invalid file name characters in the file name.
        /// （移除檔案名稱中的非法字元。）
        /// </summary>
        /// <param name="input">The input string.（輸入字串。）</param>
        public static string RemoveInvalidChars(string input)
        {
            return new string(input.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray());
        }

        /// <summary>
        /// Reveal the file in the platform-specific file explorer. (Windows - File Explorer, Mac OS - Finder, Linux - Gnome)
        /// （在檔案瀏覽器中顯示指定目錄。）
        /// </summary>
        /// <param name="path">The path to the directory.（指向目錄的路徑。）</param>
        public static void RevealInFileExplorer(string path)
        {
            /*
                # Source: （資料來源：）

                * manuc66. (2022). From dotnet how to open file in containing folder in the Linux file manager?
                    https://stackoverflow.com/a/73409251
            */

            if (Directory.Exists(path))
            {
                try
                {
                    switch (GetOSPlatform())
                    {
                        case "LINUX":
                            Process.Start("xdg-open", path);
                            break;
                        
                        case "OSX":
                            Process.Start("open", path);
                            break;

                        case "WINDOWS":
                            Process.Start("explorer", path);
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    m_Log.Warn($"An error occured at RevealInFileExplorer(); 於 RevealInFileExplorer() 發生一個錯誤； {ex}");
                }
            }
            else
            {
                m_Log.Warn($"The given directory `{path}` didn't exist. 提供的目錄 `{path}` 不存在。");
            }
        }

        /// <summary>
        /// Evaluate whether two numbers has the same sign.
        /// （評估兩數是否同號。）
        /// </summary>
        /// <param name="x">First number.（第一個數字。）</param>
        /// <param name="y">Second number.（第二個數字。）</param>
        public static bool SameSign(double x, double y)
        {
            return x >= 0 && y >= 0 || x <= 0 && y <= 0;
        }

        /// <summary>
        /// Serialize the option settings into JSON string.
        /// （將選項的設定序列化為JSON字串。）
        /// </summary>
        /// <param name="input">The dictionary of field settings.（欄位設定的字典。）</param>
        public static string Serialize(Dictionary<ExportUtils.FeatureType, Dictionary<string, bool>> input)
        {
            return JSON.Dump(input, EncodeOptions.CompactPrint);
        }

        /// <summary>
        /// Evaluate whether a string is a valid latitude in decimal degree.
        /// （評估字串是否為有效的十進位緯度。）
        /// </summary>
        /// <param name="text">The string pending to evaluate.（待評估的字串。）</param>
        /// <param name="latitude">The output latitude.（轉換後的緯度。）</param>
        public static bool TryGetLatitude(string text, out double latitude)
        {
            latitude = double.NaN;

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out double number))
            {
                if ((number >= -90) & (number <= 90)) {
                    latitude = number;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Evaluate whether a string is a valid longitude in decimal degree.
        /// （評估字串是否為有效的十進位經度。）
        /// </summary>
        /// <param name="text">The string pending to evaluate.（待評估的字串。）</param>
        /// <param name="longitude">The output longitude.（轉換後的經度。）</param>
        public static bool TryGetLongtitude(string text, out double longitude)
        {
            longitude = double.NaN;

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture , out double number))
            {
                if ((number >= -180) & (number <= 180)) {
                    longitude = number;
                    return true;
                }
            }

            return false;
        }
    }
}