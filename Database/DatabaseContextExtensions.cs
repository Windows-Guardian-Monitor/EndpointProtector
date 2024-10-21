using Microsoft.EntityFrameworkCore;

namespace Database
{
	public static class DatabaseContextExtensions
	{
		public static void DetachLocal<T>(this DbContext context, T t, T local = null) where T : class
		{
			if (local is not null)
			{
				context.Entry(local).State = EntityState.Detached;
			}

			context.Entry(t).State = EntityState.Modified;
		}
	}
}
