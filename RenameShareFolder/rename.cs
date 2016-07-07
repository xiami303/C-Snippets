using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;


namespace Rename
{
	class Program
	{
		static void Main(string[] args)
		{
			Command cmd = new Command();
			string src = @"\\server01\CopyTemp\111\990684";
			string dest = @"\\server01\CopyTemp\112\990684";
			
			DateTime startTime = DateTime.Now;
			String start = String.Format("{0:yyyy-MM-dd-HH-mm-ss}", startTime);
			int result = cmd.RunCommand("move " + src + " " + dest);

			DateTime endTime = DateTime.Now;
			String end = String.Format("{0:yyyy-MM-dd-HH-mm-ss}", endTime);
			Console.WriteLine("start at: {0},  end at {1}", start, end);

			Console.ReadLine();
		}
	}



	public class Command
	{
		private Process proc = null;

		public Command()
		{
			proc = new Process();
		}

		public int RunCommand(string cmd){
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.FileName = "cmd.exe";
			//proc.StartInfo.WorkingDirectory = @"\\avshars02\COD_Archive";
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardOutput = true;

		
			proc.Start();
			proc.StandardInput.WriteLine(cmd + "&exit");
			proc.StandardInput.AutoFlush = true;

			string output = proc.StandardOutput.ReadToEnd();
			proc.WaitForExit();
			/*
			string outStr ="";
			string tmptStr = "";
			while (tmptStr != "")
			{
				outStr += outStr;
				tmptStr = proc.StandardOutput.ReadLine();
			} */

			proc.Close();
			Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
			Console.WriteLine(output);

			if (output.Contains("complete"))
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}

		public void RunProgram(string programName, string cmd)
		{
			Process proc = new Process();
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.FileName = programName;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.Start();
			if (cmd.Length != 0)
			{
				proc.StandardInput.WriteLine(cmd);
			}
			proc.Close();
		}

		public void RunProgram(string programName)
		{
			this.RunProgram(programName, "");
		} 
	}
}
