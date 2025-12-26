using SmartServe.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Stores
{
	public interface IProductVariantStore
	{
		Task<ProductVariant> GetVariantAsync(int variantId);
	}
}
