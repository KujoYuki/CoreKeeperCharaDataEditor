using CKCharaDataEditor.Model.Pet;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CKCharaDataEditor.Model.ItemAux
{
    /// <summary>
    /// 補助データの管理クラス
    /// </summary>
    public class AuxPrefabManager
    {
        public List<AuxPrefab> Prefabs { get; private set; }

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

        public bool HasData(ulong prefabHash, ulong stableTypeHash)
        {
            return Prefabs?.Any(p => p.prefabHash == prefabHash && p.types.Any(t => t.stableTypeHash == stableTypeHash)) ?? false;
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
                prefab.types = updatedTypes.ToList();
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

        public void DeletePrefab(ulong prefabHash, ulong stableTypeHash)
        {
            bool? exist = Prefabs.Where(p => p.prefabHash == prefabHash)
                .SelectMany(p => p.types)
                .Where(s => s.stableTypeHash == stableTypeHash)
                .Any();
            if (exist == false)
            {
                throw new KeyNotFoundException($"対象のハッシュが存在しません。\n" +
                    $"prefabHash:{prefabHash}\nstableTypeHash:{stableTypeHash}");
            }

            // stableTypeHashに対応するデータを削除
            int prefabIndex = Prefabs.FindIndex(p => p.prefabHash == prefabHash);
            var prefab = Prefabs[prefabIndex];

            int stableTypeIndex = prefab.types.FindIndex(s => s.stableTypeHash == stableTypeHash);
            Prefabs[prefabIndex].types.RemoveAt(stableTypeIndex);

            //Prefab内に他のtypesが残っていなければ削除
            if (Prefabs[prefabIndex].types.Count == 0)
            {
                Prefabs.RemoveAt(prefabIndex);
            }
        }

        public void UpdatePet(string petName, PetColor petColor, IEnumerable<PetTalent> petTalents)
        {
            UpdateData(AuxHash.PetGroupHash, AuxHash.PetColorHash, [((int)petColor).ToString()]);
            UpdateData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash, petTalents.Select(t => t.ToJsonString()));
            UpdateData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash, [petName]);
        }

        public bool TryGetData(ulong prefabHash, ulong stableTypeHash, out IEnumerable<string>? data)
        {
            try
            {
                data = Prefabs?.SingleOrDefault(p => p.prefabHash == prefabHash)?.types
                .SingleOrDefault(s => s.stableTypeHash == stableTypeHash)?.data;
                return data is not null;
            }
            catch (Exception)
            {
                data = null;
                return false;
            }
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
            var petNameStable = new AuxStableType(AuxHash.ItemNameHash, [petName]);
            prefabs.Add(new AuxPrefab(AuxHash.ItemNameGroupHash, [petNameStable]));
            var petColorStable = new AuxStableType(AuxHash.PetColorHash, [color.ToString()]);
            var petTalentsStable = new AuxStableType(AuxHash.PetTalentsHash, talents.Select(t => t.ToJsonString()));
            prefabs.Add(new AuxPrefab(AuxHash.PetGroupHash, [petColorStable, petTalentsStable]));
            return new AuxPrefabManager(prefabs);
        }

        public string GetJsonString()
        {
            if (Prefabs is null)
            {
                return new JsonObject().ToJsonString();
            }
            if (Prefabs.Count == 0)
            {
                return string.Empty;
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
            if (Prefabs.Count is 0)
            {
                return string.Empty;
            }
            var jsonObject = new JsonObject
            {
                ["prefabs"] = new JsonArray(
                    Prefabs!.Select(p => JsonSerializer.SerializeToNode(p, options))
                    .ToArray()),
            };
            return jsonObject
                .ToJsonString(options);
        }

        public static readonly AuxPrefabManager Default = new AuxPrefabManager(new JsonObject
        {
            ["prefabs"] = new JsonArray()
        });

        public AuxPrefabManager DeepCopy()
        {
            var copiedPrefabs = Prefabs
                .Select(prefab => new AuxPrefab(
                    prefab.prefabHash,
                    prefab.types
                        .Select(type => new AuxStableType(type.stableTypeHash, type.data.ToList()))
                        .ToList()
                ))
                .ToList();

            return new AuxPrefabManager(copiedPrefabs);
        }
    }
}
