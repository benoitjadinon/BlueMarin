namespace BlueMarin
{
	// http://www.uglybugger.org/software/post/friends_dont_let_friends_use_automapper

	public interface IMapToNew<TSource, TTarget>
		where TSource : new()
		where TTarget : new()
	{
		TTarget Map(TSource source);
		TSource MapReverse(TTarget target);
	}

	public interface IMapToExisting<TSource, TTarget>
	{
		TTarget Map(TSource source, TTarget target);
		TSource MapReverse(TTarget target, TSource source);
	}

	public abstract class AbstractMap<TSource, TTarget> : IMapToNew<TSource, TTarget>, IMapToExisting<TSource, TTarget>
		where TSource : new()
		where TTarget : new()
	{
		#region IMapToNew implementation

		public TTarget Map(TSource source){
			return Map (source, default(TTarget));
		}
		public TSource MapReverse(TTarget target){
			return MapReverse (target, default(TSource));
		}

		#endregion

		#region IMapToExisting implementation

		public abstract TTarget Map (TSource source, TTarget target);
		public abstract TSource MapReverse (TTarget target, TSource source);

		#endregion
	}
}