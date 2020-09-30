namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IIdentity
	{
		IdentityCategories Category { get; }

		string Name { get; }
		IQualified From { get; }

		bool IsEquivalentTo(IIdentity identity);
	}
}