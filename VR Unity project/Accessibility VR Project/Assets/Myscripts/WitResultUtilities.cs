using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi.Json;

namespace Meta.WitAi.Utilities
{
    public static class WitResultUtilities
    {
        public static string GetFirstEntityValue(WitResponseNode response, string entityName)
        {
            if (response == null || string.IsNullOrEmpty(entityName))
            {
                return null;
            }

            var entities = response["entities"];
            if (entities == null || entities[entityName] == null)

            {
                return null;
            }

            var values = entities[entityName];
            if (values == null || values.Count == 0)
            {
                return null;
            }

            return values[0]?["value"]?.Value;
        }
    }
}

