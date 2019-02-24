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
	class instance_forms
	{
		private Instance instance;
		private IZennoPosterProjectModel project;
		public instance_forms(Instance instance, IZennoPosterProjectModel project)
		{
			this.instance=instance;
			this.project=project;
		}
		public void dbset()
		{
			project.Variables["action"].Value="dbset";
			project.Variables["DB_set"].Value=System.IO.File.ReadAllText(project.Directory+@"\settings\DB_set_s.txt");
			project.Variables["cp_srv"].Value=System.IO.File.ReadAllText(project.Directory+@"\settings\captcha.txt");
			string site=project.Variables["site"].Value;
			string DB_set=project.Variables["DB_set"].Value;
			string temp=string.Empty;
			string query=string.Empty;
			do{
				query="SELECT `value` FROM `DB_users` WHERE  `connects`<`max_connects` ORDER BY  `connects`+0 asc LIMIT 1;";
				temp=ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, " ", "\r\n");
			}while(temp=="");
			DB_set=temp;
			project.Variables["DB_set"].Value=DB_set;
			query="UPDATE `DB_users` SET `connects`=`connects`+1 WHERE  `value`='"+DB_set+"';\r\nSELECT  `url` FROM `api_rk` where `name`='notification';";
			project.Variables["url_for_notifications"].Value=ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, " ", "\r\n");
		}
		public void get_object()
		{
			project.Variables["action"].Value="get_object";
			var t=project.Tables["temp"];
			var obj=project.Tables["object"];
			lock(obj){
				project.Variables["id"].Value=obj.GetCell(0,0);
				project.Variables["operation_id"].Value=obj.GetCell(1,0);
				project.Variables["type_id"].Value=obj.GetCell(2,0);
				project.Variables["object_id"].Value=obj.GetCell(3,0);
				project.Variables["street"].Value=obj.GetCell(4,0);
				project.Variables["house"].Value=obj.GetCell(5,0);
				project.Variables["description"].Value=obj.GetCell(6,0);
				project.Variables["price"].Value=obj.GetCell(7,0);
				project.Variables["furniture"].Value=obj.GetCell(8,0);
				project.Variables["washer"].Value=obj.GetCell(9,0);
				project.Variables["heater"].Value=obj.GetCell(10,0);
				project.Variables["fridge"].Value=obj.GetCell(11,0);
				project.Variables["tv"].Value=obj.GetCell(12,0);
				project.Variables["internet"].Value=obj.GetCell(13,0);
				project.Variables["window"].Value=obj.GetCell(14,0);
				project.Variables["shower"].Value=obj.GetCell(15,0);
				project.Variables["balcony"].Value=obj.GetCell(16,0);
				project.Variables["toilet"].Value=obj.GetCell(17,0);
				project.Variables["floor"].Value=obj.GetCell(18,0);
				project.Variables["floors"].Value=obj.GetCell(19,0);
				project.Variables["wall_material"].Value=obj.GetCell(20,0);
				project.Variables["all_area"].Value=obj.GetCell(21,0);
				project.Variables["living_area"].Value=obj.GetCell(22,0);
				project.Variables["kitchen_area"].Value=obj.GetCell(23,0);
				project.Variables["layout"].Value=obj.GetCell(24,0);
				project.Variables["latitude"].Value=obj.GetCell(25,0);
				project.Variables["longitude"].Value=obj.GetCell(26,0);
				project.Variables["inet_title"].Value=obj.GetCell(27,0);
				project.Variables["city"].Value=obj.GetCell(28,0);
				project.Variables["region"].Value=obj.GetCell(29,0);
				project.Variables["landmark"].Value=obj.GetCell(30,0);
				project.Variables["user_id"].Value=obj.GetCell(31,0);
				project.Variables["photos"].Value=obj.GetCell(32,0);
				project.Variables["adv_number"].Value=obj.GetCell(33,0);
				project.Variables["adv_mail"].Value=obj.GetCell(34,0);
				project.Variables["new_house"].Value=obj.GetCell(35,0);
				project.Variables["process"].Value=obj.GetCell(36,0);
				obj.DeleteRow(0);
			}
			string query="UPDATE  `objects` set `"+project.Variables["site"].Value+"`=2 where `id`='"+project.Variables["id"].Value+"';\r\nSELECT  `id`, `profile` FROM `accs` where `rkId`='"+project.Variables["user_id"].Value+"';";
			ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, ref t);
			project.Variables["id_profile"].Value=t.GetCell(0,0);
			project.Variables["profile"].Value=t.GetCell(1,0);
		}
		public void check_run()
		{
			project.Variables["action"].Value="check_run";
			var t=project.Tables["temp"];
			string temp="1";
			string query=string.Empty;
			do{
				query="SELECT  `run`  FROM `si_"+project.Variables["site"].Value+"` where `id`='"+project.Variables["id_profile"].Value+"';";
				temp=ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, " ", "\r\n");
				if(temp=="")ZennoPoster.Db.ExecuteQuery("INSERT INTO `si_"+project.Variables["site"].Value+"` (`id`) VALUES ('"+project.Variables["id_profile"].Value+"');", null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, " ", "\r\n");
			}while(temp=="1");
			query="update `si_"+project.Variables["site"].Value+"` set `run`='1'  where `id`='"+project.Variables["id_profile"].Value+"';\r\nSELECT  `log`, `pas`, `cookie` FROM `si_"+project.Variables["site"].Value+"` where `id`='"+project.Variables["id_profile"].Value+"' LIMIT 1;";
			ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, ref t);
			project.Variables["log"].Value=t.GetCell(0,1);
			project.Variables["pas"].Value=t.GetCell(1,1);
			project.Variables["cookie"].Value=t.GetCell(2,1);
		}
		public void go_url(string url)
		{
			project.Variables["action"].Value="click "+url;
			instance.ActiveTab.Navigate(url);
			instance.ActiveTab.WaitDownloading();
		}
		public void click(string tag, string href, string text, string search_type, int number)
		{
			project.Variables["action"].Value="click "+text;
			HtmlElement he = instance.ActiveTab.FindElementByAttribute(tag, href, text, search_type, number);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			instance.ActiveTab.WaitDownloading();
		}
		public void captcha1(string tag, string atr, string text, string search_type, int number)
		{
			project.Variables["action"].Value="captcha1 "+text;
			string cps="RuCaptcha.dll";
			if(project.Variables["cp_srv"].Value=="antigate")cps="Anti-Captcha.dll";
			HtmlElement he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute(tag,atr,text,search_type,number);
			var result = ZennoPoster.CaptchaRecognition(cps, he.DrawToBitmap(false), "");
			var tmp = result.Split(new [] {"-|-"},StringSplitOptions.None);
			if(tmp.Length > 1){
				project.Variables["captcha"].Value=tmp[0];
				project.Variables["bad_captcha"].Value=tmp[1];
			}
			
		}
		public void reg_form_2_999_999()
		{
			project.Variables["action"].Value="reg_form_2_999_999";
			Random r = new Random();
			HtmlElement he;
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("user-login-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("user-login");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 0);
			}
			// Задержка эмуляции
			instance.WaitFieldEmulationDelay();
			// Установить элементу значение "project.Profile.Login"
			he.SetValue(project.Profile.Login, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			// пароль
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("user-password-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("user-password");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 1);
			}
			// Задержка эмуляции
			instance.WaitFieldEmulationDelay();
			// Установить элементу значение "project.Profile.EmailPassword"
			he.SetValue(project.Profile.EmailPassword, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("user-fullname-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("user-fullname");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 2);
			}
			// Задержка эмуляции
			instance.WaitFieldEmulationDelay();
			// Установить элементу значение "project.Profile.Name"
			he.SetValue(project.Profile.Name, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("user-email-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("user-email");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 3);
			}
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Profile.Email, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			// код
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("captcha-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("captcha");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 4);
			}
			// Задержка эмуляции
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["captcha"].Value, instance.EmulationLevel, false);
			// Клик на "Зарегистрировать"
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("submit-btn-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("submit-btn");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:submit", "fulltag", "input:submit", "text", 0);
			}
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			System.Threading.Thread.Sleep(r.Next(2, 5) * 1000);
		}
		public void auth_form_2_999_999()
		{
			Random r = new Random();
			// Клик на "Личный кабинет"
			HtmlElement he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "href", "https://2-999-999.ru/personal/auth/", "regexp", 0);
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "InnerText", "Личный\\ кабинет", "regexp", 0);
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "fulltag", "a", "text", 4);
			}
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			System.Threading.Thread.Sleep(r.Next(4, 8) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("login-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("login");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 0);
			}
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Profile.Login, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("password-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("password");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:password", "fulltag", "input:password", "text", 0);
			}
			instance.WaitFieldEmulationDelay();
			// Установить элементу значение "project.Profile.EmailPassword"
			he.SetValue(project.Profile.EmailPassword, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("set-authenticated-hash-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("set-authenticated-hash");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:checkbox", "fulltag", "input:checkbox", "text", 0);
			}
			instance.WaitFieldEmulationDelay();
			he.SetValue("True", instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("submit-btn-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("submit-btn");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:submit", "fulltag", "input:submit", "text", 0);
			}
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			System.Threading.Thread.Sleep(r.Next(6, 10) * 1000);
		}
		public void post_form_2_999_999()
		{
			string v=string.Empty;
			switch(project.Variables["operation_id"].Value){
				case"1":
					if(project.Variables["object_id"].Value=="1")v="1";
					else if(project.Variables["object_id"].Value=="2")v="2";
					else if(project.Variables["object_id"].Value=="3")v="3";	
					else if(project.Variables["object_id"].Value=="4"||project.Variables["object_id"].Value=="5")v="4";
					else if(project.Variables["object_id"].Value=="9"||project.Variables["object_id"].Value=="10")v="5";
					else if(project.Variables["object_id"].Value=="12"||project.Variables["object_id"].Value=="13"||project.Variables["object_id"].Value=="14"||project.Variables["object_id"].Value=="15")v="6";
					else if(project.Variables["object_id"].Value=="6"||project.Variables["object_id"].Value=="7"||project.Variables["object_id"].Value=="8")v="7";
					else v="8";
					break;
				case"2":
					if(project.Variables["object_id"].Value=="1")v="9";
					else if(project.Variables["object_id"].Value=="2")v="10";
					else if(project.Variables["object_id"].Value=="3"||project.Variables["object_id"].Value=="4"||project.Variables["object_id"].Value=="5")v="11";	
					else if(project.Variables["object_id"].Value=="9"||project.Variables["object_id"].Value=="10")v="12";
					else if(project.Variables["object_id"].Value=="12"||project.Variables["object_id"].Value=="13"||project.Variables["object_id"].Value=="14"||project.Variables["object_id"].Value=="15")v="13";
					else if(project.Variables["object_id"].Value=="6"||project.Variables["object_id"].Value=="7"||project.Variables["object_id"].Value=="8")v="14";
					else v="15";
					break;	
				case"4":
					if(project.Variables["object_id"].Value=="1")v="56";
					else if(project.Variables["object_id"].Value=="2")v="57";
					else if(project.Variables["object_id"].Value=="3")v="58";
					else if(project.Variables["object_id"].Value=="4")v="59";
					else if(project.Variables["object_id"].Value=="5")v="60";		
					else if(project.Variables["object_id"].Value=="51")v="61";		
					else if(project.Variables["object_id"].Value=="10")v="62";		
					else if(project.Variables["object_id"].Value=="8")v="69";			
					else if(project.Variables["object_id"].Value=="12"||project.Variables["object_id"].Value=="13"||project.Variables["object_id"].Value=="14")v="65";
					else if(project.Variables["object_id"].Value=="53"||project.Variables["object_id"].Value=="15")v="68";
					else if(project.Variables["object_id"].Value=="7"||project.Variables["object_id"].Value=="9")v="70";
					else v="71";
					break;	
				case"3":
					if(project.Variables["object_id"].Value=="1")v="105";
					else if(project.Variables["object_id"].Value=="2")v="106";
					else if(project.Variables["object_id"].Value=="3")v="107";
					else if(project.Variables["object_id"].Value=="4")v="108";
					else if(project.Variables["object_id"].Value=="5")v="109";		
					else if(project.Variables["object_id"].Value=="51")v="110";		
					else if(project.Variables["object_id"].Value=="10")v="111";		
					else if(project.Variables["object_id"].Value=="15")v="112";
					else if(project.Variables["object_id"].Value=="53")v="113";			
					else if(project.Variables["object_id"].Value=="12"||project.Variables["object_id"].Value=="13"||project.Variables["object_id"].Value=="14")v="116";
					else if(project.Variables["object_id"].Value=="8")v="120";		
					else if(project.Variables["object_id"].Value=="7"||project.Variables["object_id"].Value=="9")v="121";		
					else v="119";
					break;		
				
				
				
				
			}
			
			
			HtmlElement he;
			Random r = new Random();
			
			Tab tab = instance.ActiveTab;
			if (tab.IsBusy) tab.WaitDownloading();
			tab.Navigate("https://2-999-999.ru/personal/board/?add-object", "");
			if (tab.IsBusy) tab.WaitDownloading();

			
			
			
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("subrubric-widget");
			if (he.IsVoid) he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("subrubric");
			if (he.IsVoid) he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("select", "InnerText", "Выберите\\ подрубрику\\ Сдам\\ 1-комнатную\\ квартиру\\ Сдам\\ 2-комнатную\\ квартиру\\ Сдам\\ 3-комнатную\\ квартиру\\ Сдам\\ 4-комнатную\\ квартиру\\ Сдам\\ гостинку,\\ секционку\\ Сдам\\ дом,\\ дачу\\ Сдам\\ комнату,\\ подселение\\ Сдам\\ нежилое\\ Сниму\\ 1-комнатную\\ квартиру\\ Сниму\\ 2-комнатную\\ квартиру\\ Сниму\\ 3-комнатную\\ и\\ больше\\ Сниму\\ гостинку,\\ секционку\\ Сниму\\ дом,\\ дачу\\ Сниму\\ комнату,\\ подселение\\ Сниму\\ нежилое\\ Найдено\\ Потеряно\\ IT,\\ компьютеры,\\ ПО\\ Автосервис\\ Бухгалтерия,\\ 1С,\\ кассовый\\ учет\\ Водители,\\ курьеры,\\ логистики\\ Воспитатели,\\ няни,\\ педагоги,\\ тренеры\\ Директора,\\ руководители\\ Инженеры,\\ технологи,\\ ЖКХ\\ Люди\\ искусства\\ Медицина,\\ фармацевтика\\ Менеджеры\\ по\\ персоналу,\\ кадровики\\ Менеджеры\\ по\\ продажам\\ Общепит,\\ повара,\\ официанты\\ Операторы,\\ администраторы\\ Охрана,\\ полиция,\\ безопасность\\ Персонал\\ для\\ дома\\ Производство,\\ сборка,\\ монтаж\\ Работа\\ активным\\ Разнорабочие,\\ технический\\ персонал\\ Реклама,\\ маркетинг,\\ PR\\ Риэлторы,\\ недвижимость\\ Секретари,\\ помощники,\\ офис-менеджеры\\ СМИ,\\ журналисты,\\ редакторы\\ Стройка,\\ отделка,\\ ремонт\\ Сфера\\ услуг,\\ сервис\\ для\\ населения\\ Торговля,\\ торговые\\ представители,\\ склад\\ Туризм,\\ гостиничный\\ бизнес\\ Финансы,\\ консалдинг,\\ аудит\\ Юриспруденция,\\ приставы\\ Друга/подругу\\ \\(знакомства\\)\\ Партнера\\ по\\ бизнесу\\ Разное\\ Автомобиль,\\ спецтехника\\ Бытовая\\ техника\\ \\ Материалы\\ разные\\ Мототехника\\ Оборудование\\ Оргтехника,\\ ПО\\ Разное\\ 1-комнатная\\ квартира\\ 2-комнатная\\ квартира\\ 3-комнатная\\ квартира\\ 4-комнатная\\ квартира\\ 5-комнатная\\ и\\ более\\ Гараж\\ Гостинка\\ Долевое\\ Доля\\ в\\ квартире\\ Дом,\\ коттедж\\ Жилье\\ за\\ границей\\ Жилье\\ у\\ моря\\ Земля,\\ дачный\\ участок\\ Комната\\ в\\ квартире\\ Комната\\ в\\ общежитии\\ Нежилое,\\ коммерческая\\ недвижимость\\ Квартира,\\ дом\\ Разное\\ Домашние\\ животные\\ Автозапчасти\\ Автоспецтехника\\ Автотранспорт\\ Автохимия,\\ ГСМ\\ Бизнес\\ Бытовая\\ техника\\ Детские\\ товары\\ Домашние\\ животные\\ Инструмент,\\ приборы\\ Мебель\\ Медицинское\\ оборудование\\ \\ Медпрепараты,\\ БАД\\ Металлопродукция\\ Музыкальные\\ инструменты\\ Оборудование\\ разное\\ Одежда,\\ обувь\\ Оргтехника,\\ ПО\\ Парфюмерия,\\ косметика\\ Пиломатериал\\ Пищевое\\ оборудование\\ \\ Продукты\\ питания\\ Разное\\ Спортинвентарь\\ Средства\\ связи\\ Стройматериалы\\ Сувениры\\ Ткани,\\ нитки\\ Туристическое\\ снаряжение\\ Украшения,\\ аксессуары\\ Цветы,\\ растения,\\ семена\\ 1-комнатная\\ квартира\\ 2-комнатная\\ квартира\\ 3-комнатная\\ квартира\\ 4-комнатная\\ квартира\\ 5-комнатная\\ квартира\\ и\\ более\\ Гараж\\ Гостинка\\ Дача\\ Дача,\\ земля,\\ дачный\\ участок\\ Долевое\\ Доля\\ в\\ квартире\\ Дом,\\ коттедж\\ Жилье\\ за\\ границей\\ Жилье\\ у\\ моря\\ Коммерческая\\ недвижимость\\ Комната\\ в\\ квартире\\ Комната\\ в\\ общежитии\\ Авто,\\ лимузины,\\ снегоходы\\ Разное\\ Автомобили\\ Бытовая\\ техника\\ Квартиры,\\ офисы\\ Оргтехника\\ Разное\\ Аудио,\\ видео,\\ фотосервис\\ Гадание,\\ магия\\ Грузоперевозки\\ Домашние\\ животные\\ Клининг\\ Ковка,\\ сварка\\ Консалтинг,\\ аудит,\\ бухгалтерские\\ услуги\\ Кредит,\\ заём\\ Медицина\\ Металлообработка\\ Обучение,\\ дипломы,\\ переводы\\ Охрана,\\ безопасность\\ Праздники\\ Программисты,\\ системные\\ администраторы\\ Проектирование,\\ дизайн\\ Разные\\ услуги\\ Реклама,\\ полиграфия\\ Салон\\ красоты\\ Строительство,\\ производство,\\ монтаж\\ Туризм,\\ отдых\\ Утилизация\\ Юридические\\ услуги,\\ адвокаты", "regexp", 0);
			if (he.IsVoid)he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("select", "fulltag", "select", "text", 0);
			
			instance.WaitFieldEmulationDelay();
			he.SetValue(v, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			int p=int.Parse(project.Variables["photos"].Value);
			if(p>0){
				instance.SetFileUploadPolicy("ok", "");
				instance.SetFilesForUpload(project.Path + "photos\\\\" + project.Variables["id"].Value + "_1.jpg");
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("file-widget");
				if (he.IsVoid)he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("file");
				if (he.IsVoid)he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:file", "fulltag", "input:file", "text", 0);
				
				instance.WaitFieldEmulationDelay();
				he.RiseEvent("click", instance.EmulationLevel);
				System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);	
			}
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("name-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("name");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 0);
			}
			
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Profile.Name, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("phone-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("phone");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:text", "fulltag", "input:text", "text", 1);
			}
			
			instance.WaitFieldEmulationDelay();
			he.SetValue("8" + project.Variables["adv_number"].Value, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			// описание
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("text-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("text");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("textarea", "fulltag", "textarea", "text", 0);
			}
			
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["inet_title"].Value + "\r\n" + project.Variables["description"].Value, instance.EmulationLevel, false);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);
			// Клик на "Сохранить"
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildById("submit-btn-widget");
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("submit-btn");
			}
			if (he.IsVoid) {
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:submit", "fulltag", "input:submit", "text", 0);
			}
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			System.Threading.Thread.Sleep(r.Next(1, 2) * 1000);	
		}
		public void save_form_2_999_999()
		{
			string query=string.Empty;
			string text=string.Empty;
			if(instance.ActiveTab.DomText.Contains("Вы превысили свой суточный лимит")){
				query="update `si_"+project.Variables["site"].Value+"` set `day_limit`=0 where `id`='"+project.Variables["id_profile"].Value+"';";
				ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, " ", "\r\n");
				text="<u>Достигнут суточный лимит на 2-999-999.ru</u>";
			}
			text="Объявление <u>"+project.Variables["inet_title"].Value+"</u> успешно размещено на 2-999-999.ru";
			string url=project.Variables["url_for_notifications"].Value+project.Variables["user_id"].Value+"&msg="+text;
			string responce=string.Empty;
			responce=ZennoPoster.HttpGet(
				url,//url
				"",//proxy
				"utf-8",//charset
				ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly
			);
			string DB_set=project.Variables["DB_set"].Value;
			string site=project.Variables["site"].Value;
			string id_profile=project.Variables["id_profile"].Value;
			string objects_id_rk=string.Empty;
			query="select `objects_id_rk` from `si_"+site+"` where `id`='"+id_profile+"';";
			objects_id_rk=ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, " ", "\r\n");
			if(objects_id_rk=="")objects_id_rk=project.Variables["id"].Value;
			else objects_id_rk=objects_id_rk+"\r\n"+project.Variables["id"].Value;
			query="update `si_"+site+"` set `objects_count`=`objects_count`+1,  `objects_id_rk`='"+objects_id_rk+"',`run`='0' where `id`='"+project.Variables["id_profile"].Value+"';\r\nUPDATE `counters` SET `Gposts`=`Gposts`+1, `"+project.Variables["site"].Value+"P`=`"+project.Variables["site"].Value+"P`+1  WHERE  1;\r\nUPDATE `DB_users` SET `connects`=`connects`-1 WHERE  `value`='"+DB_set+"';\r\nupdate `si_"+project.Variables["site"].Value+"` set `day_limit`=`day_limit`-1 where `id`='"+project.Variables["id_profile"].Value+"';\r\nUPDATE  `objects` set `"+site+"`=1 where `id`='"+project.Variables["id"].Value+"';";
			ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, " ", "\r\n");
			project.Profile.Save(project.Directory+@"\profile\"+project.Variables["id_profile"].Value+".zpprofile");
		}
		public void save_reg(string site_for_cookie, string log, string pas)
		{
			project.Profile.Save(project.Directory+@"\profile\"+project.Variables["id_profile"].Value+".zpprofile");
			string Cookie=instance.GetCookie(site_for_cookie,true).ToString();
			string query=string.Empty;
			ZennoPoster.Db.ExecuteQuery("update `si_"+project.Variables["site"].Value+"` set  `log`='"+log+"', `pas`='"+pas+"', `cookie`='"+Cookie+"'  where `id`='"+project.Variables["id_profile"].Value+"';\r\nUPDATE `counters` SET `"+project.Variables["site"].Value+"R`=`"+project.Variables["site"].Value+"R`+1 WHERE  1;", null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, "", "\r\n");
			project.Variables["log"].Value=project.Profile.Login;
			project.Variables["pas"].Value=project.Profile.EmailPassword;
			project.Variables["cookie"].Value=Cookie;
			
			
		}
		public void good_captcha()
		{
			ZennoPoster.Db.ExecuteQuery("UPDATE `counters` SET  `Gcaptcha`=`Gcaptcha`+1 WHERE  1;", null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, "", "\r\n");
		}
		public void bad_captcha()
		{
			ZennoPoster.Db.ExecuteQuery("UPDATE `counters` SET `Bcaptcha`=`Bcaptcha`+1 WHERE  1;", null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, " ", "\r\n");
			ZennoPoster.HttpGet(project.Variables["bad_captcha"].Value);
		}
		public void error()
		{
			var taB=project.Tables["temp"];
			var error = project.GetLastError();
			var tmp = "";
			string query=string.Empty;
			string DB_set=project.Variables["DB_set"].Value;
			string id=string.Empty;
			if(error != null){
				tmp = string.Format("ActionId: {0}", error.ActionId);
				query="SELECT `id` from `log_errors` where `project`='"+project.Name+"' and `action`='"+tmp+"';";
				id=ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, " ", "\r\n");
				if(id==""){
					taB.Clear();
					query="INSERT INTO `log_errors` (`project`, `action`, `mes`,`object_id`) VALUES ('"+project.Name+"', '"+tmp+"', '"+instance.ActiveTab.DomText+"','"+project.Variables["id"].Value+"');\r\nSELECT `chat_id`,`token` from `admin_tg` where 1;";
					ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, ref taB);
					string chat_tg=taB.GetCell(0,0);
					string Btok_tg=taB.GetCell(1,0);
					string message="new error|"+project.Name+"|"+tmp;
					ZennoPoster.HttpGet(
						"https://api.telegram.org/bot"+Btok_tg+"/sendmessage?chat_id="+chat_tg+"&text="+message,//url
						"",//proxy
						"utf-8",//charset
						ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly
					);
				}
			}
			query="UPDATE `DB_users` SET `connects`=`connects`-1 WHERE  `value`='"+DB_set+"';\r\nupdate `si_"+project.Variables["site"].Value+"` set `run`='0' where `id`='"+project.Variables["id_profile"].Value+"';\r\nUPDATE `counters` SET `errors`=`errors`+1,`Bposts`=`Bposts`+1 WHERE  1;\r\nUPDATE  `objects` set `"+project.Variables["site"].Value+"`=3 where `id`='"+project.Variables["id"].Value+"';";
			ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, DB_set, " ", "\r\n");
		}	

	}
	class instance_limits
	{
		private Instance instance;
		private IZennoPosterProjectModel project;
		public instance_limits(Instance instance, IZennoPosterProjectModel project)
		{
			this.instance=instance;
			this.project=project;
		}
		public void limit_2_999_999()
		{
			project.Variables["action"].Value="check_limit";
			string query="SELECT  `day_limit`  FROM `si_"+project.Variables["site"].Value+"` where `id`='"+project.Variables["id_profile"].Value+"';";
			query=ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, " ", "\r\n");
			int l=int.Parse(query);
			if(l==0) 
			{
				project.SendInfoToLog("Достигнут суточный лимит",true);
				project.Variables["alert"].Value="limit";
				string text="<u>Достигнут суточный лимит на 2-999-999.ru</u>";
				string url=project.Variables["url_for_notifications"].Value+project.Variables["user_id"].Value+"&msg="+text;
				ZennoPoster.HttpGet(
				url,//url
				"",//proxy
				"utf-8",//charset
				ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly
				);
				query="UPDATE `DB_users` SET `connects`=`connects`-1 WHERE  `value`='"+project.Variables["DB_set"].Value+"';\r\nupdate `si_"+project.Variables["site"].Value+"` set `run`='0' where `id`='"+project.Variables["id_profile"].Value+"';\r\nUPDATE  `objects` set `"+project.Variables["site"].Value+"`=1 where `id`='"+project.Variables["id"].Value+"';";
				ZennoPoster.Db.ExecuteQuery(query, null, ZennoLab.InterfacesLibrary.Enums.Db.DbProvider.MySqlClient, project.Variables["DB_set"].Value, " ", "\r\n");	
				throw new Exception("достигнут суточный лимит");
			}
		}
	}
	class instance_proxy
	{
		private Instance instance;
		private IZennoPosterProjectModel project;
		public instance_proxy(Instance instance, IZennoPosterProjectModel project)
		{
			this.instance=instance;
			this.project=project;
		}
		public void get_proxy()//выполнение вложенного проэкта(запрос прокси из тор)
		{
			project.Variables["action"].Value="get_proxy";
			var mapVars = new List<Tuple<string, string>>();         
			mapVars.Add(new Tuple<string, string>("proxy", "proxy"));
			mapVars.Add(new Tuple<string, string>("site_to_test", "site"));
			project.ExecuteProject(project.Directory+ @"\proxy\proxy.xmlz",mapVars,false,true,false);
		}	
	}
	class browser_pref
	{
		private Instance instance;
		private IZennoPosterProjectModel project;
		public browser_pref(Instance instance, IZennoPosterProjectModel project)
		{
			this.instance=instance;
			this.project=project;
		}
		public void b_2_999_999(bool pic)
		{
			instance.SetProxy(project.Variables["proxy"].Value);
			instance.ClearCookie("2-999-999.ru");
			var domains = new []{ "2-999-999.ru" };
			instance.SetContentPolicy("WhiteList", domains, null);
			project.Profile.Load(project.Directory+@"\profile\"+project.Variables["id_profile"].Value+".zpprofile");
			instance.LoadPictures = pic;
			instance.AllowPopUp = false;
			instance.DownloadFrame = false;
			instance.UseCSS = false;
			instance.UseMedia = false;
			instance.UseAdds = false;
			instance.UsePluginsForceWmode = false;
			instance.UsePlugins = false;
			instance.UseJavaScripts = false;
		}
	}	
}