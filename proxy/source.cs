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

namespace ZennoLab.OwnCode
{
	class bol
	{
		private Instance instance;
		private IZennoPosterProjectModel project;
		public bol(Instance instance, IZennoPosterProjectModel project)
		{
			this.instance=instance;
			this.project=project;
		}
		
		
		public static object SyncObject = new object();
		public static object ProxyLocker = new object();
		public static object AccLocker = new object();
		public static object RecipLocker = new object();
		public static object LogFile = new object();
		public static object SubjLocker = new object();
		public static object LinkLocker = new object();
		public static object TextLocker = new object();
		public static object UbodyLocker = new object();
		
		
		
		
		

		
		

		
		public void s_input()
		{///входные настройки
			bol p=new bol(instance,project);
			project.Variables["bodyO"].Value=System.IO.File.ReadAllText(project.Variables["body"].Value);
			var e=project.Tables["email"];
			lock(AccLocker)
			{
				project.Variables["log"].Value=e.GetCell(0,0);
				project.Variables["pas"].Value=e.GetCell(1,0);
				e.DeleteRow(0);
				e.AddRow(project.Variables["log"].Value+";"+project.Variables["pas"].Value);
			}
			p.l("prepare");
			p.lo("account="+project.Variables["log"].Value+";"+project.Variables["pas"].Value);
		}	
		public void s_profile()
		{//профиль
			bol l=new bol(instance,project);
			l.l("check_profile");
			Random rr=new Random();
			if (!File.Exists(project.Directory+@"/bol/profile/"+project.Variables["log"].Value+".zpprofile"))
			{
				l.l("check_profile->create");
				int birth;
				string date0, date = DateTime.Parse("1980-01-01 05:07:13").Add(TimeSpan.FromSeconds((rr.Next(86400, (int)(DateTime.Parse("1991-01-01 05:07:13").Subtract(DateTime.Parse("1980-01-01 05:07:13")).TotalSeconds))))).ToString();
				DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss");
				date0 =new Regex(@".*?(?=\.)").Match(date).Value;
				birth= int.Parse(date0);
				project.Profile.BornDay = birth;
				date = new Regex(@"(?<=\.).*?(?=\ )").Match(date).Value;
				date0 = new Regex(@".*(?=\.)").Match(date).Value;
				birth= int.Parse(date0);
				project.Profile.BornMonth = birth;
				date0 = new Regex(@"(?<=\.).*").Match(date).Value;
				birth= int.Parse(date0);
				project.Profile.BornYear = birth;
				birth = (int)(DateTime.Today - new DateTime(project.Profile.BornYear, project.Profile.BornMonth, project.Profile.BornDay)).TotalSeconds;
				birth= birth/31536000;
				project.Profile.Age = birth;
				/*
					if(project.Variables["male"].Value=="1"){//имя фамилия мужской
						project.Profile.Sex =ZennoLab.InterfacesLibrary.ProjectModel.Enums.ProfileSex.Male;
						string[] surnameSet = {
							"Иванов",
							"Смирнов",
							"Кузнецов",
							"Попов",
							"Васильев",
							"Петров",
							"Соколов",
							"Михайлов",
							"Новиков",
							"Федоров",
							"Морозов",
							"Волков",
							"Алексеев",
							"Лебедев",
							"Семенов",
							"Егоров",
							"Павлов",
							"Козлов",
							"Степанов",
							"Николаев",
							"Орлов",
							"Андреев",
							"Макаров",
							"Никитин",
							"Захаров",
							"Зайцев",
							"Соловьев",
							"Борисов",
							"Яковлев",
							"Григорьев",
							"Романов",
							"Воробьев",
							"Сергеев",
							"Кузьмин",
							"Фролов",
							"Александров",
							"Дмитриев",
							"Королев",
							"Гусев",
							"Киселев",
							"Ильин",
							"Максимов",
							"Поляков",
							"Сорокин",
							"Виноградов",
							"Ковалев",
							"Белов",
							"Медведев",
							"Антонов",
							"Тарасов",
							"Жуков",
							"Баранов",
							"Филиппов",
							"Комаров",
							"Давыдов",
							"Беляев",
							"Герасимов",
							"Богданов",
							"Осипов",
							"Сидоров",
							"Матвеев",
							"Титов",
							"Марков",
							"Миронов",
							"Крылов",
							"Куликов",
							"Карпов",
							"Власов",
							"Мельников",
							"Денисов",
							"Гаврилов",
							"Тихонов",
							"Казаков",
							"Афанасьев",
							"Данилов",
							"Савельев",
							"Тимофеев",
							"Фомин",
							"Чернов",
							"Абрамов",
							"Мартынов",
							"Ефимов",
							"Федотов",
							"Щербаков",
							"Назаров",
							"Калинин",
							"Исаев",
							"Чернышев",
							"Быков",
							"Маслов",
							"Родионов",
							"Коновалов",
							"Лазарев",
							"Воронин",
							"Климов",
							"Филатов",
							"Пономарев",
							"Голубев",
							"Кудрявцев",
							"Прохоров",
							"Наумов",
							"Потапов",
							"Журавлев",
							"Овчинников",
							"Трофимов",
							"Леонов",
							"Соболев",
							"Ермаков",
							"Колесников",
							"Гончаров",
							"Емельянов",
							"Никифоров",
							"Грачев",
							"Котов",
							"Гришин",
							"Ефремов",
							"Архипов",
							"Громов",
							"Кириллов",
							"Малышев",
							"Панов",
							"Моисеев",
							"Румянцев",
							"Акимов",
							"Кондратьев",
							"Бирюков",
							"Горбунов",
							"Анисимов",
							"Еремин",
							"Тихомиров",
							"Галкин",
							"Лукьянов",
							"Михеев",
							"Скворцов",
							"Юдин",
							"Белоусов",
							"Нестеров",
							"Симонов",
							"Прокофьев",
							"Харитонов",
							"Князев",
							"Цветков",
							"Левин",
							"Митрофанов",
							"Воронов",
							"Аксенов",
							"Софронов",
							"Мальцев",
							"Логинов",
							"Горшков",
							"Савин",
							"Краснов",
							"Майоров",
							"Демидов",
							"Елисеев",
							"Рыбаков",
							"Сафонов",
							"Плотников",
							"Демин",
							"Хохлов",
							"Фадеев",
							"Молчанов",
							"Игнатов",
							"Литвинов",
							"Ершов",
							"Ушаков",
							"Дементьев",
							"Рябов",
							"Мухин",
							"Калашников",
							"Леонтьев",
							"Лобанов",
							"Кузин",
							"Корнеев",
							"Евдокимов",
							"Бородин",
							"Платонов",
							"Некрасов",
							"Балашов",
							"Бобров",
							"Жданов",
							"Блинов",
							"Игнатьев",
							"Коротков",
							"Муравьев",
							"Крюков",
							"Беляков",
							"Богомолов",
							"Дроздов",
							"Лавров",
							"Зуев",
							"Петухов",
							"Ларин",
							"Никулин",
							"Серов",
							"Терентьев",
							"Зотов",
							"Устинов",
							"Фокин",
							"Самойлов",
							"Константинов",
							"Сахаров",
							"Шишкин",
							"Самсонов",
							"Черкасов",
							"Чистяков",
							"Носов",
							"Спиридонов",
							"Карасев",
							"Авдеев",
							"Воронцов",
							"Зверев",
							"Владимиров",
							"Селезнев",
							"Нечаев",
							"Кудряшов",
							"Седов",
							"Фирсов",
							"Андрианов",
							"Панин",
							"Головин",
							"Терехов",
							"Ульянов",
							"Шестаков",
							"Агеев",
							"Никонов",
							"Селиванов",
							"Баженов",
							"Гордеев",
							"Кожевников",
							"Пахомов",
							"Зимин",
							"Костин",
							"Широков",
							"Филимонов",
							"Ларионов",
							"Овсянников",
							"Сазонов",
							"Суворов",
							"Нефедов",
							"Корнилов",
							"Любимов",
							"Львов",
							"Горбачев",
							"Копылов",
							"Лукин",
							"Токарев",
							"Кулешов",
							"Шилов",
							"Большаков",
							"Панкратов",
							"Родин",
							"Шаповалов",
							"Покровский",
							"Бочаров",
							"Никольский",
							"Маркин",
							"Горелов",
							"Агафонов",
							"Березин",
							"Ермолаев",
							"Зубков",
							"Куприянов",
							"Трифонов",
							"Масленников",
							"Круглов",
							"Третьяков",
							"Колосов",
							"Рожков",
							"Артамонов",
							"Шмелев",
							"Лаптев",
							"Лапшин",
							"Федосеев",
							"Зиновьев",
							"Зорин",
							"Уткин",
							"Столяров",
							"Зубов",
							"Ткачев",
							"Дорофеев",
							"Антипов",
							"Завьялов",
							"Свиридов",
							"Золотарев",
							"Кулаков",
							"Мещеряков",
							"Макеев",
							"Дьяконов",
							"Гуляев",
							"Петровский",
							"Бондарев",
							"Поздняков",
							"Панфилов",
							"Кочетков",
							"Суханов",
							"Рыжов",
							"Старостин",
							"Калмыков",
							"Колесов",
							"Золотов",
							"Кравцов",
							"Субботин",
							"Шубин",
							"Щукин",
							"Лосев",
							"Винокуров",
							"Лапин",
							"Парфенов",
							"Исаков",
							"Голованов",
							"Коровин",
							"Розанов",
							"Артемов",
							"Козырев",
							"Русаков",
							"Алешин",
							"Крючков",
							"Булгаков",
							"Кошелев",
							"Сычев",
							"Синицын",
							"Черных",
							"Рогов",
							"Кононов",
							"Лаврентьев",
							"Евсеев",
							"Пименов",
							"Пантелеев",
							"Горячев",
							"Аникин",
							"Лопатин",
							"Рудаков",
							"Одинцов",
							"Серебряков",
							"Панков",
							"Дегтярев",
							"Орехов",
							"Царев",
							"Шувалов",
							"Кондрашов",
							"Горюнов",
							"Дубровин",
							"Голиков",
							"Курочкин",
							"Латышев",
							"Севастьянов",
							"Вавилов",
							"Ерофеев",
							"Сальников",
							"Клюев",
							"Носков",
							"Озеров",
							"Кольцов",
							"Комиссаров",
							"Меркулов",
							"Киреев",
							"Хомяков",
							"Булатов",
							"Ананьев",
							"Буров",
							"Шапошников",
							"Дружинин",
							"Островский",
							"Шевелев",
							"Долгов",
							"Суслов",
							"Шевцов",
							"Пастухов",
							"Рубцов",
							"Бычков",
							"Глебов",
							"Ильинский",
							"Успенский",
							"Дьяков",
							"Кочетов",
							"Вишневский",
							"Высоцкий",
							"Глухов",
							"Дубов",
							"Бессонов",
							"Ситников",
							"Астафьев",
							"Мешков",
							"Шаров",
							"Яшин",
							"Козловский",
							"Туманов",
							"Басов",
							"Корчагин",
							"Болдырев",
							"Олейников",
							"Чумаков",
							"Фомичев",
							"Губанов",
							"Дубинин",
							"Шульгин",
							"Касаткин",
							"Пирогов",
							"Семин",
							"Трошин",
							"Горохов",
							"Стариков",
							"Щеглов",
							"Фетисов",
							"Колпаков",
							"Чесноков",
							"Зыков",
							"Верещагин",
							"Минаев",
							"Руднев",
							"Троицкий",
							"Окулов",
							"Ширяев",
							"Малинин",
							"Черепанов",
							"Измайлов",
							"Алехин",
							"Зеленин",
							"Касьянов",
							"Пугачев",
							"Павловский",
							"Чижов",
							"Кондратов",
							"Воронков",
							"Капустин",
							"Сотников",
							"Демьянов",
							"Косарев",
							"Беликов",
							"Сухарев",
							"Белкин",
							"Беспалов",
							"Кулагин",
							"Савицкий",
							"Жаров",
							"Хромов",
							"Еремеев",
							"Карташов",
							"Астахов",
							"Русанов",
							"Сухов",
							"Вешняков",
							"Волошин",
							"Козин",
							"Худяков",
							"Жилин",
							"Малахов",
							"Сизов",
							"Ежов",
							"Толкачев",
							"Анохин",
							"Вдовин",
							"Бабушкин",
							"Усов",
							"Лыков",
							"Горлов",
							"Коршунов",
							"Маркелов",
							"Постников",
							"Черный",
							"Дорохов",
							"Свешников",
							"Гущин",
							"Калугин",
							"Блохин",
							"Сурков",
							"Кочергин",
							"Греков",
							"Казанцев",
							"Швецов",
							"Ермилов",
							"Парамонов",
							"Агапов",
							"Минин",
							"Корнев",
							"Черняев",
							"Гуров",
							"Ермолов",
							"Сомов",
							"Добрынин",
							"Барсуков",
							"Глушков",
							"Чеботарев",
							"Москвин",
							"Уваров",
							"Безруков",
							"Муратов",
							"Раков",
							"Снегирев",
							"Гладков",
							"Злобин",
							"Моргунов",
							"Поликарпов",
							"Рябинин",
							"Судаков",
							"Кукушкин",
							"Калачев",
							"Грибов",
							"Елизаров",
							"Звягинцев",
							"Корольков",
							"Федосов"};
						string surname = surnameSet[r.Next(0,surnameSet.Length)].ToString();
						project.Profile.Surname = surname;
					}
					else{//имя фамилия женский
						project.Profile.Sex =ZennoLab.InterfacesLibrary.ProjectModel.Enums.ProfileSex.Female;
						string[] surnameSet = {
							"Орлова",
							"Лебедева",
							"Симонова",
							"Александрова",
							"Третьякова",
							"Ленская",
							"Каменских",
							"Кожевникова",
							"Денисова",
							"Андреева",
							"Толмачева",
							"Шевченко",
							"Панченко",
							"Назарова",
							"Безрукова",
							"Соколова",
							"Родочинская",
							"Волкова",
							"Ковалевская",
							"Обломова",
							"Королева",
							"Волочкова",
							"Матвеева",
							"Левченко",
							"Лионова",
							"Котова",
							"Братиславская",
							"Полякова",
							"Ефимова",
							"Малышева",
							"Тарасова",
							"Новицкая",
							"Новикова",
							"Истомина",
							"Ивлева",
							"Ульянова",
							"Романова",
							"Гронская",
							"Бондаренко",
							"Хованская",
							"Смирнова",
							"Генералова",
							"Игнатова",
							"Краснова",
							"Белова",
							"Маслова",
							"Марченко",
							"Карпова",
							"Поднебесная",
							"Кольцова",
							"Морозова",
							"Демидова",
							"Рубенцова",
							"Макарова",
							"Иванова",
							"Новак",
							"Максимчук",
							"Потапова",
							"Меньшова",
							"Тимошенко",
							"Малиновская",
							"Абрамова",
							"Кручинина",
							"Дарова",
							"Власова",
							"Ильина",
							"Маркова",
							"Островская",
							"Мищенко",
							"Васнецова",
							"Ларина",
							"Добровольская",
							"Сафарова",
							"Тимофеева",
							"Куликовская",
							"Преображенская",
							"Коновалова",
							"Руденко",
							"Волощук",
							"Шведова",
							"Коваль",
							"Анисимова",
							"Никитина",
							"Преснякова",
							"Владова",
							"Прохорова",
							"Орловская",
							"Химченко",
							"Аркадьева",
							"Галактионова",
							"Федорова",
							"Фролова",
							"Третьякова",
							"Николаева",
							"Софийская",
							"Соболева",
							"Кузнецова",
							"Чайковская",
							"Зайцева",
							"Аверина",
							"Князева",
							"Меркулова",
							"Чернышова",
							"Белокрылова",
							"Дмитриева",
							"Соколович",
							"Ковальчук",
							"Сафронова",
							"Каменская",
							"Романенко",
							"Бондаренко",
							"Вершинина",
							"Соловьева",
							"Голубева",
							"Федосеева",
							"Чехова",
							"Павленко",
							"Меркулова",
							"Кармазина",
							"Канаева",
							"Наварская",
							"Суворова",
							"Рощина",
							"Ленина",
							"Марченко",
							"Дементьева",
							"Архипова",
							"Шорохова",
							"Снежная",
							"Кудрявцева",
							"Дубровская",
							"Минаева",
							"Мечникова",
							"Путина",
							"Богданова",
							"Исаева",
							"Солнцева",
							"Бердинских",
							"Крымская",
							"Швец",
							"Платонова",
							"Пугачева",
							"Тихомирова",
							"Давыдова",
							"Ржевская",
							"Аверина",
							"Добровольская",
							"Садовская",
							"Дроздова",
							"Бородина",
							"Брежнева",
							"Баринова",
							"Доценко",
							"Вишневская",
							"Шевченко",
							"Абрамович",
							"Берестова",
							"Сочинская",
							"Данилова",
							"Радецкая",
							"Репина",
							"Литковская",
							"Владимирова",
							"Михайлова",
							"Шанская",
							"Любимова",
							"Громова",
							"Астахова",
							"Высоцкая",
							"Городецкая",
							"Жукова",
							"Сомова",
							"Звездная",
							"Шаповалова",
							"Галицына",
							"Быстрова",
							"Зорина",
							"Сахарова",
							"Жданова",
							"Редкая",
							"Медведева",
							"Титова",
							"Стацевич",
							"Крылова",
							"Байко",
							"Литвинова",
							"Радионова",
							"Зимина",
							"Булгакова",
							"Ермолаева",
							"Шин",
							"Сидорова",
							"Филлипова",
							"Питерская",
							"Маврина",
							"Альмова",
							"Просветова",
							"Лермонтова",
							"Охотникова",
							"Фомина",
							"Войтова",
							"Богатырева",
							"Царева",
							"Градова",
							"Екимова",
							"Соболь",
							"Осипова",
							"Андрианова",
							"Якубович",
							"Виноградова",
							"Федорчук",
							"Герасименко",
							"Бойцова",
							"Оленникова",
							"Ермилова",
							"Пименова",
							"Дарьянова",
							"Чудина",
							"Лаврентьева",
							"Павловская",
							"Точеная",
							"Снегова",
							"Добронравова",
							"Алтырева",
							"Лазарева",
							"Калашникова",
							"Серебрянникова",
							"Вещая",
							"Чайка",
							"Дружинина",
							"Валинина",
							"Довлатова",
							"Варфоломеева",
							"Кротова",
							"Верховская",
							"Богачева",
							"Золотухина",
							"Милованова",
							"Матвиенко",
							"Казакова",
							"Захарова",
							"Белоусова",
							"Санина",
							"Ерошенко",
							"Лучная",
							"Шпагина",
							"Кутузова",
							"Емельянова",
							"Круглова",
							"Кароль",
							"Баскова",
							"Уланова",
							"Краснова",
							"Железная",
							"Гоголь",
							"Бровина",
							"Румянцева",
							"Подорожная"};
						string surname = surnameSet[r.Next(0,surnameSet.Length)].ToString();
						project.Profile.Surname = surname;
						
					}
				project.Profile.Name = project.Variables["name"].Value;	
				// настройки города
				if(project.Variables["city"].Value=="24"){//красноярск
					project.Profile.Country= "Россия";
					project.Profile.CurrentRegion= "Красноярский край";
					project.Profile.Town="Красноярск";
					project.Profile.ZipCode="660010";
				}
				*/
				project.Profile.Language="Pt";
				string version = Macros.TextProcessing.Spintax("{38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53}");
				Dictionary <string, string> buildIDSet = new Dictionary <string, string>();
				buildIDSet.Add("53", "20170413192749");
				buildIDSet.Add("52", "20170316213829");
				buildIDSet.Add("51", "20170125094131");
				buildIDSet.Add("50", "20161104212021");
				buildIDSet.Add("49", "20161019084923");
				buildIDSet.Add("48", "20160817112116");
				buildIDSet.Add("47", "20160623154057");
				buildIDSet.Add("46", "20160502172042");
				buildIDSet.Add("45", "20160905130425");
				buildIDSet.Add("44", "20160210153822");
				buildIDSet.Add("43", "20160105164030");
				buildIDSet.Add("42", "20151029151421");
				buildIDSet.Add("41", "20151014143721");
				buildIDSet.Add("40", "20150812163655");
				buildIDSet.Add("39", "20150618135210");
				buildIDSet.Add("38", "20150513174244");
				instance.ShowNavigatorField(ZennoLab.InterfacesLibrary.Enums.Browser.NavigatorField.BuildId);
				instance.SetHeader(ZennoLab.InterfacesLibrary.Enums.Browser.NavigatorField.BuildId, buildIDSet[version]);
				
				string winOC = Macros.TextProcessing.Spintax("{6.0|6.1|6.2|6.3|10.0}");
				
				string platform = Macros.TextProcessing.Spintax("{Win32|Win64}");
				
				string platformApp = string.Empty;
				
				if(platform=="Win32"){platformApp = Macros.TextProcessing.Spintax("{; WOW64|}");} else {platformApp = "; Win64; x64";}
				project.Profile.UserAgent = string.Format("Mozilla/5.0 (Windows NT {0}{1}; rv:{2}.0) Gecko/20100101 Firefox/{2}.0", winOC, platformApp, version);
				project.Profile.UserAgentAppVersion = "5.0 (Windows)";
				project.Profile.UserAgentAppName = "Netscape";
				project.Profile.UserAgentAppCodeName = "Mozilla";
				project.Profile.UserAgentProduct = "Gecko";
				project.Profile.UserAgentProductSub = "20100101";
				project.Profile.UserAgentOsCpu = string.Format("Windows NT {0}{1}", winOC, platformApp);
				project.Profile.UserAgentPlatform = platform;
				string[] acceptLanguageSet = {
					"pt-PT,pt;q=0.8,en-US;q=0.6,en;q=0.4",
					"pt-PT,pt;q=0.9,en;q=0.8",
					"pt-PT,pt;q=0.8,en-US;q=0.5,en;q=0.3"};
				string acceptLanguage = acceptLanguageSet[rr.Next(0, acceptLanguageSet.Length)].ToString();
				project.Profile.AcceptLanguage = acceptLanguage;
				project.Profile.UserAgentBrowserLanguage = acceptLanguage.Substring(0, acceptLanguage.IndexOf(','));
				project.Profile.UserAgentLanguage = acceptLanguage.Substring(0, acceptLanguage.IndexOf(','));
				int [,] resolutionSet = {
					{2880, 1800},
					{2560, 1600},
					{2560, 1440},
					{1920, 1200},
					{1920, 1080},
					{1680, 1050},
					{1600, 1200},
					{1600, 900},
					{1440, 900},
					{1366, 768},
					{1360, 768},
					{1280, 1024},
					{1280, 800},
					{1280, 768},
					{1152, 864},
					{1080, 1920},
					{1024, 768}};
				int resolution = rr.Next(resolutionSet.Length/2);
				project.Profile.ScreenSizeWidth = resolutionSet[resolution, 0];
				project.Profile.ScreenSizeHeight = resolutionSet[resolution, 1];
				project.Profile.AvailScreenWidth = project.Profile.ScreenSizeWidth-17;
				project.Profile.AvailScreenHeight = project.Profile.ScreenSizeHeight-40;
				instance.SetScreenPreference("screen_color_depth", 24);
				project.Profile.Save(project.Directory+ @"\profile\bol\"+project.Variables["log"].Value+".zpprofile", false, true, true, true, true, true, true, true, true);
				l.l("check_profile->save");
				l.lo("create_profile:"+project.Directory+ @"\profile\bol\"+project.Variables["log"].Value+".zpprofile");
			}
		}
		public void s_proxy()
		{//прокси
			bol l=new bol(instance,project);
			l.l("select_proxy");
			System.Diagnostics.ProcessStartInfo startInfo = new ProcessStartInfo();
			if(project.Variables["proxy_type"].Value=="nosok")
			{
				l.l("select_proxy->nosok");
				var prL=project.Lists["nosok"];
				do
				{
					lock(ProxyLocker)
					{
						project.Variables["proxy"].Value=prL[0];
						project.Variables["proxy"].Value="socks5://"+project.Variables["proxy"].Value;
						project.Variables["responce"].Value =ZennoPoster.HttpGet("https://www.bol.uol.com.br/",project.Variables["proxy"].Value,"",ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly);
						if(!project.Variables["responce"].Value.Contains(@"HTTP/1.1 200 OK"))
						{ 
							prL.RemoveAt(0);
							l.w("select_proxy->nosok->Bad proxy");
							project.Variables["proxy"].Value="";
						}
						else
						{
							prL.RemoveAt(0);
							prL.Add(project.Variables["proxy"].Value);
							l.l("select_proxy->nosok->Good proxy");
						}
					}
				}while(project.Variables["proxy"].Value=="");
			}
			else if(project.Variables["proxy_type"].Value=="tor")
			{
				l.l("select_proxy->tor");
				var list=project.Lists["tor_proxy"];
				do
				{
					lock(ProxyLocker)
					{
						project.Variables["proxy"].Value=list[0];
						if(project.Variables["proxy"].Value=="")
						{
							list.RemoveAt(0);
							project.Variables["proxy"].Value=list[0];
						}
						list.RemoveAt(0);
						list.Add(project.Variables["proxy"].Value);
						project.Variables["port"].Value=new Regex(@"(?<=:).*").Match(project.Variables["proxy"].Value).Value;
						project.Variables["proxy"].Value="socks5://"+project.Variables["proxy"].Value;
					}
					
					startInfo.FileName = string.Format(project.Directory+@"/proxy/data/tor-NEWNYM.exe");
					startInfo.WindowStyle = ProcessWindowStyle.Hidden;
					startInfo.Arguments="-port "+project.Variables["port"].Value;
					Process.Start(startInfo);
					System.Threading.Thread.Sleep(5*1000);
					project.Variables["responce"].Value =ZennoPoster.HttpGet("https://www.bol.uol.com.br/",project.Variables["proxy"].Value,"",ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly);
					if(!project.Variables["responce"].Value.Contains(@"HTTP/1.1 200 OK"))
					{ 
						l.w("select_proxy->tor->Bad proxy");
						project.Variables["proxy"].Value="";
					}
					else l.l("select_proxy->tor->Good proxy");
				}while(project.Variables["proxy"].Value=="");
			}
			l.lo("proxy:"+project.Variables["proxy"].Value+" type:"+project.Variables["proxy_type"].Value);
		}
		public void s_browser_prefence()
		{//настройки браузера
			bol l=new bol(instance,project);
			l.l("bol_browser_prefence");
			instance.ClearCookie("bol.uol.com.br");
			instance.SetProxy(project.Variables["proxy"].Value);
			project.Profile.Load(project.Directory+ @"\profile\bol\"+project.Variables["log"].Value+".zpprofile");
			instance.LoadPictures = false;
			instance.AllowPopUp = false;
			instance.DownloadFrame = false;
			instance.UseCSS = false;
			instance.UseJavaScripts = true;
			instance.UsePlugins = false;
			instance.UsePluginsForceWmode = false;
			instance.UseAdds = false;
			instance.UseMedia = false;
			var regexs = new []{ "uol.com" };
			instance.SetContentPolicy("WhiteList", null, regexs);
			l.lo("set_browser_prefence");
		}
		public void load_main_page()
		{///загрузка главной страцицы
			bol s=new bol(instance,project);
			s.l("load_main_page");
			Tab tab = instance.ActiveTab;
			tab.Navigate("https://bol.uol.com.br/", "");
			tab.WaitDownloading();
			s.lo("navigate:https://bol.uol.com.br/");
		}
		public void s_check_authoriz()
		{//проверка наличия авторизации у профиля
			bol l =new bol(instance,project);
			l.l("check_authoriz");
			HtmlElement he = instance.ActiveTab.FindElementByAttribute("a", "class", "couting", "regexp", 0);
			project.Variables["temp"].Value = he.GetAttribute("outerhtml");
			if(project.Variables["temp"].Value.Contains(@"<a href=""http://bmail.uol.com.br/checkin""")){
				he = instance.ActiveTab.FindElementByAttribute("a", "class", "access", "regexp", 0);
				instance.WaitFieldEmulationDelay();
				he.RiseEvent("click", instance.EmulationLevel);
				instance.ActiveTab.WaitDownloading();
				l.l("check_authoriz->true");
				project.Variables["alert"].Value="true";
				l.lo("authoriz->true");
			}
			else
			{
				he = instance.ActiveTab.FindElementByAttribute("span", "class", "count", "regexp", 0);
				project.Variables["temp"].Value = he.GetAttribute("outerhtml");
				project.Variables["temp"].Value=new Regex("[0-9]+").Match(project.Variables["temp"].Value).Value;
				if(project.Variables["temp"].Value!="")
				{
					he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("span", "class", "count", "regexp", 0);
					instance.WaitFieldEmulationDelay();
					he.RiseEvent("click", instance.EmulationLevel);
					instance.ActiveTab.WaitDownloading();
					l.l("check_authoriz->true");
					project.Variables["alert"].Value="true";
					l.lo("authoriz->true");
				}
				else if(instance.ActiveTab.DomText.Contains(@"Acessando o BOL Mail ..."))l.load_main_page();
				else
				{
					l.l("check_authoriz->false");
					project.Variables["alert"].Value="false";
					l.lo("authoriz->false");
					l.load_main_page();
				}
			}
			
			
		}
		public void s_authoriz()
		{//авторизация
			bol l=new bol(instance,project);
			l.l("authoriz");
			l.l("authoriz|set->log");
			HtmlElement he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("user");
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:email", "class", "mod-header-login-user", "regexp", 0);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:email", "fulltag", "input:email", "text", 0);
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["log"].Value, instance.EmulationLevel, false);
			instance.ActiveTab.WaitDownloading();
			l.l("authoriz|set->pas");
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByName("pass");
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:password", "class", "mod-header-login-pass", "regexp", 0);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("input:password", "fulltag", "input:password", "text", 0);
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["pas"].Value, instance.EmulationLevel, false);
			instance.ActiveTab.WaitDownloading();
			l.l("authoriz|click->entar");
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("button", "class", "mod-header-login-button", "regexp", 0);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("button", "InnerText", "ENTRAR", "regexp", 0);
			he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 1).FindChildByAttribute("button", "fulltag", "button", "text", 0);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			instance.ActiveTab.WaitDownloading();
			l.lo("set->log set->pas click->entar");
		}
		public void s_check_authoriz_validate()
		{//проверка валидности акка
			bol l=new bol(instance,project);
			l.l("check_authoriz_validate");
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("p", "innertext", @"Atenção\\ com\\ as\\ teclas\\ \""Caps\\ Lock\""\\ e\\ \""Shift\"",\\ pois\\ o\\ BOL\\ Mail\\ diferencia\\ letras\\ maiúsculas\\ e\\ minúsculas.", "regexp", 0);
			project.Variables["temp"].Value = he.GetAttribute("innertext");
			if(project.Variables["temp"].Value==@"Atenção com as teclas ""Caps Lock"" e ""Shift"", pois o BOL Mail diferencia letras maiúsculas e minúsculas.")
			{
				l.lo("bad_authoriz->try2");
				l.l("check_authoriz_validate->bad");
				l.l("check_authoriz_validate-try->authoriz");
				l.s_authoriz();
				he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("p", "innertext", @"Atenção\\ com\\ as\\ teclas\\ \""Caps\\ Lock\""\\ e\\ \""Shift\"",\\ pois\\ o\\ BOL\\ Mail\\ diferencia\\ letras\\ maiúsculas\\ e\\ minúsculas.", "regexp", 0);
				project.Variables["temp"].Value = he.GetAttribute("innertext");
				if(project.Variables["temp"].Value==@"Atenção com as teclas ""Caps Lock"" e ""Shift"", pois o BOL Mail diferencia letras maiúsculas e minúsculas.")
				{
					l.lo("bad_account");
					l.l("check_authoriz_validate->bad_account");
					var e=project.Tables["email"];
					project.Variables["temp"].Value=project.Variables["log"].Value+";"+project.Variables["pas"].Value;
					lock(AccLocker)
					{
						for(int i=0;i<e.RowCount;i++)
						{
							string text=e.GetRow(i).ToString();
							if(text==project.Variables["temp"].Value)
							{
								e.DeleteRow(i);
								break;
								
							}
						}
					}
					System.IO.File.Delete(project.Directory+@"/bol/profile/"+project.Variables["log"].Value+".zpprofile");
					var ba=project.Lists["bad_accs"];
					ba.Add(project.Variables["temp"].Value);
					l.lo("OK");
				}
				else {l.l("check_authoriz_validate->OK");l.lo("authoriz-.succes");}
			}
			else {l.l("check_authoriz_validate->OK");l.lo("authoriz-.succes");}
		}
		public void s_check_limit()
		{
			bol l =new bol(instance,project);
			l.l("check_limit");
			l.lo("check_limit");
			string txt="";
			int t=int.Parse(project.Variables["wait_to_next_post"].Value);
			if(t>0)
			{
				txt=string.Format(@"|left->{0}",t);
				t--;
				l.l("check_limit"+txt);
				l.lo("check_limit"+txt);
				project.Variables["wait_to_next_post"].Value=t.ToString();
			}
			else
			{
				l.l("check_limit->reached");
				l.lo("check_limit->reached");
				l.lo("OK");
				throw new Exception("исчерпан лимит для аккаунта");
			}
		}
		public void s_check_ads()
		{//проверка на рекламу
			bol l=new bol(instance,project);
			l.l("check_ads");
			HtmlElement he;
			Tab tab=instance.ActiveTab;
			project.Variables["temp"].Value = new Regex(@"Continuar navegando").Match(tab.DomText).Value;
			if (project.Variables["temp"].Value==@"Continuar navegando")
			{
				l.l("check_ads->true");
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "class", "button-back", "regexp", 0);
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "href", @"https://produtos.uol.com.br/porteiras/clube-uol/volta-as-aulas/\\?skin=bol-default&url=http://visitante.acesso.uol.com.br/refreshCookie.html\\?dest=WEBMAIL\\#", "regexp", 0);
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "InnerText", @"Continuar\\ navegando\\ »", "regexp", 0);
				he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("a", "fulltag", "a", "text", 1);
				instance.WaitFieldEmulationDelay();
				he.RiseEvent("click", instance.EmulationLevel);
				tab.WaitDownloading();
				l.lo("popup->ads");
			}
			else l.l("check_ads->false");
		}
		public void s_check_tutorial()
		{//проверка на обучение
			bol l=new bol(instance,project);
			System.Threading.Thread.Sleep(5 * 1000);
			l.l("check_tutorial->strart");
			l.lo("check_tutorial->strart");
			project.Variables["temp"].Value="";
			Tab tab=instance.ActiveTab;
			HtmlElement he;
			int tr=0;
			do{
				tr++;
				project.Variables["temp"].Value= new Regex(@"Finalizar").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"tFinalizar"))l.s_finalizar();
				project.Variables["temp"].Value= new Regex(@"Nesta coluna você verá sua conta principal e poderá adicionar outras contas de e-mail para serem acessadas, até mesmo sendo de outros provedores.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Nesta coluna você verá sua conta principal e poderá adicionar outras contas de e-mail para serem acessadas, até mesmo sendo de outros provedores."))l.s_proximo();
				
				project.Variables["temp"].Value= new Regex(@"As pastas de cada conta de e-mail ficam nesta área").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"As pastas de cada conta de e-mail ficam nesta área"))l.s_proximo();
				
				project.Variables["temp"].Value= new Regex(@"As barras na esquerda da listagem de e-mails").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"As barras na esquerda da listagem de e-mails"))l.s_proximo();				

				project.Variables["temp"].Value= new Regex(@"Saiba quanto espaço seus e-mails já ocupam na sua conta.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Saiba quanto espaço seus e-mails já ocupam na sua conta."))l.s_proximo();				

				project.Variables["temp"].Value= new Regex(@"Utilize esta função para eliminar e marcar os e-mails indesejados.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Utilize esta função para eliminar e marcar os e-mails indesejados."))l.s_proximo();
				
				project.Variables["temp"].Value= new Regex(@"Utilize este atalho para configurações, autorresposta e outras funções.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Utilize este atalho para configurações, autorresposta e outras funções."))l.s_proximo();
			
				project.Variables["temp"].Value= new Regex(@"Clique neste ícone sempre que quiser voltar para sua caixa de entrada.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Clique neste ícone sempre que quiser voltar para sua caixa de entrada."))l.s_proximo();

				project.Variables["temp"].Value= new Regex(@"Aqui você consulta e organiza sua lista de destinatários.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Aqui você consulta e organiza sua lista de destinatários."))l.s_proximo();

				project.Variables["temp"].Value= new Regex(@"Consulte e adicione compromissos, reuniões e lembretes.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"Consulte e adicione compromissos, reuniões e lembretes."))l.s_proximo();
				
				project.Variables["temp"].Value= new Regex(@"O sistema notifica quando você recebe novos e-mails.").Match(tab.DomText).Value;
				if(project.Variables["temp"].Value.Contains(@"O sistema notifica quando você recebe novos e-mails."))l.s_finalizar();
				
				
				if(project.Variables["temp"].Value!="1")
				{
					he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "innertext", @"\\r\\n\\t\\t\\tPróximo\\r\\n\\t\\t", "regexp", 0);
					project.Variables["temp"].Value = he.GetAttribute("innertext");
					if(project.Variables["temp"].Value.Contains(@"Próximo"))l.s_proximo();
					else if(tr>15)project.Variables["temp"].Value="1";
				}
			}while(project.Variables["temp"].Value!="1");
			l.l("check_tutorial->end");
			l.lo("check_tutorial->end");
		}
		public void s_finalizar()
		{//tutorial
			bol l=new bol(instance,project);
			System.Threading.Thread.Sleep(5*1000);
			l.l("check_tutorial->finaliza");
			Tab tab=instance.ActiveTab;
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "innertext", @"Finalizar", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "innerhtml", @"Finalizar", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "outerhtml", @"<button\\ ng-if=\""step\\+1\\ ==\\ tourSteps\""\\ class=\""modal-bt-confirm\\ default\\ ng-scope\""\\ ng-click=\""tourEnded\\(\\)\"">\\r\\n\\t\\t\\tFinalizar\\r\\n\\t\\t</button>", "regexp", 0);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			tab.WaitDownloading();
			project.Variables["temp"].Value="1";
		}
		public void s_proximo()
		{//tutorial
			bol l=new bol(instance,project);
			System.Threading.Thread.Sleep(2 * 1000);
			l.l("check_tutorial->proximo");
			Tab tab=instance.ActiveTab;
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "innertext", "Próximo", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "innerhtml", "Próximo", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("button", "outerhtml", @"<button\\ ng-if=\""step\\+1\\ <\\ tourSteps\""\\ class=\""modal-bt-confirm\\ default\\ ng-scope\""\\ ng-click=\""stepForward\\(\\)\"">\\r\\n\\t\\t\\tPróximo\\r\\n\\t\\t</button>", "regexp", 0);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			tab.WaitDownloading();
		}
		public void send_prepare()
		{
			bol l=new bol(instance,project);
			l.l("get_recip");
			l.get_recip();
			l.l("get_subj");
			l.get_subj();
			l.l("get_link");
			l.get_link();
			l.l("get_text_to_paste");
			l.get_text_to_paste();
			l.l("body_spint");
			l.body_spint();
			l.l("click_Escever");
			l.lo("click_Escever");
			project.Variables["temp"].Value= new Regex(@"Escrever").Match(instance.ActiveTab.DomText).Value;
			if(project.Variables["temp"].Value.Contains(@"Escrever"))l.s_click_Escrever();
			else throw new Exception("ошибка на кнопке написать письмо");
		}
		public void body_spint()
		{
			var s=project.Lists["unique_body"];
			project.Variables["body"].Value="";
			do
			{
				project.Variables["body"].Value= Macros.TextProcessing.Spintax(project.Variables["bodyO"].Value);
				if(project.Variables["link"].Value!="") project.Variables["body"].Value = Macros.TextProcessing.Replace(project.Variables["body"].Value, @"$PASTE_LINK$", project.Variables["link"].Value, "Text", "All");
				else
				{
					if(project.Variables["body"].Value.Contains(@"$PASTE_LINK$")) project.Variables["body"].Value = Macros.TextProcessing.Replace(project.Variables["body"].Value, @"$PASTE_LINK$", "", "Text", "All");
				}
				if(project.Variables["tex_paste"].Value!="") project.Variables["body"].Value = Macros.TextProcessing.Replace(project.Variables["body"].Value, @"$PASTE_TEXT$", project.Variables["tex_paste"].Value, "Text", "All");
				else
				{
					if(project.Variables["body"].Value.Contains(@"$PASTE_TEXT$")) project.Variables["body"].Value = Macros.TextProcessing.Replace(project.Variables["body"].Value, "$PASTE_TEXT$", "", "Text", "All");
				}
				lock(UbodyLocker)
				{
					if(s.Count==0)s.Add(project.Variables["body"].Value);
					else
					{
						for(int i=0;i<s.Count;i++)
						{
							project.Variables["temp"].Value=s.ElementAt(i);
							if(project.Variables["temp"].Value==project.Variables["body"].Value)
							{
								project.Variables["body"].Value="";
								break;
							}
						}
						if(project.Variables["body"].Value!="")
						{
							if(project.Variables["body"].Value!=project.Variables["temp"].Value)s.Add(project.Variables["body"].Value);
							else project.Variables["body"].Value="";
						}
					}
				}
			}while(project.Variables["body"].Value=="");
		}
		public void get_recip()
		{
			bol l=new bol(instance,project);
			l.l("get_recip");
			l.lo("get_recip");
			var r=project.Tables["recip"];
			if(r.RowCount==0)
			{
				l.l("end_recip_list");
				l.lo("end_recip_list");
				l.lo("OK");
				throw new Exception("Список получателей пуст");
			}
			else
			{
				lock(RecipLocker)
				{
					project.Variables["adresat"].Value=r.GetCell(0,0);
					project.Variables["adresat_name"].Value=r.GetCell(1,0);
					r.DeleteRow(0);
				}
				l.l("get_recip->"+project.Variables["adresat"].Value);
				l.lo("get_recip->"+project.Variables["adresat"].Value);
			}
			
		}
		public void get_subj()
		{
			bol l=new bol(instance,project);
			l.l("get_subj");
			l.lo("get_subj");
			var s=project.Lists["subj"];
				lock(SubjLocker)
				{
					project.Variables["subj"].Value=s.ElementAt(0);
					s.RemoveAt(0);
					s.Add(project.Variables["subj"].Value);
				}
				l.l("get_subj->"+project.Variables["subj"].Value);
				l.lo("get_subj->"+project.Variables["subj"].Value);
			
		}
		public void get_link()
		{
			bol l=new bol(instance,project);
			var s=project.Lists["link"];
			if(s.Count>0)
			{
				l.l("get_link");
				l.lo("get_link");
				lock(LinkLocker)
				{
					project.Variables["link"].Value=s.ElementAt(0);
					s.RemoveAt(0);
					s.Add(project.Variables["link"].Value);
				}
				l.l("get_link->"+project.Variables["link"].Value);
				l.lo("get_link->"+project.Variables["link"].Value);
			}	
		}		
		public void get_text_to_paste()
		{
			bol l=new bol(instance,project);
			var s=project.Lists["paste_text"];
			if(s.Count>0)
			{
				l.l("get_text_to_paste");
				l.lo("get_text_to_paste");
				lock(TextLocker)
				{
					project.Variables["tex_paste"].Value=s.ElementAt(0);
					s.RemoveAt(0);
					s.Add(project.Variables["tex_paste"].Value);
				}
				l.l("get_text_to_paste->"+project.Variables["tex_paste"].Value);
				l.lo("get_text_to_paste->"+project.Variables["tex_paste"].Value);
			}	
		}			
		public void s_click_Escrever()
		{
			Tab tab=instance.ActiveTab;
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "outerhtml", @"<menu\\ ng-click=\""onComposeNew\\(\\$event\\)\""\\ class=\""bt-write\\ btn-color-1\\ ng-scope\""><span\\ class=\""icon-write\""></span>\\ Escrever\\ </menu>", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "innerhtml", @"<span\\ class=\""icon-write\""></span>\\ Escrever\\ ", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "innertext", @"\\ Escrever\\ ", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "tagname", "menu", "regexp", 0);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			tab.WaitDownloading();	
		}
		public void send()
		{
			bol l =new bol(instance,project);
			l.l("send_mail");
			l.lo("send_mail");
			l.post_recip();
			l.post_subj();
			l.time_checkbox();
			l.text_to_simple();
			l.post_body();
			l.check_orig_mail();
			l.click_send();
			/*
			if(project.Variables["temp"].Value=="adresat_null")
			{
				l.post_recip();
				l.click_send();
				if(project.Variables["temp"].Value=="adresat_null") throw new Exception("Не удалось заполнить адресат");
			}
			l.check_res();
			*/
		}
		public void check_res()
		{
			bol l=new bol(instance,project);
			l.l("check_result");
			l.lo("check_result");
			if(project.Variables["temp"].Value.Contains("Aviso")){
				project.Variables["temp"].Value=new Regex(@"(?<=<p>).*?(?=</p>)").Match(project.Variables["temp"].Value).Value;
				if(project.Variables["temp"].Value=="Seu e-mail foi enviado")
				{
					l.l("check_result->OK");
				    l.lo("check_result->OK");
				}
				else {
					string message="новый результат на хороей ветке bol.uol temp="+project.Variables["temp"].Value+"\r\n\r\n\r\n\r\n\r\n\r\n"+instance.ActiveTab.DomText;
					ZennoPoster.HttpGet("https://api.telegram.org/bot731614490:AAG2XZ8H-v_FVGIom-GyqhhH5kwHfjz7QpA/sendmessage?chat_id=241662927&text="+message);
					throw new Exception("ddf");
				}
			}
		}
		public void text_to_simple()
		{
			instance.SetOkCancelConfirmPolicy("Ok");
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "outerhtml", @"<menu\\ ng-click=\""displayEditorSimpleMode\\(\\$event\\)\""\\ ng-if=\""onRichMode\""\\ class=\""bt-compose-draft\\ ng-scope\""><span\\ class=\""icon-no_format\""></span>\\ Texto\\ simples</menu>", "regexp", 0);
			if (he.IsVoid)  he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "innerhtml", @"<span\\ class=\""icon-no_format\""></span>\\ Texto\\ simples", "regexp", 0);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
		}
		public void post_body()
		{
			bol l =new bol(instance,project);
			l.l("post_body");
			l.lo("post_body");
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("textarea", "outerhtml", @"<textarea\\ style=\""visibility:\\ hidden;\\ display:\\ none;\""\\ id=\""field-body\""\\ class=\""field-body\\ textareaMessage\\ ng-untouched\\ ng-valid\\ flex\\ flex-box-v\\ ng-valid-parse\\ ng-pristine\""\\ ng-class=\""\\{\'flex\\ flex-box-v\':\\ \\(onRichMode\\ \\|\\|\\ !includeOriginalEmail\\)}\""\\ name=\""textareaMessage\""\\ wor-content-editable=\""field-body,,field-body-slot\""\\ ckeditor=\""editorOptions\""\\ ng-model=\""body\""\\ ng-model-options=\""\\.*", "regexp", 0);
			if(he.IsVoid)he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("textarea", "outerhtml", @"<textarea style=\""visibility: hidden; display: none;\"" id=\""field-body\"" class=\""field-body textareaMessage ng-untouched ng-valid flex flex-box-v ng-dirty ng-valid-parse\"" ng-class=\""{\'flex flex-box-v\': (onRichMode || !includeOriginalEmail)}\"" name=\""textareaMessage\"" wor-content-editable=\""field-body,,field-body-slot\"" ckeditor=\""editorOptions\"" ng-model=\""body\"" ng-model-options=\""{updateOn:\'default blur\', debounce:{\'default\':500, \'blur\':0}}\""></textarea>", "regexp", 0);
			if(he.IsVoid)he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByName("textareaMessage");
			if(he.IsVoid)
			{
				l.l("post_body->false");
				l.lo("post_body->false");
				l.lo("OK");
				throw new Exception("Не удалось написать тело письма");
			}
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["body"].Value, instance.EmulationLevel, false);
			instance.ActiveTab.WaitDownloading();
			l.l("post_body->OK");
			l.lo("post_body->OK");
		}	
		public void check_orig_mail()
		{
			bol l =new bol(instance,project);
			l.l("check_orig_mail");
			l.lo("check_orig_mail");
			
			HtmlElement he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("input:checkbox", "class", @"ng-untouched\\ ng-valid\\ ng-dirty\\ ng-valid-parse", "regexp", 0);
			if (he.IsVoid) he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByAttribute("input:checkbox", "fulltag", "input:checkbox", "text", 1);
			instance.WaitFieldEmulationDelay();
			he.SetValue("True", instance.EmulationLevel, false);
			instance.ActiveTab.WaitDownloading();
			l.l("check_orig_mail->OK");
			l.lo("check_orig_mail->OK");
		}		
		public void time_checkbox()
		{
			bol l =new bol(instance,project);
			l.l("time_checkbox");
			l.lo("time_checkbox");

			HtmlElement he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 3).FindChildByAttribute("select", "class", @"form-control\\ form-control-small\\ field\\ field-text-inline\\ ng-valid\\ ng-touched\\ ng-dirty\\ ng-valid-parse", "regexp", 0);
			if (he.IsVoid) he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 3).FindChildByAttribute("select", "InnerText", @"Não\\ lembrar\\ Na\\ data\\ do\\ evento\\ 5\\ minutos\\ antes\\ 10\\ minutos\\ antes\\ 15\\ minutos\\ antes\\ 30\\ minutos\\ antes\\ 1\\ hora\\ antes\\ 2\\ horas\\ antes\\ 6\\ horas\\ antes\\ 12\\ horas\\ antes\\ 1\\ dia\\ antes", "regexp", 0);	
			if (he.IsVoid) he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 3).FindChildByAttribute("select", "fulltag", "select", "text", 1);
			instance.WaitFieldEmulationDelay();
			he.SetValue("0", instance.EmulationLevel, false);
			instance.ActiveTab.WaitDownloading();
			l.l("time_checkbox->OK");
			l.lo("time_checkbox->OK");
		}		
		public void post_recip()
		{
			bol l =new bol(instance,project);
			l.l("send_mail->post_recip");
			l.lo("send_mail->post_recip");
			
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("input:text", "outerhtml", @".*realfieldid=\""field-to\"" id=\""fake_input__field-to\"" wor-autocomplete=\""fake_input__field-to\"" autocapitalize=\""off\"" autocorrect=\""off\"" autocomplete=\""off\"" type=\""text\"">", "text", 0);
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["log"].Value, instance.EmulationLevel, false);
			instance.ActiveTab.WaitDownloading();
			he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("input:text", "outerhtml", @".*realfieldid=\""field-to\"" id=\""fake_input__field-to\"" wor-autocomplete=\""fake_input__field-to\"" autocapitalize=\""off\"" autocorrect=\""off\"" autocomplete=\""off\"" type=\""text\"">", "text", 0);
			project.Variables["temp"].Value = he.GetAttribute("value");
			if(project.Variables["temp"].Value==project.Variables["log"].Value)
			{
				l.l("send_mail->post_recip->OK");
				l.lo("send_mail->post_recip->OK");
			}
			else
			{
				he = instance.ActiveTab.GetDocumentByAddress("0").FindElementById("fake_input__field-to");
				instance.WaitFieldEmulationDelay();
				he.SetValue(project.Variables["log"].Value, instance.EmulationLevel, false);
				instance.ActiveTab.WaitDownloading();
				l.l("send_mail->post_recip->udacha");
				l.lo("send_mail->post_recip->udacha");
			}
		}
		public void post_subj()
		{
			bol l =new bol(instance,project);
			l.l("send_mail->post_subj");
			l.lo("send_mail->post_subj");
			
				
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("input:text", "outerhtml", @"<input class=\""form-control form-control-small field field-text ng-pristine ng-untouched ng-valid ng-valid-maxlength\"" ng-keypress=\""preventSubmit($event)\"" ng-change=\""preventSubmit($event)\"" ng-paste=\""preventSubmit($event)\"" id=\""field-subject\"" ng-model=\""subject\"" value=\""\"" autocomplete=\""off\"" maxlength=.", "text", 0);
			instance.WaitFieldEmulationDelay();
			he.SetValue(project.Variables["subj"].Value, instance.EmulationLevel, false);
			he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("input:text", "outerhtml", @"<input\\ class=\""form-control\\ form-control-small\\ field\\ field-text\\ ng-untouched\\ ng-valid\\ ng-valid-maxlength\\ ng-dirty\\ ng-valid-parse\""\\ ng-keypress=\""preventSubmit\\(\\$event\\)\""\\ ng-change=\""preventSubmit\\(\\$event\\)\""\\ ng-paste=\""preventSubmit\\(\\$event\\)\""\\ id=\""field-subject\""\\ ng-model=\""subject\""\\ value=\"".*", "regexp", 0);
			project.Variables["temp"].Value= he.GetAttribute("value");
			if(project.Variables["temp"].Value==project.Variables["subj"].Value)
			{
				l.l("send_mail->post_subj->");
				l.lo("send_mail->post_subj->");
				
			}
			/*
			else
			{
				he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("input:text", "class", "form-control\\ form-control-small\\ field\\ field-text\\ ng-pristine\\ ng-untouched\\ ng-valid\\ ng-valid-maxlength", "regexp", 0);
				he.SetValue(project.Variables["subj"].Value, instance.EmulationLevel, false);
				he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("input:text", "outerhtml", @"<input\\ class=\""form-control\\ form-control-small\\ field\\ field-text\\ ng-untouched\\ ng-valid\\ ng-valid-maxlength\\ ng-dirty\\ ng-valid-parse\""\\ ng-keypress=\""preventSubmit\\(\\$event\\)\""\\ ng-change=\""preventSubmit\\(\\$event\\)\""\\ ng-paste=\""preventSubmit\\(\\$event\\)\""\\ id=\""field-subject\""\\ ng-model=\""subject\""\\ value=\"".*", "regexp", 0);
				project.Variables["temp"].Value= he.GetAttribute("value");
				if(project.Variables["temp"].Value==project.Variables["subj"].Value)
				{
					l.l("send_mail->post_subj->");
					l.lo("send_mail->post_subj->");
					
				}
				else
				{
					he = instance.ActiveTab.GetDocumentByAddress("0").FindElementById("field-subject");
					project.Variables["temp"].Value= he.GetAttribute("value");
					if(project.Variables["temp"].Value==project.Variables["subj"].Value)
					{
						l.l("send_mail->post_subj->");
						l.lo("send_mail->post_subj->");
						
					}
					else throw new Exception("Не удалось запостить тему письма");
					
				}
			}
			*/
			
		}
		public void search_coords()
		{
			var sc=project.Tables["search_cords"];//для поиска координат
			bol l =new bol(instance,project);
			project.Variables["left"].Value="";
			project.Variables["width"].Value="";
			project.Variables["top"].Value="";
			project.Variables["height"].Value="";
			HtmlElement he;
			for(int i=0;i<sc.RowCount;i++)
			{
				string tag=sc.GetCell(0,i);
				string tagAtr=sc.GetCell(1,i);
				string tagNa=sc.GetCell(2,i);
				string te=sc.GetCell(3,i);
				int e=int.Parse(sc.GetCell(4,i));
				he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute(tag, tagAtr, tagNa, te, e);
				project.Variables["left"].Value = he.GetAttribute("left");//x ot
				project.Variables["width"].Value = he.GetAttribute("width");//x do
				project.Variables["top"].Value = he.GetAttribute("top");//y ot
				project.Variables["height"].Value = he.GetAttribute("height");//y do
				if(project.Variables["left"].Value!=""&&project.Variables["width"].Value!=""&&project.Variables["top"].Value!=""&&project.Variables["height"].Value!="")
				{
					sc.Clear();
					l.mouse_click(project.Variables["left"].Value, project.Variables["width"].Value, project.Variables["top"].Value, project.Variables["height"].Value);
					break;
				}
			}
			if(project.Variables["left"].Value==""&&project.Variables["width"].Value==""&&project.Variables["top"].Value==""&&project.Variables["height"].Value=="")throw new Exception("не удалось выполнить клик мышью");
		}
		public void mouse_click(string lef,string wi,string to,string hei)
		{
			int x_ot=int.Parse(lef);
			int x_do=int.Parse(wi);
			int y_ot=int.Parse(to);
			int y_do=int.Parse(hei);
			x_do+=x_ot;
			y_do+=y_ot;
			instance.Click(x_ot, x_do, y_ot, y_do, "Left", "Normal");
		}
		public void lo(string text)
		{
			if(text=="OK")
			{
				project.Variables["lol"].Value+="\r\n"+text;
				var list=project.Lists["log"];
				lock(LogFile) list.Add(project.Variables["lol"].Value);
			}
			else
			{
				if(project.Variables["lol"].Value=="")project.Variables["lol"].Value="_________________________________________________________________"+"\r\n"+text;
				else project.Variables["lol"].Value+="\r\n"+text;
			}

		}
		public void l(string text)
		{//лог
			project.SendInfoToLog(project.Variables["log"].Value+"|"+text,true);
		}
		public void w(string text)
		{//предупреждение
			project.SendWarningToLog(project.Variables["log"].Value+"|"+text,true);
		}
		public void e(string text)
		{//ошибка
			project.SendErrorToLog(project.Variables["log"].Value+"|"+text,true);
		}
		public void click_send()
		{
			bol l =new bol(instance,project);
			l.l("click_send");
			l.lo("click_send");
			project.Variables["temp"].Value="";
			HtmlElement he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "innerhtml", @"<span\\ class=\""icon-enviar\""></span>&nbsp;&nbsp;Enviar", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "outerhtml", @"<menu\\ ng-click=\""onSend\\(\\)\""\\ class=\""bt-compose-send\\ highlight\""><span\\ class=\""icon-enviar\""></span>&nbsp;&nbsp;Enviar</menu>", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "class", "bt-compose-send\\ highlight", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "innertext", "Enviar", "regexp", 0);
			if (he.IsVoid) he = instance.ActiveTab.GetDocumentByAddress("0").FindElementByAttribute("menu", "class", "bt-compose-send\\ highlight", "regexp", 0);
			instance.WaitFieldEmulationDelay();
			he.RiseEvent("click", instance.EmulationLevel);
			/*
			for(int i=0;i<1000;i++)
			{
				project.Variables["temp"].Value= new Regex(@"(?<=class=""content""><span\ class=""title"">).*?(?=<a)").Match(instance.ActiveTab.DomText).Value;
				if(project.Variables["temp"].Value.Contains("Aviso"))
				{
					l.l("click_send->OK");
					l.lo("click_send->OK");
					break;
				}
				else if(project.Variables["temp"].Value.Contains("Atenção"))
				{
					l.e("click_send->error");
					l.lo("click_send->error");
					project.Variables["temp"].Value= new Regex(@"(?<=<p>).*?(?=</p>)").Match(project.Variables["temp"].Value).Value;
					if(project.Variables["temp"].Value=="Nenhum destinatário foi informado")
					{
						l.e("click_send->input_recip->null");
						l.lo("click_send->input_recip->null");
						project.Variables["temp"].Value="adresat_null";
						break;
					}
					else throw new Exception("Ошибка на кнопки постить письмо");
				}
			}
			if(!project.Variables["temp"].Value.Contains("Aviso")||!project.Variables["temp"].Value.Contains("Atenção"))throw new Exception("Ошибка на кнопки постить письмо(нет ниодного ответа)");
		*/
		}
		
	}
	class nosok
	{	
		private IZennoPosterProjectModel project;
		public nosok(IZennoPosterProjectModel project)
		{
			this.project=project;
		}
		public static object ProxyLocker = new object();
		
		public void get_upd()
		{
			project.Variables["responce"].Value=ZennoPoster.HttpGet(project.Variables["url"].Value,"","",ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
		}
		public void upd_list()
		{
			var n=project.Lists["proxy"];
			lock(ProxyLocker)
			{
				n.Clear();
				Macros.TextProcessing.ToList(project.Variables["responce"].Value, "\r\n", "Text", project, n);
			}
		}
		public void wait()
		{
			int t=int.Parse(project.Variables["wait"].Value);
			t=t*60;
			System.Threading.Thread.Sleep(t * 1000);
		}
	}
}