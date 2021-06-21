using System.IO;
using System.Text;
using System.Windows.Forms;
using Roster.Business.Extensions;
namespace Roster.Business
{
    public static class Setting
    {
        private static int DefaultExtenTimeProces = 5;
        private static string fullPathFile = Path.Combine(Application.StartupPath, "setting.txt");
        public static void WriteFile(string ExtenTimeProces)
        {
            StringBuilder buildMesageError = new StringBuilder();
            using (StreamWriter stream = new StreamWriter(fullPathFile, true))
            {
                try
                {
                    stream.WriteLine(ExtenTimeProces);
                }
                catch
                {

                }
            }
        }

        public static int GetExtenTimeProcesFromFileConfig()
        {
            if (!File.Exists(fullPathFile))
            {
                return DefaultExtenTimeProces;
            }
            string[] readText = File.ReadAllLines(fullPathFile);
            if (readText.Length == 0)
            {
                return DefaultExtenTimeProces;
            }
            return readText[0].ToInt(DefaultExtenTimeProces);
        }

    }
}
