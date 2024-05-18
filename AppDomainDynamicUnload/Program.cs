using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace AppDomainDynamicUnload
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//1) Создаем домен приложения:
			AppDomain domain=AppDomain.CreateDomain("ConsoleDomain");
			//2) Загружаем DLL в созданный домен:
			Assembly asm = domain.Load(AssemblyName.GetAssemblyName("SampleLibrary.dll"));
			//3) Получаем модуль из которого будем выполнять вызов:
			Module module = asm.GetModule("SampleLibrary.dll");
			//4) Получаем класс из DLL-библиотеки:
			Type type = module.GetType("SampleLibrary.SampleClass");
			//5) Вытаскиваем метод из класса, который будем вызывать:
			MethodInfo method = type.GetMethod("Hello");
			//6) Вызываем метод:
			method.Invoke(null, null);

			AppDomain.Unload(domain);
		}
	}
}
