using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IQualified
	{
		IPhysicalStorage Storage { get; }
		IReadOnlyList<IIdentity> Identites { get; }
		bool IsLogicallyEquivalentTo(IQualified other);
		bool IsPhysicallyEquivalentTo(IQualified other);
	}
}