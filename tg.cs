using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.Emulation;

namespace ZennoLab.OwnCode
{
	/// <summary>
	/// A simple class of the common code
	/// </summary>
	public class CommonCode
	{
		/// <summary>
		/// Lock this object to mark part of code for single thread execution
		/// </summary>
		public static object SyncObject = new object();

		// Insert your code here
	}
}
