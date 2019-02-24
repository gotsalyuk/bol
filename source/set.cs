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
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace ZennoLab.OwnCode
{
	class source
	{
		private IZennoPosterProjectModel project;
		public source(IZennoPosterProjectModel project)
		{
			this.project=project;
		}
		System.Diagnostics.ProcessStartInfo startInfo = new ProcessStartInfo();
		public void proxy()
		{
			if(project.Variables["proxy_type"].Value=="nosok")
			{
				project.SendInfoToLog("Запуск парсера носков",true);
				string text=System.IO.File.ReadAllText(project.Directory+@"/proxy/nosok.xml");
				text = Macros.TextProcessing.Replace(text, @"(?<=url</Name><Value>).*?(?=</Value>)", project.Variables["nosok_url"].Value, "Regex", "All");
				text = Macros.TextProcessing.Replace(text, @"(?<=wait</Name><Value>).*?(?=</Value>)", project.Variables["nosok_time_update"].Value, "Regex", "All");
				System.IO.File.WriteAllText(project.Directory+@"/proxy/nosok.xml", text);
				System.Threading.Thread.Sleep(5 * 1000);
				startInfo.FileName = string.Format(project.Directory+@"/proxy/nosok_run.bat");
				Process.Start(startInfo);
			}
			else
			{
				project.SendInfoToLog("Запуск tor",true);
				startInfo.FileName = string.Format(project.Directory+@"/proxy/2. stop.bat");
				Process.Start(startInfo);
				System.Threading.Thread.Sleep(10 * 1000);
				var List=project.Lists["temp"];
				foreach (string File in Directory.GetDirectories(project.Directory+@"/proxy/data"))List.Add(File);
				for(int rc=0;rc<List.Count;rc++)
				{
					string dir=List[rc];
					Directory.Delete(dir,true);
				}
				System.Threading.Thread.Sleep(5 * 1000);
				startInfo.FileName = string.Format(project.Directory+@"/proxy/data/tor.exe");
				Process.Start(startInfo);
				System.Threading.Thread.Sleep(5 * 1000);
				startInfo.FileName = string.Format(project.Directory+@"/proxy/1. launch.vbs - Shortcut.lnk");
				Process.Start(startInfo);
			}
		}
		public void lists()
		{
			project.SendInfoToLog("подготовка списков",true);
			var recip =project.Tables["recip"];
			var email =project.Tables["email"];
			var subj =project.Lists["subj"];
			var link =project.Lists["link"];
			var paste_text =project.Lists["paste_text"];
			recip.Clear();email.Clear();subj.Clear();link.Clear();paste_text.Clear();
			string path=project.Variables["recip_list"].Value;
			var val=System.IO.File.ReadAllText(path);
			Macros.TextProcessing.ToTable(val, "\r\n", "Text", ":", "Text", project, recip);
			path=project.Variables["subj_list"].Value;
			val=System.IO.File.ReadAllText(path);
			Macros.TextProcessing.ToList(val, "\r\n", "Text", project, subj);
			path=project.Variables["email_list"].Value;
			val=System.IO.File.ReadAllText(path);
			Macros.TextProcessing.ToTable(val, "\r\n", "Text", ";", "Text", project, email);
			path=project.Variables["link_list"].Value;
			val=System.IO.File.ReadAllText(path);
			Macros.TextProcessing.ToList(val, "\r\n", "Text", project, link);
			path=project.Variables["paste_text_list"].Value;
			val=System.IO.File.ReadAllText(path);
			Macros.TextProcessing.ToList(val, "\r\n", "Text", project, paste_text);
			path=System.IO.File.ReadAllText(project.Directory+@"/set/s_bol.xml");
			path = Macros.TextProcessing.Replace(path, @"(?<=wait</Name><Value>).*?(?=</Value>)", project.Variables["limit_post_for_one_akk"].Value, "Regex", "All");
			path = Macros.TextProcessing.Replace(path, @"(?<=body</Name><Value>).*?(?=</Value>)", project.Variables["body"].Value, "Regex", "All");
			path = Macros.TextProcessing.Replace(path, @"(?<=proxy_type</Name><Value>).*?(?=</Value>)", project.Variables["proxy_type"].Value, "Regex", "All");
			val=System.IO.File.ReadAllText(project.Variables["body"].Value);
			if(val.Contains(@"$PASTE_LINK$"))path = Macros.TextProcessing.Replace(path, @"(?<=body_contains_link</Name><Value>).*?(?=</Value>)", "1", "Regex", "All");
			else path = Macros.TextProcessing.Replace(path, @"(?<=body_contains_link</Name><Value>).*?(?=</Value>)", "0", "Regex", "All");
			if(val.Contains(@"$PASTE_TEXT$"))path = Macros.TextProcessing.Replace(path, @"(?<=body_contains_text</Name><Value>).*?(?=</Value>)", "1", "Regex", "All");
			else path = Macros.TextProcessing.Replace(path, @"(?<=body_contains_text</Name><Value>).*?(?=</Value>)", "0", "Regex", "All");
			System.IO.File.WriteAllText(project.Directory+@"/set/s_bol.xml", path);
			startInfo.FileName = string.Format(project.Directory+@"/set/s_bol.bat");
			Process.Start(startInfo);
			if(project.Variables["proxy_type"].Value=="nosok"){
				ZennoPoster.AddTries("Nosok",1);
				System.Threading.Thread.Sleep(5 * 1000);
				if (!File.Exists(project.Directory+@"/proxy/proxy_nosok.txt"))System.Threading.Thread.Sleep(5 * 1000);
			}
			
			int t=int.Parse(project.Variables["limit_post_for_one_akk"].Value);
			t=recip.RowCount/t;
			project.SendInfoToLog("Запуск потоков",true);
			System.Threading.Thread.Sleep(3 * 1000);
			
			ZennoPoster.AddTries("S_bol",t);
			System.Threading.Thread.Sleep(1 * 500);
		}
	}
}