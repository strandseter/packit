using System;

namespace Packit.Repository
{
    public static class Repository
    {
        public static void LoadItems()
        {
            var result = await httpClient.GetAsync(itemsUri);
            var json = await result.Content.ReadAsStringAsync();
            var items = DeserializeObject<Item[]>(json);
            foreach (Item item in items)
                ItemsList.Add(item);
        }
    }
}
