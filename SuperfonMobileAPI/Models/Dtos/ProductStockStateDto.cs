namespace SuperfonMobileAPI.Models.Dtos
{
    public class ProductStockStateDto
    {
        public string sku { get; set; }
        public bool hasStock { get; set; }
        public double price { get; set; }
        public string skuName { get; set; }
    }
}
