using System.Collections.Generic;
using System.Text;
using MinecraftWorldConverter.Properties;
using Newtonsoft.Json.Linq;

namespace MinecraftWorldConverter.Utils
{
    public static class RuntimeTable
    {
        private static List<Table> tables = new List<Table>();

        private static Dictionary<string, Table> _nameToTable = new Dictionary<string, Table>();
        private static Dictionary<int, Table> _fullBlockToTable = new Dictionary<int, Table>();

        static RuntimeTable()
        {
            int identity = 0;
            JArray data = JArray.Parse(Encoding.UTF8.GetString(Resources.runtimeid_table));
            foreach (JToken jToken in data)
            {
                var table = (JObject) jToken;
                Table t = new Table(
                    identity,
                    table.Value<int>("id"),
                    table.Value<int>("data"),
                    table.Value<string>("name")
                );
                tables.Add(t);

                if (!_nameToTable.ContainsKey(t.Name))
                    _nameToTable.Add(t.Name, t);
                _fullBlockToTable.Add(t.Id << 4 | t.Data, t);

                ++identity;
            }
        }

        public static Table GetNameToTable(string name)
        {
            try
            {
                return _nameToTable[name];
            }
            catch
            {
                return tables[0];
            }
        }

        public static Table GetFullBlockToTable(int fullBlock)
        {
            try
            {
                return _fullBlockToTable[fullBlock];
            }
            catch
            {
                return tables[0];
            }
        }

        public class Table
        {
            public int RuntimeId { get; }
            public int Id { get; }
            public int Data { get; }
            public string Name { get; }

            public Table(int runtimeId, int id, int data, string name)
            {
                RuntimeId = runtimeId;
                Id = id;
                Data = data;
                Name = name;
            }
        }
    }
}
