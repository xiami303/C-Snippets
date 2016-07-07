						// set tmp virtual drive for build path
						String basepath = @"\\share01";

						int mapresult = NetResource.MapNetworkDrive("T", param.BuildPath);
						if (mapresult == 0)
						{
							try
							{
								string src = @"\\share01\test\just\89973";
								string dest = @"\\share01\test\just02\7522";
								if (Directory.Exists(src))
								{
									DirectoryInfo di = new DirectoryInfo(src.Replace(basepath, "T:"));
									
									String dest = dest.Replace(basepath, "T:");
									if (Directory.Exists(dest))
									{
										Directory.Delete(dest, true);
									}
									
									di.MoveTo(dest);
								}
							}
							catch (Exception e)
							{
								Console.WriteLine("Rename error :  {0}", e.Message);
							}
						}
						NetResource.DisconnectNetworkDrive("T", true);
