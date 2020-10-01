namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IIdentity
	{
		IdentityCategories Category { get; }

		int Order { get; }
		string Name { get; }
		IQualified From { get; }

		bool IsEquivalentTo(IIdentity identity);
	}
}