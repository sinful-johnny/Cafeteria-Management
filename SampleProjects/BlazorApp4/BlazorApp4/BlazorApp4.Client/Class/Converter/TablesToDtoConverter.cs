using System.Text.Json;

namespace Class.Converter
{
	public static class TablesToDtoConverter
	{
		private class FOODOnTABLE
		{
			public string foodId { get; set; } = string.Empty;
			public string ID_TABLE { get; set; } = string.Empty;
			public string foodName { get; set; } = string.Empty;
			public int? amount_left { get; set; } = 0;
			public decimal? price { get; set; } = 0;
			public string foodTypeStatus { get; set; } = string.Empty;
			public string? imageURL { get; set; } = null;
		}

		private class FOODwithAmount
		{
			public FOODOnTABLE food { get; set; } = new FOODOnTABLE();
			public int? amount { get; set; } = 0;
		}
		private class TABLE_FOODsDto
		{
			public double? x { get; set; } = 0;
			public double? y { get; set; } = 0;
			public double? width { get; set; } = 0;
			public double? height { get; set; } = 0;
			public double? radius { get; set; } = 0;
			public List<FOODwithAmount> foods { get; set; } = new List<FOODwithAmount>();
			public string tableStatus { get; set; } = string.Empty;
			public string tableId { get; set; } = string.Empty;
			public string shapeId { get; set; } = string.Empty;
			public string ID_CANVA { get; set; } = string.Empty;
		}

		private static List<TABLE_FOODsDto> ConvertToDtoList(List<ITable> tables)
		{
			var tableFoodsDtoList = new List<TABLE_FOODsDto>();

			foreach (var table in tables)
			{
				var dto = new TABLE_FOODsDto
				{
					x = table.X,
					y = table.Y,
					tableId = table.TableId.ToString(),
					shapeId = table.ShapeId != null ? table.ShapeId : "",
					tableStatus = table.TableStatus != null ? table.TableStatus : "unlocked",
					width = table is RectangleTable ? ((RectangleTable)table).Width : (double?)null,
					height = table is RectangleTable ? ((RectangleTable)table).Height : (double?)null,
					radius = table is CircleTable ? ((CircleTable)table).Radius : (double?)null,
					foods = table.Foods.Select(f => new FOODwithAmount
					{
						food = new FOODOnTABLE
						{
							foodId = f.Food.FoodId.ToString(),
							foodName = f.Food.FoodName,
							amount_left = f.Food.AmountLeft,
							price = f.Food.Price,
							foodTypeStatus = f.Food.FoodTypeStatus,
							imageURL = f.Food.ImageUrl
						},
						amount = f.Amount
					}).ToList()
				};

				tableFoodsDtoList.Add(dto);
			}

			return tableFoodsDtoList;
		}
		public static string ConvertToJson(List<ITable> tables)
		{
			var dtoList = ConvertToDtoList(tables);
			return JsonSerializer.Serialize(dtoList);
		}
	}
}
