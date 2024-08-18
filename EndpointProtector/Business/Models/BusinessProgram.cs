using Database.Models;

namespace EndpointProtector.Business.Models
{
	public class BusinessProgram
	{
		public BusinessProgram(string path, string name, string hash)
		{
			Path = path;
			Name = name;
			Hash = hash;
		}

		public string Path { get; set; }
		public string Name { get; set; }
		public string Hash { get; set; }

		public static implicit operator DbProgram(BusinessProgram program) => new DbProgram(program.Path, program.Name, program.Hash);
	}
}
