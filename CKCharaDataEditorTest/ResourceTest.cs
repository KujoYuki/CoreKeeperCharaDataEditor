using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKFoodMakerTest
{
    [TestClass]
    public class ResourceTest
    {
        //ゲーム内公式リソースのパス（ドライブは環境依存）
        const string LocalizationFile = @"G:\SteamLibrary\steamapps\common\Core Keeper\localization\Localization Template.csv";

        [TestMethod]
        public void ReadLocalizationTest()
        {
            Dictionary<string, string> LocalizationConditionDict = new();
            var lines = File.ReadAllLines(LocalizationFile);
            foreach (var line in lines)
            {
                var split = line.Split('\t');
                if (split.Length > 8 && split[0].Contains("Condition"))
                {
                    string key = split[0].Split('/')[1];
                    try
                    {
                        
                        string value = split[8].Replace(@"{0}", "x");

                        // キーの重複をチェックしてから追加
                        if (!LocalizationConditionDict.TryAdd(key, value))  //internalName, description
                        {
                            if (LocalizationConditionDict[key].Length < value.Length)
                            {
                                // キーが重複したいた場合は、valueが長い方で上書きする
                                LocalizationConditionDict[key] = value;
                            }
                        }
                        //LocalizationConditionDict.TryAdd(key, value);   //internalName, description
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            List<(int Id, string InternalName, string DescriptionEN)> ConditionDescriptions = new();
            string conditionDescriptionFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "ConditionDescription.csv")
                ?? throw new FileNotFoundException($"ConditionDescription.csvが見つかりません。");
            var descriptions = File.ReadAllLines(conditionDescriptionFilePath).ToArray();
            foreach (var description in descriptions)
            {
                var split = description.Split(',');
                ConditionDescriptions.Add((int.Parse(split[0]), split[1], split[2]));  //conditionId, internalName, description EN
            }

            var sb = new StringBuilder();
            foreach (var name in ConditionDescriptions.Select(d=>d.InternalName))
            {
                int id = ConditionDescriptions.Where(d => d.InternalName == name).Single().Id;
                if (LocalizationConditionDict.TryGetValue(name, out var descJP))
                {
                    sb.AppendLine($"{id},{ConditionDescriptions.Single(d=>d.Id == id).InternalName},{descJP}");
                }
                else
                {
                    sb.AppendLine($"{id},{ConditionDescriptions.Single(d => d.Id == id).InternalName},{ConditionDescriptions.Single(c => c.InternalName == name).DescriptionEN}");
                }
            }
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "ConditionDescriptionJP.csv"), sb.ToString());

            Assert.IsTrue(true);
        }
    }
}
