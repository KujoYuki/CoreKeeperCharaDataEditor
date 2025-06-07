using System.Text.Json.Nodes;
using System.Text.Json;
using CKCharaDataEditor.Model.Pet;

namespace CKCharaDataEditor.Model.ItemAux
{
    /// <summary>
    /// 補助データの管理クラス
    /// </summary>
    public class AuxPrefabManager
    {
        public List<AuxPrefab>? Prefabs { get; private set; }

        public AuxPrefabManager(JsonObject json)
        {
            Prefabs = json["prefabs"]!
                .AsArray()!
                .Select(prefabNode => JsonSerializer.Deserialize<AuxPrefab>(prefabNode!.ToJsonString()))!
                .ToList()!;
        }

        public AuxPrefabManager(List<AuxPrefab> prefabs)
        {
            Prefabs = prefabs;
        }

        //Addメソッドを利用した無からの作成
        public void AddPrefab(IEnumerable<AuxPrefab> prefabs)
        {
            Prefabs?.AddRange(prefabs);
        }

        public void AddStableType(ulong prefabHash, AuxStableType stableType)
        {
            var prefab = Prefabs?.FirstOrDefault(p => p.prefabHash == prefabHash);
            if (prefab != null)
            {
                var updatedTypes = prefab.types.Append(stableType);
                prefab.types = updatedTypes;
            }
            else
            {
                throw new InvalidOperationException("Prefab not found");
            }
        }

        public void UpdateData(ulong prefabHash, ulong stableTypeHash, IEnumerable<string> newData)
        {
            var prefab = Prefabs?.SingleOrDefault(p => p.prefabHash == prefabHash);
            if (prefab is not null)
            {
                var stableType = prefab?.types.SingleOrDefault(t => t.stableTypeHash == stableTypeHash);
                if (stableType is not null)
                {
                    stableType!.data = newData;
                }
                else
                {
                    throw new InvalidOperationException($"StableType:{stableTypeHash} not found");
                }
            }
            else
            {
                throw new InvalidOperationException($"Prefab:{prefabHash} not found");
            }
        }

        public void UpdatePet(string petName, PetColor petColor, IEnumerable<PetTalent> petTalents)
        {
            UpdateData(AuxHash.PetGroupHash, AuxHash.PetColorHash, [((int)petColor).ToString()]);
            UpdateData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash, petTalents.Select(t => t.ToJsonString()));
            UpdateData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash, [petName]);
        }

        public IEnumerable<string> GetData(ulong prefabHash, ulong stableTypeHash)
        {
            try
            {
                var data = Prefabs?.SingleOrDefault(p => p.prefabHash == prefabHash)?.types
                .SingleOrDefault(s => s.stableTypeHash == stableTypeHash)?.data!;
                return data;
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Data not found for Prefab:{prefabHash}, StableType:{stableTypeHash}");
            }
        }

        public static AuxPrefabManager CreatePet(string petName, int color, IEnumerable<PetTalent> talents)
        {
            var prefabs = new List<AuxPrefab>();
            var petNameStable = new AuxStableType(AuxHash.ItemNameHash, new[] { petName });
            prefabs.Add(new AuxPrefab(AuxHash.ItemNameGroupHash, new[] { petNameStable }));
            var petColorStable = new AuxStableType(AuxHash.PetColorHash, new[] { color.ToString() });
            var petTalentsStable = new AuxStableType(AuxHash.PetTalentsHash, talents.Select(t => t.ToJsonString()));
            prefabs.Add(new AuxPrefab(AuxHash.PetGroupHash, new[] { petColorStable, petTalentsStable }));
            return new AuxPrefabManager(prefabs);
        }

        public string GetJsonString()
        {
            if (Prefabs is null)
            {
                return new JsonObject().ToJsonString();
            }
            var jsonObject = new JsonObject
            {
                ["prefabs"] = new JsonArray(
                    Prefabs.Select(p => JsonSerializer.SerializeToNode(p))
                    .ToArray()),
            };

            return jsonObject.ToJsonString();
        }

        public string ToInnerJsonString(JsonSerializerOptions options)
        {
            var jsonObject = new JsonObject
            {
                ["prefabs"] = new JsonArray(
                    Prefabs!.Select(p => JsonSerializer.SerializeToNode(p, options))
                    .ToArray()),
            };
            return jsonObject
                .ToJsonString(options);
        }
    }
}
