using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utils
{
    public class OdinBiDictionaryExample : SerializedMonoBehaviour
    {
        [DictionaryDrawerSettings(KeyLabel = "ID", ValueLabel = "Name")]
        public Dictionary<int, string> idToName = new();

        [ShowInInspector, ReadOnly]
        public Dictionary<string, int> nameToId =>
            idToName.ToDictionary(pair => pair.Value, pair => pair.Key);
    }
}