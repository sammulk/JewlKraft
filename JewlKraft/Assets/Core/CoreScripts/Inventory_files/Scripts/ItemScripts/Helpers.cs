namespace Core.Inventory_files.Scripts.ItemScripts
{
    public static class Helpers
    {
        public enum MaterialType
        {
            Win,
            Gold,
            Ruby,
            Sapphire,
            Emerald,
            Topaz,
            Diamond,
            Onyx,
            Amethyst,
            Rose_Quartz
        }

        public enum CraftStage
        {
            Raw,
            Processed,
            Ready
        }
        
        public enum ItemType
        {
            Undefined,
            ToeRing,
            Necklace,
            Helmet,
            Boots,
            Ring,
            Bracelet
        }
    }
}