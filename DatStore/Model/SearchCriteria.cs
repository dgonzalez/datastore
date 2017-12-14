using System;
namespace DatStore.Model
{
    public class SearchCriteria
    {
        private string _key;
        private string _value;

        public SearchCriteria(string key, string value)
        {
            this._key = key;
            this._value = value;
        }

        public string Value { get => _value; set => _value = value; }
        public string Key { get => _key; set => _key = value; }
    }
}
