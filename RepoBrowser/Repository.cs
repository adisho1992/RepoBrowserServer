namespace RepoBrowser
{
    public class RepositoryList
    {
        public List<Item> items { get; set; }
        public int total_count { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public Owner owner { get; set; }
    }

    public class Owner
    {
        public string avatar_url { get; set; }
    }
}