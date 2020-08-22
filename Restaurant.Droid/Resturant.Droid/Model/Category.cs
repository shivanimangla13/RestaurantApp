using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.resturant.Droid.Model
{
    public class Category
    {
        public int GroupMastID { get; set; }
        public string ItemCategory { get; set; }
        public string level { get; set; }
        public string? qualitycheck { get; set; }
        public string ParentItemCategory { get; set; }
        public int ParentItemCategoryID { get; set; }
        public int CategoryLevel { get; set; }
        public int createdby { get; set; }
        public int? createdby1 { get; set; }
        public int modifiedby { get; set; }
        public int? modifiedby1 { get; set; }
        public DateTime? createddate { get; set; }
        public DateTime? modifieddate { get; set; }
        public int versionno { get; set; }
        public int rowstate { get; set; }
        public int? PurchaseType { get; set; }
        public int? SalesType { get; set; }
        public int purchaseitemacc { get; set; }
        public int salesitemac { get; set; }
        public int salesreturnitemacc { get; set; }
        public int purchasereturnitemacc { get; set; }
        public int? Shape { get; set; }
        public decimal? ShapeValue { get; set; }
        public decimal SpecificGravity { get; set; }
        public decimal GSTRate { get; set; }
        public string? HSNNumber { get; set; }
        public decimal? SpecificGravityValue { get; set; }
        public bool IsStorePublished { get; set; }
        public string m_ParentItemImageURL { get; set; }
        public string m_ParentItemText { get; set; }
        public string m_ParentSubCategoryText { get; set; }
    }
}