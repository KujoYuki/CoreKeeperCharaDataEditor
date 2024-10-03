using System.Text.Json.Nodes;
using System.Text.Json;

namespace CKFoodMaker.Model.ItemAux
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

        public AuxPrefabManager()
        {
            Prefabs = new List<AuxPrefab>();
        }

        //todo Addメソッドを利用した無からのペット作成。要：aux.data.indexのルールについて調査
        public void AddPrefab(AuxPrefab prefab)
        {
            Prefabs?.Add(prefab);
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
            UpdateData(AuxHash.PetNameGroupHash, AuxHash.PetNameHash, [petName]);
        }

        public void UpdateCattle()
        {
            // 家畜も補助データをもつので、必要に応じて実装する。
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetData(ulong prefabHash, ulong stableTypeHash)
        {
            var data = Prefabs?.SingleOrDefault(p => p.prefabHash == prefabHash)?.types
                .SingleOrDefault(s => s.stableTypeHash == stableTypeHash)?.data!;
            if (data is null)
            {
                throw new KeyNotFoundException($"Not found instead of the hash:{prefabHash} or {stableTypeHash}");
            }
            return data;
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
