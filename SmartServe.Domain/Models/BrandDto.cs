using SmartServe.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Models
{
	public class BrandDto
	{
		public int BrandId { get; set; }

		public string Name { get; set; } = null!;

		public bool? IsActive { get; set; }

		public virtual ICollection<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();
	}
}
