namespace api.Dtos.FOOD
{
    public class FOODOnTABLE
    {
        public string foodId { get; set; } = string.Empty;
        public string ID_TABLE {  get; set; } = string.Empty;
        public string foodName { get; set; } = string.Empty;
        public int? amount_left { get; set; } = 0;
        public decimal? price { get; set; } = 0;
        public string foodTypeStatus { get; set; } = string.Empty;
        public string? imageURL { get; set; } = null;
    }

    public class FOODwithAmount
    {
        public FOODOnTABLE food { get; set; } = new FOODOnTABLE();
        public int? amount { get; set; } = 0;
    }
}
