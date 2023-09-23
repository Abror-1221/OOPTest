using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace YourProject
{
    public class RepositoryManager
    {
        private Dictionary<string, Tuple<string, int>> repository;

        public RepositoryManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            repository = new Dictionary<string, Tuple<string, int>>();
        }

        public void Register(string itemName, string itemContent, int itemType)
        {
            // Perform validation against itemContent based on itemType
            // If validation fails, throw an exception
            if (itemType == 1 && !IsValidJson(itemContent))
            {
                throw new ArgumentException("Invalid JSON content for item type 1.");
            }

            // Example: Check if itemContent is valid XML for itemType 2
            if (itemType == 2 && !IsValidXml(itemContent))
            {
                throw new ArgumentException("Invalid XML content for item type 2.");
            }

            if (repository.ContainsKey(itemName))
            {
                throw new ArgumentException("An item with the same name already exists.");
            }

            repository[itemName] = Tuple.Create(itemContent, itemType);
        }

        private bool IsValidJson(string json)
        {
            try
            {
                JObject.Parse(json);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private bool IsValidXml(string xml)
        {
            try
            {
                XDocument.Parse(xml);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public string Retrieve(string itemName)
        {
            if (repository.ContainsKey(itemName))
            {
                return repository[itemName].Item1;
            }

            return null; // or throw an exception if item not found
        }

        public int GetType(string itemName)
        {
            if (repository.ContainsKey(itemName))
            {
                return repository[itemName].Item2;
            }

            return -1; // or throw an exception if item not found
        }

        public void Deregister(string itemName)
        {
            if (repository.ContainsKey(itemName))
            {
                repository.Remove(itemName);
            }
            else
            {
                throw new ArgumentException("Item not found.");
            }
        }
    }

}
