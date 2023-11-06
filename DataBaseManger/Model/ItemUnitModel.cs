using System.Security.Cryptography.X509Certificates;

namespace DataBaseManger.Model
{
    public class ItemUnitModel
    {
        public int UnitId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        

        public ItemUnitModel(int unitId, string fullName, string shortName)
        {
            this.UnitId = unitId;
            this.FullName = fullName;
            this.ShortName = shortName;
            
        }
    }
}
