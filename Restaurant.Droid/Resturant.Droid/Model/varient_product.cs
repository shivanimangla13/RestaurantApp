

namespace com.resturant.Droid.Model
{
    public class varient_product
    {
        string variant_id, variant_name, product_id, variant_price, variant_mrp, variant_unit, variant_unit_value, varient_imqge;

        bool ischeckd;

        public string getVariant_id()
        {
            return variant_id;
        }

        public string getVarient_imqge()
        {
            return varient_imqge;
        }

        public void setVarient_imqge(string varient_imqge)
        {
            this.varient_imqge = varient_imqge;
        }

        public void setVariant_id(string variant_id)
        {
            this.variant_id = variant_id;
        }

        public string getVariant_name()
        {
            return variant_name;
        }

        public void setVariant_name(string variant_name)
        {
            this.variant_name = variant_name;
        }

        public string getProduct_id()
        {
            return product_id;
        }

        public void setProduct_id(string product_id)
        {
            this.product_id = product_id;
        }

        public string getVariant_price()
        {
            return variant_price;
        }

        public void setVariant_price(string variant_price)
        {
            this.variant_price = variant_price;
        }

        public string getVariant_mrp()
        {
            return variant_mrp;
        }

        public void setVariant_mrp(string variant_mrp)
        {
            this.variant_mrp = variant_mrp;
        }

        public string getVariant_unit()
        {
            return variant_unit;
        }

        public void setVariant_unit(string variant_unit)
        {
            this.variant_unit = variant_unit;
        }

        public string getVariant_unit_value()
        {
            return variant_unit_value;
        }

        public void setVariant_unit_value(string variant_unit_value)
        {
            this.variant_unit_value = variant_unit_value;
        }

        public bool getIscheckd()
        {
            return ischeckd;
        }

        public void setIscheckd(bool ischeckd)
        {
            this.ischeckd = ischeckd;
        }
    }
}