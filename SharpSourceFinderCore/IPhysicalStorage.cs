namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IPhysicalStorage
	{
		string Path { get; }

		bool IsEquivalentTo(IPhysicalStorage other);

	}
}