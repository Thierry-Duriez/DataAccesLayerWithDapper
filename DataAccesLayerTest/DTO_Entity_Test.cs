using System;
using DatasAccesLayer;

namespace DataAccesLayerTest
{
    [TableSQL(NameSQL = "DTO_Product")]
    public class DTO_Product : DTOBase
    {            
        [FieldSQL(NameSQL = "ProductId", TypeSQL = "int", Nullable = false, PK = true, Identity = true, OrdreSQL = 1)]
        public int ProductId { get; set; }

        [FieldSQL(NameSQL = "RegionId", TypeSQL = "int", Nullable = false, FK = true, OrdreSQL = 2)]
        public int RegionId { get; set; }

        [FieldSQL(NameSQL = "ProductCode", TypeSQL = "varchar", LengthSQL = 25, Nullable = false, OrdreSQL = 3)]
        public string ProductCode { get; set; }

        [FieldSQL(NameSQL = "ProductLabel", TypeSQL = "varchar", LengthSQL = 50, Nullable = false, OrdreSQL = 4)]
        public string ProductLabel { get; set; }

        [FieldSQL(NameSQL = "IsActive", TypeSQL = "bit", Nullable = false, OrdreSQL = 5)]
        public bool IsActive { get; set; }

        [FieldSQL(NameSQL = "ProductDateCreation", TypeSQL = "datetime", Nullable = false, OrdreSQL = 6)]
        public DateTime ProductDateCreation { get; set; }

        /// <summary>
        /// Constructeur 
        /// </summary>
        public DTO_Product()
        {
            ProductId = Int_BaseValue;
            RegionId = Int_BaseValue;            
            ProductCode = String_EmptyValue;            
            ProductLabel = String_EmptyValue;            
            IsActive = bool_FalseValue;
            ProductDateCreation = DateTime_BaseValue;
        }
    }
}
