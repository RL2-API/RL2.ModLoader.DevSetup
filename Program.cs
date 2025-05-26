using NativeFileDialogSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading;

public class Program
{
	static string OutputPath = string.Join('\\', AppDomain.CurrentDomain.SetupInformation.ApplicationBase!.Split("\\").SkipLast(2));

	public static void Main(string[] args) {
		Console.WriteLine("Welcome to the RL2.ModLoader.DevSetup");
		Console.WriteLine("Choose your game's .exe file");
		Thread.Sleep(500);

		DialogResult result = Dialog.FileOpen();

		if (!result.Path.EndsWith("Rogue Legacy 2.exe")) {
			Console.WriteLine("The provided path is incorrect");
			Console.WriteLine("Please select the 'Rogue Legacy 2.exe' file");
			return;
		}

		string directory = result.Path.Replace("\\Rogue Legacy 2.exe", "");
		string DataPath = directory + "\\Rogue Legacy 2_Data";
		string ManagedPath = DataPath + "\\Managed";


		File.WriteAllText(OutputPath + "\\RL2.Dev.targets", $"""
		<Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
			<!-- MSBuild variables -->
			<PropertyGroup>
				<RL2_Path>{directory}\</RL2_Path>
				<RL2_DataPath>{DataPath}\</RL2_DataPath>
				<RL2_LibPath>{ManagedPath}\</RL2_LibPath>
				<RL2_ModsPath>{DataPath}\Mods\</RL2_ModsPath>
			</PropertyGroup>

			<!-- Shared references -->
			<ItemGroup>
				<Reference Include="$(RL2_LibPath)*.dll">
					<Private>false</Private>
				</Reference>
			</ItemGroup>
		</Project>
		""");

		Console.WriteLine($"RL2.ModLoader.targets created in {OutputPath}");
		Console.WriteLine("Closing in 2 seconds...");
		Thread.Sleep(2_000);
	}
}