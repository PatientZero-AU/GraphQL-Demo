using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToughCuddles.Core.Helpers
{
  public static class DictionaryExtensions
  {
    public static IEnumerable<Tuple<T, TK>> ToTupleList<T, TK>(this Dictionary<T, TK> dictionary)
    {
      var tupleList = new List<Tuple<T, TK>>(dictionary.Count);
      var keyValuePairs = dictionary.ToList();
      tupleList.AddRange(
        keyValuePairs
          .Select(keyValuePair => new Tuple<T, TK>(keyValuePair.Key, keyValuePair.Value)
        )
     );
      return tupleList;
    }
  }
}
