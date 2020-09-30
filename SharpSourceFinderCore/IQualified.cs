using System.Collections.Generic;

namespace Tokeiya3.SharpSourceFinderCore
{
	public interface IQualified
	{
		IReadOnlyList<IIdentity> Identities { get; }
		bool IsEquivalentTo(IQualified other);

	}
}