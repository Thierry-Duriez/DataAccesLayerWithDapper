using System;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Class of Attributs Object                                                                                --//
//--------------------------------------------------------------------------------------------------------------//
namespace DatasAccesLayer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableSQLAttribute : Attribute
    {
        /// <summary>
        /// Default empty. Required
        /// </summary>
        public string NameSQL { get; set; }

        public TableSQLAttribute()
        {
            NameSQL = "";
        }
    }

    //-----------------------------------------------------------------------------------------------------------//
    
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldSQLAttribute : Attribute
    {
        /// <summary>
        /// Default false
        /// </summary>
        public bool ExcluSQL { get; set; }
        
        /// <summary>
        /// Default false
        /// </summary>
        public bool PK { get; set; }
        
        /// <summary>
        /// Default false
        /// </summary>
        public bool FK { get; set; }
        
        /// <summary>
        /// Default false
        /// </summary>
        public bool IX { get; set; }
        
        /// <summary>
        /// Default false
        /// </summary>
        public bool Identity { get; set; }
        
        /// <summary>
        /// Default true
        /// </summary>
        public bool Nullable { get; set; }
        
        /// <summary>
        /// Default empty. Required
        /// </summary>
        public string NameSQL { get; set; }
        
        /// <summary>
        /// Default empty. Required
        /// </summary>
        public string TypeSQL { get; set; }

        /// <summary>
        /// Default 0. Required for char, nchar, varchar, nvarchar, decimal, float
        /// </summary>
        public int LengthSQL { get; set; }

        /// <summary>
        /// Default 0. Required for decimal, float
        /// </summary>
        public int PrecisionSQL { get; set; }
        
        /// <summary>
        /// Default 0. Required
        /// </summary>
        public int OrdreSQL { get; set; }

        public FieldSQLAttribute()
        {
            ExcluSQL = false;
            PK = false;
            FK = false;
            IX = false;
            Identity = false;
            Nullable = true;
            NameSQL = "";
            TypeSQL = "";
            LengthSQL = 0;
            PrecisionSQL = 0;
            OrdreSQL = 0;
        }
    }
}
