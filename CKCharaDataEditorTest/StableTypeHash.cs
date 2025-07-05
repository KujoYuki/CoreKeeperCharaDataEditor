using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKCharaDataEditorTest
{
    // FNV1A64 ハッシュを使用して、型の安定したハッシュ値を生成するゲーム内クラスです。
    public static class StableTypeHash
    {
        private static bool _isInitialized;
        private static Dictionary<Type, ulong> _cache = new Dictionary<Type, ulong>();
        private static Dictionary<ulong, Type> _reverseLookup = new Dictionary<ulong, Type>();

        private static void InitializeCacheIfNeeded()
        {
            if (StableTypeHash._isInitialized)
                return;
            StableTypeHash._isInitialized = true;
            //foreach (TypeManager.TypeInfo allType in TypeManager.AllTypes)
            //{
            //    if (!(allType.Type == (Type)null))
            //    {
            //        ulong stableTypeHash = TypeHash.CalculateStableTypeHash(allType.Type, hashCache: StableTypeHash._cache);
            //        StableTypeHash._reverseLookup[stableTypeHash] = allType.Type;
            //    }
            //}
        }

        public static ulong GetStableTypeHash<T>() => StableTypeHash.GetStableTypeHash(typeof(T));

        public static ulong GetStableTypeHash(Type type)
        {
            StableTypeHash.InitializeCacheIfNeeded();
            return StableTypeHash._cache[type];
        }

        public static Type GetTypeFromTypeHash(ulong typeHash)
        {
            StableTypeHash.InitializeCacheIfNeeded();
            return StableTypeHash._reverseLookup[typeHash];
        }
    }
}
