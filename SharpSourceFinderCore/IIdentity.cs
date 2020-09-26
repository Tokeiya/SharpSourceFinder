namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IIdentity
	{
		IdentityCategories Category { get; }
		IPhysicalStorage Storage { get;}

		string Name { get; }

		bool IsLogicallyEquivalentTo(IIdentity identity);

		bool IsPhysicallyEquivalentTo(IIdentity identity);
		IQualified From { get; }
	}
}