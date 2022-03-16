using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Leclair.Stardew.Common.Integrations;

using ProducerFrameworkMod.Api;

namespace Leclair.Stardew.BetterCrafting.Integrations.ProducerFrameworkMod;

public class PFMIntegration : BaseAPIIntegration<IProducerFrameworkModApi, ModEntry> {

	public PFMIntegration(ModEntry mod)
		: base(mod, "Digus.ProducerFrameworkMod", "1.7.4") {

	}

	public HashSet<string>? GetMachineIDs() {
		if (!IsLoaded)
			return null;

		var output = API.GetRecipes();
		Log($"Output: {output}", StardewModdingAPI.LogLevel.Debug);

		HashSet<string> machines = new();

		foreach(var recipe in output) {
			if (!recipe.TryGetValue("MachineID", out object? value))
				continue;

			if (value is int val)
				machines.Add($"{val}");
			else if (value is string str)
				machines.Add(str);
		}

		return machines.Count > 0 ? machines : null;
	}

}
