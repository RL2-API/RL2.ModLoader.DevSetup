using NativeFileDialogSharp;
using System;
using System.IO;
using System.Threading;

public class Program
{
	static string OutputLibPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Replace("RL2.ModLoader.DevSetup\\", "") + "lib\\";

	public static void Main(string[] args) {
		Console.WriteLine("Welcome to the RL2.ModLoader.DevSetup");
		Console.WriteLine("Choose your games installation directory");
		Thread.Sleep(500);

		DialogResult result = Dialog.FolderPicker();
		string DataPath = result.Path + "\\Rogue Legacy 2_Data";
		string ManagedPath = DataPath + "\\Managed";

		if (!Directory.Exists(DataPath) || !Directory.Exists(ManagedPath)) {
			Console.WriteLine("The provided path is incorrect");
			return;
		}

		if (!Directory.Exists(OutputLibPath)) {
			Directory.CreateDirectory(OutputLibPath);
		}

		Console.WriteLine("\nStart copying libs...");
		DirectoryInfo librariesDirectory = new DirectoryInfo(ManagedPath);
		foreach (FileInfo fileInfo in librariesDirectory.GetFiles("**.dll")) {
			if (fileInfo.Name == "RL2.ModLoader.dll" || fileInfo.Name == "RL2.API.dll") {
				continue;
			}
			Console.WriteLine(fileInfo.Name);
			File.Copy(fileInfo.FullName, OutputLibPath + fileInfo.Name, true);
		}
		Console.WriteLine("Press any key to exit...");
		Console.ReadKey();
	}
}