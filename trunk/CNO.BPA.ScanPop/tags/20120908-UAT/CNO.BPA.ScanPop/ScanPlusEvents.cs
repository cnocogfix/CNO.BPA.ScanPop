using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emc.InputAccel.ScanPlus.Scripting;
using Emc.InputAccel.QuickModule.ClientScriptingInterface;

namespace CNO.BPA.ScanPop
{
	public class ScanPlusModuleEvents: IScanPlusModuleEvents
	{ 
		public void BeforeNewBatch(INewBatchParameters settings, Boolean autoBatchCreation)
		{

		}

		public Boolean BatchCreationError(INewBatchParameters settings, Boolean autoBatchCreation, IBatchCreationError errorInfo)
		{
			return false;
		}

		public Boolean FilterProcessList(IBatchInformation process)
		{
			return !process.Name.StartsWith("_");
		}

		public Boolean FilterBatchList(IBatchInformation batch)
		{
			return true;
		}

		public void ModuleStart(IApplication arg)
		{
		}

		public void ModuleFinish()
		{
		}

		public void GotServerConnection(IServerInformation serverInformation)
		{              
        }

		public void LostServerConnection(String serverHostName)
		{
		}
	}


}
