using Burdy.KodTest.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burdy.KodTest.Services
{
    public class PackageService
    {
        private List<Package> packages = new();
        private Random random = new Random();
        public void InitiateAllPackages()
        {
            for (int i = 1; i < 10; i++)
            {
                var package = new Package()
                {
                    PackageNumber = 345678910111213111 + i,
                    Height = random.Next(5, 60),
                    Lenght = random.Next(5, 60),
                    Weight = random.Next(1, 20),
                    Width = random.Next(5, 60),
                };

                packages.Add(package);
            }
        }
        public Package GetPackageByPackageNumber(long packageNumber)
        {
            var package = packages.Select(x=> x).Where(x=>x.PackageNumber.Equals(packageNumber)).FirstOrDefault();

            return package;
        }
        public (string, bool) CheckPackageNumber(long packageNumber)
        {
            var count = Math.Floor(Math.Log10(packageNumber) + 1);

            if (count != 18)
            {
                return ("Invalid package number", false);
            }
            
            return (packageNumber.ToString(), true);
        }
    }
}
