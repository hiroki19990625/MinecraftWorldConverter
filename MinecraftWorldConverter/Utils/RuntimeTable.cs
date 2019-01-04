using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinecraftWorldConverter.Properties;
using MinecraftWorldConverter.Tools;
using MineNET.NBT.Tags;
using Newtonsoft.Json.Linq;

namespace MinecraftWorldConverter.Utils
{
    public static class RuntimeTable
    {
        private static List<Table> tables = new List<Table>();

        private static Dictionary<string, Table> _nameToTable = new Dictionary<string, Table>();
        private static Dictionary<int, Table> _fullBlockToTable = new Dictionary<int, Table>();
        private static Dictionary<int, List<States>> _statesTable = new Dictionary<int, List<States>>();

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
                else if (table.ContainsKey("states"))
                {
                    if (!_statesTable.ContainsKey(t.Id))
                        _statesTable.Add(t.Id, new List<States>());

                    States s = new States(t.Data);
                    JObject states = (JObject) table["states"];
                    foreach (KeyValuePair<string, JToken> state in states)
                    {
                        s.Add(state.Key, state.Value.Value<string>());
                    }
                    _statesTable[t.Id].Add(s);
                }


                if (table.ContainsKey("id"))
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

        public static Table GetNameToTable(string name, Dictionary<string, Tag> properties)
        {
            try
            {
                Table table = _nameToTable[name];
                States states = _statesTable[table.Id].First((state) =>
                {
                    int match = 0;
                    foreach (KeyValuePair<string, Tag> property in properties)
                    {
                        if (property.Value is StringTag)
                        {
                            StringTag t = (StringTag) property.Value;
                            if (state.Properties.ContainsKey(property.Key))
                            {
                                if (state.Properties[property.Key].Value == t.Data)
                                {
                                    match++;
                                }
                            }
                        }
                    }

                    return match >= state.Properties.Count;
                });

                return new Table(table, states.Data);
            }
            catch (Exception e)
            {
                return GetNameToTable(name);
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

            internal Table(int runtimeId, int id, int data, string name)
            {
                RuntimeId = runtimeId;
                Id = id;
                Data = data;
                Name = name;
            }

            internal Table(Table table, int data)
            {
                RuntimeId = table.RuntimeId;
                Id = table.Id;
                Data = data;
                Name = table.Name;
            }
        }

        public class States
        {
            public Dictionary<string, State> Properties { get; }
            public int Data;

            internal States(int data)
            {
                Properties = new Dictionary<string, State>();
                Data = data;
            }

            internal void Add(string stateName, string value)
            {
                Properties.Add(stateName, new State(value));
            }
        }

        public class State
        {
            public string Value;

            internal State(string value)
            {
                Value = value;
            }
        }
    }
}
