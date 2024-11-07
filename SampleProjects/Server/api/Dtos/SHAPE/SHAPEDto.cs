namespace api.Dtos.SHAPE
{
    public class SHAPEDto
    {
        public string? ID_SHAPE { get; set; }
        public string? SHAPE_TYPENAME { get; set; }
        public double? WIDTH { get; set; } = 0;
        public double? HEIGHT { get; set; } = 0;
        public double? RADIUS { get; set; } = 0;
    }
}
