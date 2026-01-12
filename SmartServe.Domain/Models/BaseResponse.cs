using SmartServe.Domain.Constants;

namespace SmartServe.Domain.Models
{
	public class BaseResponse<T> : BaseResponse
	{
		public T Data { get; set; }
		public BaseResponse()
		{
			this.MetaData = new MetaData
			{
				ResultCode = ResultCodes.Success,
				ResultMessage = ResultMessages.Success
			};
		}
		public static BaseResponse<T> New<T>() where T : class, new()
		{
			return new BaseResponse<T>()
			{
				MetaData = new MetaData
				{
					ResultCode = ResultCodes.Success,
					ResultMessage = ResultMessages.Success
				}
			};
		}
		public static BaseResponse<T> New<T>(T data) where T : class, new()
		{
			return new BaseResponse<T>()
			{
				Data = data,
				MetaData = new MetaData
				{
					ResultCode = ResultCodes.Success,
					ResultMessage = ResultMessages.Success
				}
			};
		}
	}

	public class BaseResponse
	{
		public MetaData MetaData { get; set; }
		public static BaseResponse New()
		{
			return new BaseResponse()
			{
				MetaData = new MetaData
				{
					ResultCode = ResultCodes.Success,
					ResultMessage = ResultMessages.Success
				}
			};
		}
		
	}
}
