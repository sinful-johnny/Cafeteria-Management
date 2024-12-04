namespace Class.Converter
{
    public class ShapeDto
    {
        public string? iD_SHAPE { get; set; }
        public string? shapE_TYPENAME { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double radius { get; set; }
    }
    public static class JsonToTableTypeConverter
    {
        public static Shape ToShape(this ShapeDto dto)
        {
            //return new Shape(
            //    dto.iD_SHAPE,
            //    (dto.shapE_TYPENAME.ToLower() == "rectangle" ? "./rectangle.svg" : "./circle.svg"),
            //    dto.shapE_TYPENAME.ToLower(),
            //    (int)dto.width,
            //    (int)dto.height,
            //    (int)dto.radius,
            //    dto.shapE_TYPENAME
            //);

            return new Shape()
            {
                ShapeId = dto.iD_SHAPE == null ? "" : dto.iD_SHAPE,
                ImgURL = dto.shapE_TYPENAME.ToLower() == "rectangle" ? "./public/square.svg" : "./public/circle.svg",
                ShapeName = dto.shapE_TYPENAME,
                ShapeType = dto.shapE_TYPENAME.ToLower(),
                Width = (int)dto.width,
                Height = (int)dto.height,
                Radius = (int)dto.radius
            };
        }

    }
}
