namespace DataBaseManger.Model
{
    public class ItemMapModel
    {
        public int MapID { get; set; }
        public int BaseUnitID { get; set; }
        public int SecondUnitID { get; set; }
        public double Rate { get; set; }
        

        public ItemMapModel(int mapID, int baseUnitID, int secondUnitID, double rate)
        {
            this.MapID = mapID;
            this.BaseUnitID = baseUnitID;
            this.SecondUnitID = secondUnitID;
            this.Rate = rate;
            
        }
    }
}
